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
            List<MAUSAC> mausacdangco = new List<MAUSAC>();
            var mauSac = db.MAUSACs.ToList();
            var anhvali = db.ANHVALIs.ToList();

            foreach(var ms in mauSac)
            {
                int stt = 0;
                foreach (var anh in anhvali)
                {
                    if (anh.mamausac == ms.mamausac)
                    {
                        stt++;
                        if (stt == 1)
                        {
                            mausacdangco.Add(ms);
                        }
                    }
                }
            }
            return PartialView(mausacdangco);
        }
        //tim kiem theo mau sac
        public ActionResult MauSacTimKiem(int id)
        {
            ValiDBDataContext db = new ValiDBDataContext();
            var listSP = db.ANHVALIs.Where(p => p.MAUSAC.mamausac == id).ToList();
            return View(listSP.OrderBy(p => p.VALI.tenvali));
        }

        // Tìm kiếm theo thương hiệu 
        public ActionResult ThuongHieu()
        {
            List<THUONGHIEU> thuonghieudangco = new List<THUONGHIEU>();
            var thuonghieu = db.THUONGHIEUs.ToList();
            var vali = db.VALIs.ToList();

            foreach (var th in thuonghieu)
            {
                int stt = 0;
                foreach (var vl in vali)
                {
                    if (th.mathuonghieu == vl.mathuonghieu)
                    {
                        stt++;
                        if (stt == 1)
                        {
                            thuonghieudangco.Add(th);
                        }
                    }
                }
            }
            return PartialView(thuonghieudangco);
        }

        public ActionResult TimKiemThuongHieu(int id)
        {
            ValiDBDataContext db = new ValiDBDataContext();
            var listSP = db.VALIs.Where(p => p.THUONGHIEU.mathuonghieu == id).ToList();
            return View(listSP.OrderBy(p => p.tenvali));
        }
    }
}