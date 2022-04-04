using QLCuaHangVali.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLCuaHangVali.Controllers
{
    public class TrangChuController : Controller
    {
        // GET: TrangChu
        ValiDBDataContext db = new ValiDBDataContext();
        private List<VALI> Vailimoi(int count)
        {
            return db.VALIs.OrderByDescending(a => a.ngaytao).Take(count).ToList();
        }

        public ActionResult Index()
        {
            var sachmoi = Vailimoi(3);
            return View(sachmoi);
        }

        public ActionResult DanhMuc()
        {
            var Danhmuc = from dm in db.DANHMUCVALIs select dm;
            return PartialView(Danhmuc);
        }

        public ActionResult ThuongHieu()
        {
            var ThuongHieu = from dm in db.THUONGHIEUs select dm;
            return PartialView(ThuongHieu);
        }
        
    }
}