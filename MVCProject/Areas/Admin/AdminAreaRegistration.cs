using System.Web.Mvc;

namespace MVCProject.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "AdminLogin",
                "Admin/login",
                new { Controller = "Auth", action = "login", id = UrlParameter.Optional }
            );
     
            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new {Controller = "Product", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}