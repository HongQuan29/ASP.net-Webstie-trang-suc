using MVCProject.Common;
using MVCProject.Library;
using MVCProject.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace MVCProject.Areas.Admin.Controllers
{
    /*[CustomAuthorizeAttribute(RoleID = "USER")]*/
    public class UserController : Controller
    {
        private MojiDbContext db = new MojiDbContext();

        //GET admin/user
        public ActionResult Index()
        {
            var list = db.Users.Where(m => m.Status != 0).OrderByDescending(m => m.ID).ToList();
            return View(list);
        }

        // admin/user/create
        public ActionResult Create()
        {
            ViewBag.Role = db.Roles.ToList();
            return View();  
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( User user, FormCollection data)
        {
            if (ModelState.IsValid)
            {
                string password1 = data["password1"];
                string password2 = data["password2"];

                string username = user.Username;
                var Luser = db.Users.Where(m => m.Status == 1 && m.Username == username);
                if (password1 != password2) { ViewBag.error = "PassWord không khớp"; }
                if (Luser.Count() > 0) { ViewBag.error1 = "Tên Đăng nhâp đã tồn tại"; }
                else
                {
                    string pass = MyString.ToMD5(password1);
                    user.Img = "ádasd";
                    user.Password = pass;
                    user.Created_at = DateTime.Now;
                    user.Updated_at = DateTime.Now;
                    user.Created_by = 1;
                    user.Updated_by = 1;
                    db.Users.Add(user);
                    db.SaveChanges();
                    Message.set_flash("Tạo user  thành công", "success");
                    return RedirectToAction("Index");
                }
            }
            return View(user);
        }

        // GET: Admin/User/Edit/1
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User muser = db.Users.Find(id);
            if (muser == null)
            {
                return HttpNotFound();
            }
            ViewBag.role = db.Roles.ToList();
            return View(muser);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                user.Img = "ádasd";
                user.Updated_at = DateTime.Now;
                user.Updated_by = int.Parse(Session["Admin_id"].ToString());
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                Message.set_flash("Cập nhật thành công", "success");
                return RedirectToAction("Index");
            }
            return View(user);
        }

        //status
        public ActionResult Status(int id)
        {
            User user = db.Users.Find(id);
            user.Status = (user.Status == 1) ? 2 : 1;
            user.Updated_at = DateTime.Now;
            user.Updated_by = int.Parse(Session["Admin_id"].ToString());
            db.Entry(user).State = EntityState.Modified;
            db.SaveChanges();
            Message.set_flash("Thay đổi trạng thái thành công", "success");
            return RedirectToAction("Index");
        }

        //trash
        public ActionResult trash()
        {
            var list = db.Users.Where(m => m.Status == 0).ToList();
            return View("Trash", list);
        }

        public ActionResult Deltrash(int id)
        {
            User user = db.Users.Find(id);
            user.Status = 0;
            user.Updated_at = DateTime.Now;
            user.Updated_by = int.Parse(Session["Admin_id"].ToString());
            db.Entry(user).State = EntityState.Modified;
            db.SaveChanges();
            Message.set_flash("Xóa thành công", "success");
            return RedirectToAction("Index");
        }

        public ActionResult Retrash(int id)
        {
            User user = db.Users.Find(id);
            user.Status = 2;
            user.Updated_at = DateTime.Now;
            user.Updated_by = int.Parse(Session["Admin_id"].ToString());
            db.Entry(user).State = EntityState.Modified;
            db.SaveChanges();
            Message.set_flash("khôi phục thành công", "success");
            return RedirectToAction("trash");
        }

    }
}