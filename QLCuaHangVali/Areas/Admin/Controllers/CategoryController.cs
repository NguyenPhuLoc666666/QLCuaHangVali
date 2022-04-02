using QLCuaHangVali.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLCuaHangVali.Areas.Admin.Controllers
{
    public class CategoryController : BaseController
    {
        // GET: Admin/Category

        ValiDBDataContext db = new ValiDBDataContext();
        public ActionResult Index()
        {
            var all_dmvl = from dmvl in db.DANHMUCVALIs select dmvl;
            return View(all_dmvl);
        }

        public ActionResult Details(int id)
        {
            DANHMUCVALI find = db.DANHMUCVALIs.FirstOrDefault(m => m.madanhmuc == id);
            if (find == null)
                return HttpNotFound();
            return View(find);
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(FormCollection collection, DANHMUCVALI dmvl)
        {
            var madanhmuc = collection["madanhmuc"];
            var tendanhmuc = collection["tendanhmuc"];
            var trangthai = collection["trangthai"];
            if (string.IsNullOrEmpty(tendanhmuc))
            {
                ViewData["Error"] = "Don't empty!";
            }
            else
            {
                dmvl.madanhmuc = int.Parse(madanhmuc.ToString());
                dmvl.tendanhmuc = tendanhmuc.ToString();
                dmvl.trangthai = Convert.ToBoolean(trangthai.ToString());

                db.DANHMUCVALIs.InsertOnSubmit(dmvl);
                db.SubmitChanges();
                return RedirectToAction("Index");
            }
            return this.Create();
        }

        public ActionResult Edit(int id)
        {
            DANHMUCVALI find = db.DANHMUCVALIs.FirstOrDefault(m => m.madanhmuc == id);
            if (find == null)
                return HttpNotFound();
            return View(find);
        }
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            var dmvl = db.DANHMUCVALIs.First(m => m.madanhmuc == id);
            var tendanhmuc = collection["tendanhmuc"];
            var trangthai = collection["trangthai"];

            dmvl.madanhmuc = id;
            if (string.IsNullOrEmpty(tendanhmuc))
            {
                ViewData["Error"] = "Don't empty!";
            }
            else
            {
                dmvl.tendanhmuc = tendanhmuc.ToString();
                dmvl.trangthai = Convert.ToBoolean(trangthai.ToString());

                UpdateModel(dmvl);
                db.SubmitChanges();
                return RedirectToAction("Index");
            }
            return this.Edit(id);
        }
        //-----------------------------------------
        public ActionResult Delete(int id)
        {
            DANHMUCVALI find = db.DANHMUCVALIs.FirstOrDefault(m => m.madanhmuc == id);
            if (find == null)
                return HttpNotFound();
            return View(find);
        }
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            var Del_dmvl = db.DANHMUCVALIs.Where(m => m.madanhmuc == id).First();
            db.DANHMUCVALIs.DeleteOnSubmit(Del_dmvl);
            db.SubmitChanges();
            return RedirectToAction("Index");
        }

    }
}