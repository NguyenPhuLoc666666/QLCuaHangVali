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
        public List<GioHang> Laygiohang()
        {
            List<GioHang> lstGiohang = Session["Giohang"] as List<GioHang>;
            if (lstGiohang == null)
            {
                // Nếu giỏi hàng chưa tồn tại thì khởi tạo listGiohang
                lstGiohang = new List<GioHang>();
                Session["Giohang"] = lstGiohang;
            }
            return lstGiohang;
        }

        public ActionResult ThemGiohang(String Id, int Quantity)
        {
            int id = int.Parse(Id);
            // Lấy ra Session giao hàng 
            List<GioHang> lstGiohang = Laygiohang();
            // Kiểm tra vali này tồn tại trong Session["Giohang"] chưa?
            GioHang sanpham = lstGiohang.Find(n => n.imavali == id);
            //Session["Count"] = 1;
            if (sanpham == null)
            {
                sanpham = new GioHang(id);
                sanpham.isoluong = Quantity;
                lstGiohang.Add(sanpham);
                Session["count"] = Convert.ToInt32(Session["count"]) + 1;
                //return Redirect(strURL);
            }
            else
            {
                sanpham.isoluong = Quantity + sanpham.isoluong;
                //return Redirect(strURL);
            }
            return Json(new { Message = "Thành công", JsonRequestBehavior.AllowGet });
        }

        private int TongSoLuong()
        {
            int iTongSoLuong = 0;
            List<GioHang> lstGiohang = Session["Giohang"] as List<GioHang>;
            if (lstGiohang != null)
            {
                iTongSoLuong = lstGiohang.Sum(n => n.isoluong);
            }
            return iTongSoLuong;
        }

        // Tính tổng tiền 
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

        // Xây dựng trang Giỏ hàng
        public ActionResult Giohang()
        {
            List<GioHang> lstGiohang = Laygiohang();
            if (lstGiohang.Count == 0)
            {
                return RedirectToAction("Index", "TrangChu");
            }
            ViewBag.Tonsoluong = TongSoLuong();
            ViewBag.Tongtien = TongTien();
            return View(lstGiohang);
        }

        public ActionResult GiohangPartial()
        {
            ViewBag.Tongsoluong = TongSoLuong();
            ViewBag.Tongtien = TongTien();
            return PartialView();
        }

        // xóa giỏ hàng
        public ActionResult XoaGiohang(int imavali)
        {
            // lấy giỏ hàng từ Seccsion
            List<GioHang> lstGiohang = Laygiohang();
            // Kiểm tra sách đã có trong Session["Giohang"]
            GioHang sanpham = lstGiohang.SingleOrDefault(n => n.imavali == imavali);
            // nếu tồn tại thì cho sửa số lượng
            if (sanpham != null)
            {
                lstGiohang.RemoveAll(n => n.imavali == imavali);
                return RedirectToAction("GioHang");
            }

            if (lstGiohang.Count == 0)
            {
                return RedirectToAction("Index", "TrangChu");
            }

            return RedirectToAction("GioHang");
        }

        public ActionResult CapnhatGiohang(int iMaSP, FormCollection f)
        {
            // lấy giỏ hàng từ Session
            List<GioHang> lstGiohang = Laygiohang();
            // Kiểm tra sách có trong Session["Giohang]
            GioHang sanpham = lstGiohang.SingleOrDefault(n => n.imavali == iMaSP);
            // nếu tồn tại thì cho sử số lượng
            if (sanpham != null)
            {
                sanpham.isoluong = int.Parse(f["txtSoluong"].ToString());
            }
            return RedirectToAction("Giohang");
        }

        public ActionResult Xoatatcagiohang()
        {
            // lấy giỏ hàng từ Session
            List<GioHang> lstGiohang = Laygiohang();
            lstGiohang.Clear();
            return RedirectToAction("Index", "TrangChu");
        }
    }
}