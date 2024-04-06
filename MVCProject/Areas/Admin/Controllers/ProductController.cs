using MVCProject.Library;
using MVCProject.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MVCProject.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        // GET: Admin/Product
        MojiDbContext db = new MojiDbContext();
        public ActionResult Index()
        {
            var list = db.Products.Where(m => m.Status != 0).OrderByDescending(m => m.ID).ToList();
            return View(list);
        }
        public ActionResult Create()
        {
            ViewBag.ListCategory = db.Categories.Where(m => m.Status != 0 && m.ID >= 1).ToList();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(Product p, HttpPostedFileBase f)
        {
            ViewBag.ListCategory = db.Categories.Where(m => m.Status != 0 && m.ID >= 1).ToList();
            if(ModelState.IsValid)
            {
                // lấy tên sản phẩm làm slug
                string slug = MyString.ToSlug(p.Name.ToString());
                if (db.Categories.Where(m=>m.Slug == slug).Count() > 0)
                {
                    Message.set_flash("Sản phẩm đã tồn tại", "danger");
                    return View(p);
                }
                if (db.Products.Where(m => m.Slug == slug).Count() > 0)
                {
                    Message.set_flash(" Sản phẩm đã tồn tại", "danger");
                    return View(p);
                }
                // lấy tên loại sản phẩm
                var namecateDb = db.Categories.Where(m => m.ID == p.CatId).First();
                string namecate = MyString.ToStringWithoutSpace(namecateDb.Name);
                // lấy tên ảnh
                f = Request.Files["img"];
                string filename = f.FileName.ToString();
                //lấy đuôi ảnh
                string ExtensionFile = MyString.GetFileExtension(filename);
                //lấy tên mới của ảnh slug + [đuôi ảnh lấy đc]
                string namefilenew = namecate + "/" + slug + "." + ExtensionFile;
                //lưu ảnh vào đường đẫn
                var path = Path.Combine(Server.MapPath("~/public/images"), namefilenew);
                //nếu thư mục k tồn tại thì tạo thư mục
                var folder = Server.MapPath("~/public/images/" + namecate);
                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }
                f.SaveAs(path);
                p.Img = namefilenew;
                p.Slug = slug;
                p.Sold = 0;
                p.Created_at = DateTime.Now;
                p.Updated_at = DateTime.Now;
                //p.Created_by = int.Parse(Session["Admin_id"].ToString());
                //p.Updated_by = int.Parse(Session["Admin_id"].ToString());
                db.Products.Add(p);
                db.SaveChanges();
                Message.set_flash("Thêm thành công", "success");
                return RedirectToAction("index");
            }
            Message.set_flash("Thêm Thất Bại", "danger");
            return View(p);
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product p = db.Products.Find(id);
            if (p == null)
            {
                return HttpNotFound();
            }
            ViewBag.ListCategory = db.Categories.Where(m => m.Status != 0 && m.ID >= 1).ToList();
            return View(p);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(Product p, HttpPostedFileBase f) 
        {
            if(ModelState.IsValid)
            {
                string slug = MyString.ToSlug(p.Name.ToString());
                f = Request.Files["img"];
                string filename = f.FileName.ToString();
                if (filename.Equals("") == false)
                {
                    var namecateDb = db.Categories.Where(m => m.ID == p.CatId).First();
                    string namecate = MyString.ToStringWithoutSpace(namecateDb.Name);
                    string ExtensionFile = MyString.GetFileExtension(filename);
                    string namefilenew = namecate + "/" + slug + "." + ExtensionFile;
                    var path = Path.Combine(Server.MapPath("~/public/images"), namefilenew);
                    var folder = Server.MapPath("~/public/images/" + namecate);
                    if (!Directory.Exists(folder))
                    {
                        Directory.CreateDirectory(folder);
                    }
                    f.SaveAs(path);
                    p.Img = namefilenew;
                }
                p.Slug = slug;
                p.Updated_at = DateTime.Now;
                //p.Updated_by = int.Parse(Session["Admin_id"].ToString());
                db.Entry(p).State = EntityState.Modified;
                db.SaveChanges();
                ViewBag.ListCategory = db.Categories.Where(m => m.Status != 0 && m.ID >= 1).ToList();
                Message.set_flash("Sửa thành công", "success");
                return RedirectToAction("Index");
            }
            Message.set_flash("Sửa thất bại", "danger");
            ViewBag.ListCategory = db.Categories.Where(m => m.Status != 0 && m.ID >= 1).ToList();
            return View(p);
        }

        public ActionResult Delete(int id)
        {
            Product p = db.Products.Find(id);
            p.Status = 0;
            p.Updated_at = DateTime.Now;
            p.Updated_by = int.Parse(Session["Admin_id"].ToString());
            db.Entry(p).State = EntityState.Modified;
            db.SaveChanges();
            Message.set_flash("Xóa thành công", "success");
            return RedirectToAction("Index");
        }

        public ActionResult Status(int id)
        {
            Product product = db.Products.Find(id);
            product.Status = (product.Status == 1) ? 2 : 1;
            product.Updated_at = DateTime.Now;
            product.Updated_by = int.Parse(Session["Admin_id"].ToString());
            db.Entry(product).State = EntityState.Modified;
            db.SaveChanges();
            Message.set_flash("Thay đổi trang thái thành công", "success");
            return RedirectToAction("Index");
        }

        public ActionResult trash()
        {
            var list = db.Products.Where(m => m.Status == 0).ToList();
            return View("Trash", list);
        }

        public ActionResult Deltrash(int id)
        {
            Product product = db.Products.Find(id);
            product.Status = 0;
            product.Updated_at = DateTime.Now;
            product.Updated_by = int.Parse(Session["Admin_id"].ToString());
            db.Entry(product).State = EntityState.Modified;
            db.SaveChanges();
            Message.set_flash("Xóa thành công", "success");
            return RedirectToAction("Index");
        }

        public ActionResult Retrash(int id)
        {
            Product product = db.Products.Find(id);
            product.Status = 2;
            product.Updated_at = DateTime.Now;
            product.Updated_by = int.Parse(Session["Admin_id"].ToString());
            db.Entry(product).State = EntityState.Modified;
            db.SaveChanges();
            Message.set_flash("khôi phục thành công", "success");
            return RedirectToAction("trash");
        }

        public ActionResult deleteTrash(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
            Message.set_flash("Đã xóa vĩnh viễn 1 sản phẩm", "success");
            return RedirectToAction("trash");
        }
    }
}