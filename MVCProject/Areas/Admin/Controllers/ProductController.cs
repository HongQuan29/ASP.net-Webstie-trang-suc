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
        MojiDbContext _context = new MojiDbContext();
        public ActionResult Index()
        {
            var list = _context.Products.Where(m => m.Status != 0).OrderByDescending(m => m.ID).ToList();
            return View(list);
        }
        public ActionResult Create()
        {
            ViewBag.ListCategory = _context.Categories.Where(m => m.Status != 0 && m.ID >= 1).ToList();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(Product p, HttpPostedFileBase f)
        {
            ViewBag.ListCategory = _context.Categories.Where(m => m.Status != 0 && m.ID >= 1).ToList();
            if(ModelState.IsValid)
            {
                // lấy tên sản phẩm làm slug
                string slug = MyString.ToSlug(p.Name.ToString());
                if (_context.Categories.Where(m=>m.Slug == slug).Count() > 0)
                {
                    Message.set_flash("Sản phẩm đã tồn tại", "danger");
                    return View(p);
                }
                if (_context.Products.Where(m => m.Slug == slug).Count() > 0)
                {
                    Message.set_flash(" Sản phẩm đã tồn tại", "danger");
                    return View(p);
                }
                // lấy tên loại sản phẩm
                var namecateDb = _context.Categories.Where(m => m.ID == p.CatId).First();
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
                _context.Products.Add(p);
                _context.SaveChanges();
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
            Product p = _context.Products.Find(id);
            if (p == null)
            {
                return HttpNotFound();
            }
            ViewBag.ListCategory = _context.Categories.Where(m => m.Status != 0 && m.ID >= 1).ToList();
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
                    var namecateDb = _context.Categories.Where(m => m.ID == p.CatId).First();
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
                _context.Entry(p).State = EntityState.Modified;
                _context.SaveChanges();
                ViewBag.ListCategory = _context.Categories.Where(m => m.Status != 0 && m.ID >= 1).ToList();
                Message.set_flash("Sửa thành công", "success");
                return RedirectToAction("Index");
            }
            Message.set_flash("Sửa thất bại", "danger");
            ViewBag.ListCategory = _context.Categories.Where(m => m.Status != 0 && m.ID >= 1).ToList();
            return View(p);
        }

       
    }
}