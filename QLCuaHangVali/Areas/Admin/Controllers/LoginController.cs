using QLCuaHangVali.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLCuaHangVali.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        // GET: Admin/Login
        ValiDBDataContext db = new ValiDBDataContext();
        public ActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public ActionResult Login()
        {

            //if (Session["UserAdmin"].Equals(""))
            //{
            //    return RedirectToAction("Index", "Vali");
            //}
            ViewBag.Loi = "";
            return View();
        }
        [HttpPost]
        public ActionResult Login(FormCollection collection)
        {
           
            var tendn = collection["username"];
            var matkhau = collection["password"];
            if (String.IsNullOrEmpty(tendn))
            {
                ViewData["Loi1"] = "Phải nhập tên đăng nhâp";
            }
            else if (String.IsNullOrEmpty(matkhau))
            {
                ViewData["Loi2"] = "Phải nhập mật khẩu";
            }
            else
            {
                // Gán giá trị cho đối tượng được tạo mới (ad)
                ADMIN ad = db.ADMINs.SingleOrDefault(n => n.taikhoan == tendn && n.matkhau == matkhau);
                if (ad != null)
                {
                    // ViewBag.Thongbao = " Chúc mừng đăng nhập thành công";
                    Session["UserAdmin"] = ad.taikhoan;
                    return RedirectToAction("Index", "Vali");
                }
                else
                {
                    ViewBag.Thongbao = "Tên đăng nhập hoặc mật khẩu không đúng";
                }
            }
            return View();
        }

        //logout
        public ActionResult Logout()
        {
            Session["UserAdmin"] = "";
            return RedirectToAction("Login","Login");
        }
    }
}