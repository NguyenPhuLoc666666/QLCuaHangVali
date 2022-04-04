using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLCuaHangVali.Controllers
{
    public class BaseUserController : Controller
    {
        // GET: BaseUser
        public BaseUserController()
        {
            if (System.Web.HttpContext.Current.Session["TaiKhoanKH"].Equals(""))
            {
                System.Web.HttpContext.Current.Response.Redirect("~/TrangChu/Index");
            }
        }
    }
}