using MVCProject.Common;
using MVCProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCProject.Areas.Admin.Controllers
{
    public class CategoryController : BaseController
    {
        private MojiDbContext db = new MojiDbContext();

        //GET: Admin/Category
        [CustomAuthorizeAttribute(RoleID = "SALESMAN")]

        public ActionResult Index()
        {
            ViewBag.listCate = db.Categories.Where(m => m.Status != 0).ToList();
            var list = db.Categories.Where(m => m.Status > 0).ToList() ;
            return View(list);
        }
    }
}