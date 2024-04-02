using MVCProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCProject.Controllers
{
    public class ModuleController : Controller
    {
        MojiDbContext _context = new MojiDbContext();
        // GET: Module
        public ActionResult MainMenu()
        {
            var list = _context.Menus.Where(m => m.Status == 1).
                Where(m => m.Parentid == 0 && m.Position == "mainmenu")
                .OrderBy(m => m.Orders);
            return View("main_menu", list.ToList());
        }
        public ActionResult SubMainMenu(int parentId)
        {
            ViewBag.mainmenuitem = _context.Menus.Find(parentId);
            var list = _context.Menus.Where(m => m.Status == 1).
                Where(m => m.Parentid == parentId && m.Position == "mainmenu")
                .OrderBy(m => m.Orders);
            if (list.Count() != 0)
            {
                return View("sub_menu_1", list);
            }
            else
            {
                return View("sub_menu_2", list);
            }

        }
        public ActionResult ListCategory()
        {
            var list = _context.Categories.Where(m => m.Status == 1).
               Where(m => m.Parentid == 0)
               .OrderBy(m => m.Orders);
            return View("list_category", list.ToList());
        }
        public ActionResult ListCategoryAll()
        {
            var list = _context.Categories.Where(m => m.Status == 1)
               .OrderBy(m => m.Orders);
            return View("list_category_all", list.ToList());
        }
        public ActionResult SubListCategory(int parentId)
        {
            ViewBag.mainmenuitem = _context.Categories.Find(parentId);
            var list = _context.Categories.Where(m => m.Status == 1).
                Where(m => m.Parentid == parentId)
                .OrderBy(m => m.Orders);
            if (list.Count() != 0)
            {
                return View("~/Views/Module/SubCategory/sub_category_1.cshtml", list);
            }
            else
            {
                return View("~/Views/Module/SubCategory/sub_category_2.cshtml", list);
            }
        }
        public ActionResult Slider()
        {
            var list = _context.Sliders
                .Where(m => m.Status == 1 && m.Position == "SliderShow").OrderBy(m => m.Orders).ToList();
            return View("slider", list);
        }
        public ActionResult Header()
        {
            if (Session["id"].Equals(""))
            {
                ViewBag.name = "";
            }
            else
            {
                ViewBag.name = Session["user"];
                ViewBag.id = int.Parse(Session["id"].ToString());
            }
            return PartialView("header_home");
        }
    }
}