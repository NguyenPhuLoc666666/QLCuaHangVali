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
            //if (Session["UserAdmin"].Equals(""))
            //{
            //    System.Web.HttpContext.Current.Response.Redirect("Index", "Vali");
            //}
            //kiem tra dang nhap
            if (System.Web.HttpContext.Current.Session["UserAdmin"].Equals(""))
            {
                //chuyen huong website
                System.Web.HttpContext.Current.Response.Redirect("~/Admin/Login");
            }
          
            //Dang nhap thanh cong vo Admin
            //hien tai dang trang o vali
            //chuyen qua category thi ko can login lai
            // m muon qua category -> index

        }
    }
}