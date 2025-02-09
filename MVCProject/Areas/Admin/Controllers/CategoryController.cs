﻿using MVCProject.Common;
using MVCProject.Library;
using MVCProject.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
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
            var list = db.Categories.Where(m => m.Status > 0).ToList();
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

        // GET: Admin/Category/Edit/5
        public ActionResult Edit(int? id)
        {
            ViewBag.listCate = db.Categories.Where(m => m.Status != 0).ToList();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                string slug = MyString.ToSlug(category.Name.ToString());
                category.Slug = slug;
                category.Updated_at = DateTime.Now;
                category.Updated_by = int.Parse(Session["Admin_id"].ToString());
                db.Entry(category).State = EntityState.Modified;
                Link tt_link = db.Links.Where(m => m.Slug == category.Slug).FirstOrDefault();
                if (tt_link == null)
                {
                    Link tt_link1 = new Link();
                    tt_link1.Slug = slug;
                    tt_link1.TableId = 2;
                    tt_link1.Type = "category";
                    tt_link1.ParentId = category.ID;
                    db.Links.Add(tt_link1);
                }
                else
                {
                    var thisLink = db.Links.Where(m => m.TableId == 2 && m.ParentId == category.ID).First();
                    Link tt_link3 = db.Links.Find(thisLink.ID);
                    tt_link3.Slug = slug;
                    tt_link3.TableId = 2;
                    tt_link3.Type = "category";
                    tt_link3.ParentId = category.ID;
                    db.Entry(tt_link3).State = EntityState.Modified;
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            Message.set_flash("Sửa thất bại", "success");
            return View(category);
        }
        //status
        public ActionResult Status(int id)
        {
            Category category = db.Categories.Find(id);
            category.Status = (category.Status == 1) ? 2 : 1;
            category.Updated_at = DateTime.Now;
            category.Updated_by = int.Parse(Session["Admin_id"].ToString());
            db.Entry(category).State = EntityState.Modified;
            db.SaveChanges();
            Message.set_flash("Thay đổi trang thái thành công", "success");
            return RedirectToAction("Index");
        }

        //trash
        public ActionResult trash()
        {
            var list = db.Categories.Where(m => m.Status == 0).ToList();
            return View("Trash", list);                 // danh mục
        }

        public ActionResult Deltrash(int id)
        {
            Category category = db.Categories.Find(id);
            category.Status = 0;
            category.Updated_at = DateTime.Now;
            category.Updated_by = int.Parse(Session["Admin_id"].ToString());
            db.Entry(category).State = EntityState.Modified;
            db.SaveChanges();
            Message.set_flash("Xóa thành công", "success");
            return RedirectToAction("Index");           // hiển thị danh mục
        }

        public ActionResult Retrash(int id)
        {
            Category category = db.Categories.Find(id);
            category.Status = 2;
            category.Updated_at = DateTime.Now;
            category.Updated_by = int.Parse(Session["Admin_id"].ToString());
            db.Entry(category).State = EntityState.Modified;
            db.SaveChanges();
            Message.set_flash("khôi phục thành công", "success");
            return RedirectToAction("trash");
        }

        public ActionResult deleteTrash(int id)
        {
            Category mcategory = db.Categories.Find(id);
            db.Categories.Remove(mcategory);
            db.SaveChanges();
            Message.set_flash("Đã xóa vĩnh viễn 1 sản phẩm", "success");
            return RedirectToAction("trash");
        }
    }
}