using MVCProject.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using MVCProject.Library;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Net;
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

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact contact = db.Contacts.Find(id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            return View(contact);
        }

        public ActionResult Status(int id)
        {
            Contact contact = db.Contacts.Find(id);
            contact.Status = (contact.Status == 1) ? 2 : 1;
            contact.Updated_at = DateTime.Now;
            contact.Updated_by = int.Parse(Session["Admin_id"].ToString());
            db.Entry(contact).State = EntityState.Modified;
            db.SaveChanges();
            Message.set_flash("Thay đổi trang thái thành công", "success");
            return RedirectToAction("Index");
        }

        public ActionResult trash()
        {
            var list = db.Contacts.Where(m => m.Status == 0).ToList();
            return View("Trash", list);
        }

        public ActionResult Deltrash(int id)
        {
            Contact contact = db.Contacts.Find(id);
            contact.Status = 0;
            contact.Updated_at = DateTime.Now;
            contact.Updated_by = int.Parse(Session["Admin_id"].ToString());
            db.Entry(contact).State = EntityState.Modified;
            db.SaveChanges();
            Message.set_flash("Xóa thành công", "success");
            return RedirectToAction("Index");
        }

        public ActionResult Retrash(int id)
        {
            Contact contact = db.Contacts.Find(id);
            contact.Status = 2;
            contact.Updated_at = DateTime.Now;
            contact.Updated_by = int.Parse(Session["Admin_id"].ToString());
            db.Entry(contact).State = EntityState.Modified;
            db.SaveChanges();
            Message.set_flash("khôi phục thành công", "success");
            return RedirectToAction("trash");
        }

    }
}