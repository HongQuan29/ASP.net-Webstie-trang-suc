using MVCProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCProject.Areas.Admin.Controllers
{
    public class ContactController : Controller
    {
        private MojiDbContext db = new MojiDbContext();

        // GET: Admin/Contact

        public ActionResult Index()
        {
            var list = db.Contacts.Where(m => m.Status > 0).ToList();
            return View(list);
        }
    }
}