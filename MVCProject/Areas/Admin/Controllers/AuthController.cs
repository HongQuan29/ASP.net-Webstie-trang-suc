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
    public class AuthController : Controller
    {
        // GET: Admin/Auth
        MojiDbContext _context = new MojiDbContext();
        public ActionResult login()
        {
            return View("login");
        }
        [HttpPost]
        public ActionResult login(FormCollection f)
        {
            String Username = f["username"];
            string Pass = MyString.ToMD5(f["password"]);
            var user_account = _context.Users.Where(m => m.Access != 1 && m.Status == 1 && (m.Username == Username));
            var userC = _context.Users.Where(m => m.Username.Equals(Username) && m.Access == 1).ToList();
            if (userC.Any())
            {
                ViewBag.error = "Bạn không có quyền đăng nhập";
            }
            else
            {
                if (user_account.Count() == 0)
                {
                    ViewBag.error = "Tên Đăng Nhập Không Đúng";
                }
                else
                {
                    var pass_account = _context.Users.Where(m => m.Access != 1 && m.Status == 1 && m.Password == Pass);
                    if (pass_account.Count() == 0)
                    {
                        ViewBag.error = "Mật Khẩu Không Đúng";
                    }

                    else
                    {
                        var user = user_account.First();
                        Role role = _context.Roles.Where(m => m.ParentId == user.Access).First();
                        var userSession = new UserLogin();
                        userSession.UserName = user.Username;
                        userSession.UserID = user.ID;
                        userSession.GroupID = role.GroupID;
                        userSession.AccessName = role.AccessName;
                        Session.Add(CommonConstants.USER_SESSION, userSession);
                        var i = Session["SESSION_CREDENTIALS"];
                        Session["Admin_id"] = user.ID;
                        Session["Admin_user"] = user.Username;
                        Session["Admin_fullname"] = user.Fullname;
                        Response.Redirect("~/Admin");
                    }
                }
            }
            ViewBag.sess = Session["Admin_id"];
            return View("login");
        }

        public ActionResult logout()
        {
            Session["Admin_id"] = "";
            Session["Admin_user"] = "";
            Response.Redirect("~/Admin");
            return View();
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = _context.Users.Find(id);
            ViewBag.role = _context.Roles.Where(m => m.ParentId == user.Access).First();
            if (user == null)
            {
                return HttpNotFound();
            }
            return View("information", user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //cập nhật thông tin ng dùng
        public ActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                user.Img = "ádasd";
                user.Access = 0;
                user.Created_at = DateTime.Now;
                user.Updated_at = DateTime.Now;
                user.Created_by = int.Parse(Session["Admin_id"].ToString());
                user.Updated_by = int.Parse(Session["Admin_id"].ToString());
                _context.Entry(user).State = EntityState.Modified;
                _context.SaveChanges();
                Message.set_flash("Cập nhật thành công", "success");
                ViewBag.role = _context.Roles.Where(m => m.ParentId == user.Access).First();
                return View("_information", user);
            }
            Message.set_flash("Cập nhật Thất Bại", "danger");
            ViewBag.role = _context.Roles.Where(m => m.ParentId == user.Access).First();
            return View("information", user);
        }
    }
}