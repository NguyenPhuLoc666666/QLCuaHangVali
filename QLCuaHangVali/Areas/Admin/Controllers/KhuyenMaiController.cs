using PagedList;
using QLCuaHangVali.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLCuaHangVali.Areas.Admin.Controllers
{
    public class KhuyenMaiController : Controller
    {
        // GET: Admin/ThuongHieu
        ValiDBDataContext db = new ValiDBDataContext();
        // GET: Vali
        public ActionResult Index(int? page)
        {
            if (page == null) page = 1;
            var all_vl = (from vl in db.PHIEUKHUYENMAIs select vl).OrderBy(m => m.makhuyenmai);
            int pageSize = 6;
            int pageNum = page ?? 1;
            return View(all_vl.ToPagedList(pageNum, pageSize));
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(FormCollection collection, PHIEUKHUYENMAI km)
        {

            var tenkhuyenmai = collection["tenkhuyenmai"];
            var phantram = collection["phantram"];
            var tienkhuyenmai = Convert.ToDecimal(collection["tienkhuyenmai"]);
            var ngaybatdau = Convert.ToDateTime(collection["ngaybatdau"]);
            var ngayketthuc = Convert.ToDateTime(collection["ngayketthuc"]);



            if (string.IsNullOrEmpty(tenkhuyenmai))
            {
                ViewData["Error"] = "Don't empty!";
            }
            else
            {

                km.tenkhuyenmai = tenkhuyenmai.ToString();
                km.phantram = phantram.ToString();
                km.tienkhuyenmai = tienkhuyenmai;
                km.ngaybatdau = ngaybatdau;
                km.ngayketthuc = ngayketthuc;

                db.PHIEUKHUYENMAIs.InsertOnSubmit(km);
                db.SubmitChanges();
                return RedirectToAction("Index");
            }
            return this.Create();
        }

        public ActionResult Edit(int id)
        {
            PHIEUKHUYENMAI find = db.PHIEUKHUYENMAIs.FirstOrDefault(m => m.makhuyenmai == id);
            if (find == null)
                return HttpNotFound();
            return View(find);
        }
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            var km = db.PHIEUKHUYENMAIs.First(m => m.makhuyenmai == id);
            var tenkhuyenmai = collection["tenkhuyenmai"];
            var phantram = collection["phantram"];
            var tienkhuyenmai = Convert.ToDecimal(collection["tienkhuyenmai"]);
            var ngaybatdau = Convert.ToDateTime(collection["ngaybatdau"]);
            var ngayketthuc = Convert.ToDateTime(collection["ngayketthuc"]);

            km.makhuyenmai = id;
            if (string.IsNullOrEmpty(tenkhuyenmai))
            {
                ViewData["Error"] = "Don't empty!";
            }
            else
            {
                km.tenkhuyenmai = tenkhuyenmai.ToString();
                km.phantram = phantram.ToString();
                km.tienkhuyenmai = tienkhuyenmai;
                km.ngaybatdau = ngaybatdau;
                km.ngayketthuc = ngayketthuc;

                UpdateModel(km);
                db.SubmitChanges();
                return RedirectToAction("Index");
            }
            return this.Edit(id);
        }
        //-----------------------------------------
        public ActionResult Delete(int id)
        {
            PHIEUKHUYENMAI find = db.PHIEUKHUYENMAIs.FirstOrDefault(m => m.makhuyenmai == id);
            if (find == null)
                return HttpNotFound();
            return View(find);
        }
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            var Del_th = db.PHIEUKHUYENMAIs.Where(m => m.makhuyenmai == id).First();
            db.PHIEUKHUYENMAIs.DeleteOnSubmit(Del_th);
            db.SubmitChanges();
            return RedirectToAction("Index");
        }
    }
}