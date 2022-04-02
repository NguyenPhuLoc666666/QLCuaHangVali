using System.Web.Mvc;

namespace QLCuaHangVali.Areas.Admin
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
            //login
            context.MapRoute(
                "Admin_login",
                "Admin/Login",
                new { Controller = "Login", action = "Login", id = UrlParameter.Optional }
            );

            //logout
            context.MapRoute(
                "Admin_logout",
                "Admin/Logout",
                new { Controller = "Login", action = "Logout", id = UrlParameter.Optional }
            );

            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { Controller="Dashboard", action = "Index", id = UrlParameter.Optional }
            );
            //Dashboard: bảng điều khiển - trang chủ
        }
    }
}