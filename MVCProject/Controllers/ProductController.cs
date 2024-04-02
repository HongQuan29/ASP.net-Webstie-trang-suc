using MVCProject.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace MVCProject.Controllers
{
    public class ProductController : Controller
    {
        MojiDbContext _context = new MojiDbContext();
        // GET: Product
        public ActionResult Index(int? page)
        {
            var list = _context.Products.Where(m => m.Status == 1).OrderBy(m => m.ID);
            if (page == null) page = 1;
            int pageSize = 8;
            int pageNumber = (page ?? 1);
            ViewBag.page = page;
            return View(list.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult ProductHome(int id)
        {
            var list = _context.Products.Where(m => m.Status == 1).
                Where(m => m.CatId == id || m.Submenu == id).OrderBy(m => m.ID).OrderBy(m => m.ID).Take(8);
            return View("product_home", list);
        }

        public ActionResult NewProduct()
        {
            var list = _context.Products.Where(m => m.Status == 1).
                OrderByDescending(m => m.ID).Take(8);
            return View("product_home", list);
        }

        public ActionResult BestSellerProduct()
        {
            var list = _context.Products.Where(m => m.Status == 1).
                OrderByDescending(m => m.Sold).Take(8);
            return View("product_home", list);
        }

        public ActionResult SaleProduct()
        {
            var list = _context.Products.Where(m => m.Status == 1).
                Where(m => m.Pricesale > 0).OrderBy(m => m.ID).Take(8);
            return View("sale_product", list);
        }

        public ActionResult Category(String slug)
        {
            var catId = _context.Categories.Where(m => m.Slug == slug).First();
            return View("category", catId);
        }

        public ActionResult Detail(String slug)
        {
            var item = _context.Products.Where(m => m.Status == 1 && m.Slug == slug).First();
            return View(item);
        }
        public ActionResult SameKind(int catid)
        {
            var list = _context.Products.Where(m => m.CatId == catid && m.Status == 1);
            return View("same_kind", list.ToList().Take(6));
        }
        public ActionResult SubCategory(int catid, string slug, int? page)
        {
            var list = _context.Products.Where(m => m.CatId == catid || m.Submenu == catid && m.Status == 1).OrderBy(m => m.ID);
            if (page == null) page = 1;
            int pageSize = 8;
            int pageNumber = (page ?? 1);
            ViewBag.page = page;
            ViewBag.slug = slug;
            return View("sub_category", list.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult SearchProduct(string keyw, int? page)
        {
            @ViewBag.keyw = keyw;
            if (page == null) page = 1;
            int pageSize = 8;
            int pageNumber = (page ?? 1);
            var list = _context.Products.Where(m => m.Status == 1 && m.Name.Contains(keyw)).OrderBy(m => m.ID);
            return View("search_product", list.ToPagedList(pageNumber, pageSize));
        }
    }
}