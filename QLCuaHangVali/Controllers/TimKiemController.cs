using QLCuaHangVali.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLCuaHangVali.Controllers
{
    public class TimKiemController : Controller
    {
        // GET: TimKiem
        ValiDBDataContext db = new ValiDBDataContext();
        public ActionResult KQTimKiem(string sTuKhoa)
        {
            var listSP = db.VALIs.Where(p => p.tenvali.Contains(sTuKhoa)).ToList();
            return View(listSP.OrderBy(p => p.tenvali));
        }

        public ActionResult Size()
        {
            ValiDBDataContext db = new ValiDBDataContext();
            var Size = db.SIZEVALIs.ToList();
            return PartialView(Size);
        }
        //tim kiem theo danh muc
        public ActionResult SizeTimKiem(int id)
        {
            ValiDBDataContext db = new ValiDBDataContext();
            var listSP = db.VALIs.Where(p => p.SIZEVALI.masize == id).ToList();
            return View(listSP.OrderBy(p => p.tenvali));
        }

        public ActionResult MauSac()
        {
            ValiDBDataContext db = new ValiDBDataContext();
            var mauSac = db.MAUSACs.ToList();
            return PartialView(mauSac);
        }
        //tim kiem theo mau sac
        public ActionResult MauSacTimKiem(int id)
        {
            ValiDBDataContext db = new ValiDBDataContext();
            var listSP = db.ANHVALIs.Where(p => p.MAUSAC.mamausac == id).ToList();
            return View(listSP.OrderBy(p => p.VALI.tenvali));
        }

    }
}