using MVCProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCProject.Controllers
{
    public class SiteController : Controller
    {
        MojiDbContext _context = new MojiDbContext();
        // GET: Site
        public ActionResult Index(String slug = "")
        {
            int page = 1;
            if(!string.IsNullOrEmpty(Request.QueryString["page"]))
            {
                page = int.Parse(Request.QueryString["page"]);
            }
            if(slug == "")
            {
                return this.Home();
            }
            else
            {
                var rowLink = _context.Links.Where(m => m.Slug == slug);
                if(rowLink.Count() > 0 )
                {
                    var link = rowLink.First();
                    if(link.Type == "category" && link.TableId == 1 )
                    {
                        return this.productOfCategory(slug);
                    }
                }
                else
                {
                    var product = _context.Products.Where(m => m.Status == 1 && m.Slug == slug).FirstOrDefault();
                    if (product != null)
                    {
                        return this.ProductDetail(slug);
                    }
                    else
                    {
                        return this.page404();
                    }
                }
                return this.page404();
            }
        }

        public ActionResult Home()
        {
            var list = _context.Categories.Where(m => m.Status == 1).
                Where(m => m.Parentid == 0).
                OrderBy(m => m.Orders);
            return View("Index", list);
        }
        private ActionResult ProductDetail(String slug)
        {
            var list = _context.Products.Where(m => m.Status == 1 && m.Slug == slug).First();
            return View("product_detail", list);
        }
        public ActionResult productOfCategory(String slug)
        {
            var catid = _context.Categories.Where(m => m.Slug == slug).First();
            return View("category", catid);
        }
        public ActionResult page404()
        {
            return View("");
        }
    }
}