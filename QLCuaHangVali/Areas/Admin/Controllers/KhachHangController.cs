using QLCuaHangVali.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLCuaHangVali.Areas.Admin.Controllers
{
    public class KhachHangController : Controller
    {
        // GET: Admin/KhachHang
        ValiDBDataContext db = new ValiDBDataContext();
        public ActionResult Index()
        {
            var all_kh = from kh in db.KHACHHANGs select kh;
            return View(all_kh);
        }

        public ActionResult Details(int id)
        {
            KHACHHANG find = db.KHACHHANGs.FirstOrDefault(m => m.makh == id);
            if (find == null)
                return HttpNotFound();
            return View(find);
        }

    }
}