using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLCuaHangVali.Areas.Admin.Controllers
{
    public class BaseController : Controller
    {
        // Code bẫy login
        public BaseController  ()
        {
            //kiem tra dang nhap
            if (System.Web.HttpContext.Current.Session["UserAdmin"].Equals(""))
            {
                //chuyen huong website
                System.Web.HttpContext.Current.Response.Redirect("~/Admin/Login");
            }
        }
    }
}