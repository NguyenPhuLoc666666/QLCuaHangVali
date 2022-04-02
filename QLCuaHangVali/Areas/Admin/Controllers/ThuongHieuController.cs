using QLCuaHangVali.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLCuaHangVali.Areas.Admin.Controllers
{
    public class ThuongHieuController : BaseController
    {
        // GET: Admin/ThuongHieu
        ValiDBDataContext db = new ValiDBDataContext();
        // GET: Vali
        public ActionResult Index()
        {
            var all_th = from th in db.THUONGHIEUs select th;
            return View(all_th);
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(FormCollection collection, THUONGHIEU th)
        {
  
            var tenthuonghieu = collection["tenthuonghieu"];
            
            var mota = collection["mota"];
            

            if (string.IsNullOrEmpty(tenthuonghieu))
            {
                ViewData["Error"] = "Don't empty!";
            }
            else
            {
               
                th.tenthuonghieu = tenthuonghieu.ToString();
               
                th.mota = (mota.ToString());
                
                db.THUONGHIEUs.InsertOnSubmit(th);
                db.SubmitChanges();
                return RedirectToAction("Index");
            }
            return this.Create();
        }

        public ActionResult Edit(int id)
        {
            THUONGHIEU find = db.THUONGHIEUs.FirstOrDefault(m => m.mathuonghieu == id);
            if (find == null)
                return HttpNotFound();
            return View(find);
        }
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            var th = db.THUONGHIEUs.First(m => m.mathuonghieu == id);
            var tenthuonghieu = collection["tenthuonghieu"];
       
            var mota = collection["mota"];
            
            th.mathuonghieu = id;
            if (string.IsNullOrEmpty(tenthuonghieu))
            {
                ViewData["Error"] = "Don't empty!";
            }
            else
            {
                th.tenthuonghieu = tenthuonghieu.ToString();
               
                th.mota = (mota.ToString());
                
                UpdateModel(th);
                db.SubmitChanges();
                return RedirectToAction("Index");
            }
            return this.Edit(id);
        }
        //-----------------------------------------
        public ActionResult Delete(int id)
        {
            THUONGHIEU find = db.THUONGHIEUs.FirstOrDefault(m => m.mathuonghieu == id);
            if (find == null)
                return HttpNotFound();
            return View(find);
        }
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            var Del_th = db.THUONGHIEUs.Where(m => m.mathuonghieu == id).First();
            db.THUONGHIEUs.DeleteOnSubmit(Del_th);
            db.SubmitChanges();
            return RedirectToAction("Index");
        }
    }
}