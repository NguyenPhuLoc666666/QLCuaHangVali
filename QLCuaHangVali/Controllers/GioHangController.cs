using QLCuaHangVali.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLCuaHangVali.Controllers
{
    public class GioHangController : Controller
    {
        // GET: GioHang
        ValiDBDataContext db = new ValiDBDataContext();
        public ActionResult Index()
        {
            return View();
        }
        public List<GioHang> Laygiohang()
        {
            List<GioHang> lstGiohang = Session["GioHang"] as List<GioHang>;
            if (lstGiohang == null)
            {
                lstGiohang = new List<GioHang>();
                Session["GioHang"] = lstGiohang;
            }
            return lstGiohang;
        }
        public ActionResult ThemGiohang(int imavali, string strURL)
        {
            //Lay ra Session gio hang
            List<GioHang> lstGiohang = Laygiohang();
            //Kiem tra sách này tồn tại trong Session["Giohang"] chưa?
            GioHang sanpham = lstGiohang.Find(n => n.imavali == imavali);
            if (sanpham == null)
            {
                sanpham = new GioHang(imavali);
                lstGiohang.Add(sanpham);
                //if (sanpham.soluongton < sanpham.isoluong)
                //{
                //    return View("ThongBao");
                //}
                return Redirect(strURL);
            }
            else
            {
                //if (sanpham.soluongton < sanpham.isoluong)
                //{
                //    return View("ThongBao");
                //}
                sanpham.isoluong++;
                return Redirect(strURL);
            }
        }
        //Tong so luong
        private int TongSoLuong()
        {
            int iTongSoLuong = 0;
            List<GioHang> lstGiohang = Session["GioHang"] as List<GioHang>;
            if (lstGiohang != null)
            {
                iTongSoLuong = lstGiohang.Sum(n => n.isoluong);
            }
            return iTongSoLuong;
        }
        //Tinh tong tien
        private double TongTien()
        {
            double iTongTien = 0;
            List<GioHang> lstGiohang = Session["GioHang"] as List<GioHang>;
            if (lstGiohang != null)
            {
                iTongTien = lstGiohang.Sum(n => n.dThanhtien);
            }
            return iTongTien;
        }
        //Trang Gio hang
        public ActionResult GioHang()
        {
            List<GioHang> lstGiohang = Laygiohang();
            if (lstGiohang.Count == 0)
            {
                return RedirectToAction("Index", "TrangChu");
            }
            ViewBag.Tongsoluong = TongSoLuong();
            ViewBag.Tongtien = TongTien();
            ViewBag.Message = Session["Message"];
            Session.Remove("Message");
            return View(lstGiohang);
        }
        //Tao Partial view de hien thi thong tin gio hang
        public ActionResult GiohangPartial()
        {
            ViewBag.Tongsoluong = TongSoLuong();
            ViewBag.Tongtien = TongTien();
            return PartialView();
        }
        //Xoa Giohang
        public ActionResult XoaGiohang(int mavali)
        {
            // Lay gio hang tu Session
            List<GioHang> lstGiohang = Laygiohang();
            //Kiem tra sach da co trong Session["GioHang"]
            GioHang sanpham = lstGiohang.SingleOrDefault(n => n.imavali == mavali);
            //Neu ton tai thi cho sua Soluong
            if (sanpham != null)
            {
                lstGiohang.RemoveAll(n => n.imavali == mavali);
                return RedirectToAction("GioHang");
            }
            if (lstGiohang.Count == 0)
            {
                return RedirectToAction("Index", "TrangChu");

            }
            return RedirectToAction("GioHang");
        }
        public ActionResult CapnhatGiohang(int mavali, FormCollection f)
        {
            //Lay gio hang tu Session
            List<GioHang> lstGiohang = Laygiohang();
            //Kiem tra sach da co trong Session["Giohang"]
            GioHang sanpham = lstGiohang.SingleOrDefault(n => n.imavali == mavali);
            //Neu ton tai thi cho sua Soluong
            if (sanpham != null)
            {
                sanpham.isoluong = int.Parse(f["txtSoluong"].ToString());
            }
            return RedirectToAction("GioHang");
        }
        public ActionResult XoaTatcaGiohang()
        {
            //Lay gio hang tu Session
            List<GioHang> lstGiohang = Laygiohang();
            lstGiohang.Clear();
            return RedirectToAction("Index", "TrangChu");
        }
    }
}