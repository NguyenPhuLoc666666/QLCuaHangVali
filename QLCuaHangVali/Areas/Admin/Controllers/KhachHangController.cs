using PagedList;
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
        public ActionResult Index(int? page)
        {
            if (page == null) page = 1;
            var all_vl = (from vl in db.KHACHHANGs select vl).OrderBy(m => m.makh);
            int pageSize = 6;
            int pageNum = page ?? 1;
            return View(all_vl.ToPagedList(pageNum, pageSize));
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