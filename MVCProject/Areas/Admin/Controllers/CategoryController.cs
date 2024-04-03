using MVCProject.Common;
using MVCProject.Library;
using MVCProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCProject.Areas.Admin.Controllers
{
    public class CategoryController : Controller
    {
        private MojiDbContext db = new MojiDbContext();

        //GET: Admin/Category
        /*[CustomAuthorizeAttribute(RoleID = "SALESMAN")]*/

        public ActionResult Index()
        {
            ViewBag.listCate = db.Categories.Where(m => m.Status != 0).ToList();
            var list = db.Categories.Where(m => m.Status > 0).ToList() ;
            return View(list);
        }

        public ActionResult Create()
        {

            ViewBag.listCate = db.Categories.Where(m => m.Status != 0).ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Category category)
        {

            if (ModelState.IsValid)
            {
                //category
                string slug = MyString.ToSlug(category.Name.ToString());
                if (db.Categories.Where(m => m.Slug == slug).Count() > 0)
                {
                    Message.set_flash("Loại sản phẩm đã tồn tại trong bảng Category", "danger");
                    return View(category);
                }
                //topic

               /* if (db.top.Where(m => m.slug == slug).Count() > 0)
                {
                    Message.set_flash("Loại sản phẩm đã tồn tại trong bảng Topic", "danger");
                    return View(category);
                }*/
                if (db.Products.Where(m => m.Slug == slug).Count() > 0)
                {
                    Message.set_flash("Loại sản phẩm đã tồn tại trong bảng Product", "danger");
                    return View(category);
                }
                category.Slug = slug;
                category.Created_at = DateTime.Now;
                category.Updated_at = DateTime.Now;
                category.Created_by = int.Parse(Session["Admin_id"].ToString());
                category.Updated_by = int.Parse(Session["Admin_id"].ToString());
                db.Categories.Add(category);
                db.SaveChanges();
                Link tt_link1 = new Link();
                tt_link1.Slug = slug;
                tt_link1.TableId = 2;
                tt_link1.Type = "category";
                tt_link1.ParentId = category.ID;
                db.Links.Add(tt_link1);

                db.SaveChanges();
                Message.set_flash("Thêm  thành công", "success");
                return RedirectToAction("index");
            }
            Message.set_flash("Thêm  Thất Bại", "danger");
            ViewBag.listCate = db.Categories.Where(m => m.Status != 0).ToList();// truyền vào
            return View(category);
        }
    }
}