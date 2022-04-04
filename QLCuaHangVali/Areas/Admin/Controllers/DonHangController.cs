using PagedList;
using QLCuaHangVali.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLCuaHangVali.Areas.Admin.Controllers
{
    public class DonHangController : Controller
    {
        // GET: Admin/DonHang
        ValiDBDataContext db = new ValiDBDataContext();
        public ActionResult Index(int? page)
        {
            if (page == null) page = 1;
            var all_vl = (from vl in db.DONDATHANGs select vl).OrderBy(m => m.madonhang);
            int pageSize = 6;
            int pageNum = page ?? 1;
            return View(all_vl.ToPagedList(pageNum, pageSize));
        }

        public ActionResult Details(int id)
        {
            CHITIETDONHANG find = db.CHITIETDONHANGs.FirstOrDefault(m => m.madonhang == id);
            if (find == null)
                return HttpNotFound();
            return View(find);
        }

         
         

        public ActionResult Edit(int id)
        {
            DONDATHANG find = db.DONDATHANGs.FirstOrDefault(m => m.madonhang == id);
            if (find == null)
                return HttpNotFound();
            return View(find);
        }
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            var kh = db.DONDATHANGs.First(m => m.madonhang == id);
            var makh = collection["makh"];
            var mathanhtoan = collection["mathanhtoan"];
            var ngaydat = collection["ngaydat"];
            var ngaygiao = collection["ngaygiao"];
            var tinhtrang = collection["tinhtrang"];
            var ghichu = collection["ghichu"];

            kh.makh = id;
            if (string.IsNullOrEmpty(makh))
            {
                ViewData["Error"] = "Don't empty!";
            }
            else
            {
                kh.makh = int.Parse(makh.ToString());
                kh.mathanhtoan = int.Parse(mathanhtoan.ToString());
                kh.ngaydat = DateTime.Parse(ngaydat.ToString());
                kh.ngaygiao = DateTime.Parse(ngaygiao.ToString());
                kh.tinhtrang =  (tinhtrang.ToString());
                kh.ghichu = ghichu.ToString();

                UpdateModel(kh);
                db.SubmitChanges();
                return RedirectToAction("Index");
            }
            return this.Edit(id);
        }
        //-----------------------------------------
        public ActionResult Delete(int id)
        {
            DONDATHANG find = db.DONDATHANGs.FirstOrDefault(m => m.madonhang == id);
            if (find == null)
                return HttpNotFound();
            return View(find);
        }
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            var Del_kh = db.DONDATHANGs.Where(m => m.madonhang == id).First();
            db.DONDATHANGs.DeleteOnSubmit(Del_kh);
            db.SubmitChanges();
            return RedirectToAction("Index");
        }
    }
}