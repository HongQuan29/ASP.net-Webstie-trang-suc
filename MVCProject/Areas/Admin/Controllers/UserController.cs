using MVCProject.Common;
using MVCProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}