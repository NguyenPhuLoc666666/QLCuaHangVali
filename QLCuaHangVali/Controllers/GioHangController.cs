using QLCuaHangVali.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace QLCuaHangVali.Controllers
{
    public class GioHangController : BaseUserController
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
            VALI validb = db.VALIs.FirstOrDefault(n => n.mavali == id);
            validb.soluongton = validb.soluongton - Quantity;
            UpdateModel(validb);
            db.SubmitChanges();
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
            Session["TongTienGioHang"] = TongTien();
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
                Session["count"] = lstGiohang.Count - 1;
                return RedirectToAction("GioHang");
            }

            if (lstGiohang.Count == 0)
            {
                return RedirectToAction("Index", "TrangChu");
            }

            return RedirectToAction("GioHang");
        }

        public ActionResult CapnhatGiohang(String Name,  int Quantity)
        {
            // lấy giỏ hàng từ Session
            List<GioHang> lstGiohang = Laygiohang();
            // Kiểm tra sách có trong Session["Giohang]
            GioHang sanpham = lstGiohang.SingleOrDefault(n => n.itenvali == Name);
            // nếu tồn tại thì cho sử số lượng
            if (sanpham != null)
            {
                sanpham.isoluong = Quantity;
            }
            //return RedirectToAction("Giohang");
            return Json(new { Message = "Thành công", JsonRequestBehavior.AllowGet }); 
        }

        public ActionResult Xoatatcagiohang()
        {
            // lấy giỏ hàng từ Session
            List<GioHang> lstGiohang = Laygiohang();
            Session["count"] = "0";
            lstGiohang.Clear();
            return RedirectToAction("Index", "TrangChu");
        }

        [HttpGet]
        public ActionResult DatHang()
        {
            //Kiem tra dang nhap
            if (Session["KhachHangDangNhap"] == null || Session["KhachHangDangNhap"].ToString() == "")
            {
                return RedirectToAction("Index", "TrangChu");
            }
            if (Session["Giohang"] == null)
            {
                return RedirectToAction("Index", "TrangChu");
            }

            //Lay gio hang tu Session
            List<GioHang> lstGiohang = Laygiohang();
            //ViewBag.Tongsoluong = TongSoLuong();
            //ViewBag.Tongtien = TongTien();
            return View(lstGiohang);
        }
        [HttpPost]
        public ActionResult DatHang(FormCollection collection)
        {
            //Them Don hang
            DONDATHANG ddh = new DONDATHANG();
            KHACHHANG kh = (KHACHHANG)Session["KhachHangDangNhap"];
            List<GioHang> gh = Laygiohang();
            ddh.makh = kh.makh;
            ddh.ngaydat = DateTime.Now;
            var ngaygiao = String.Format("{0:mm/dd/yyyy}", collection["Ngaygiao"]);
            //ddh.ngaygiao = DateTime.Now.AddDays(5);
            ddh.ngaygiao = DateTime.Parse(ngaygiao);
            ddh.tinhtrang = false;
            //ddh.mathanhtoan = false;
            db.DONDATHANGs.InsertOnSubmit(ddh);
            db.SubmitChanges();
            //Them chi tiet don hang
            foreach (var item in gh)
            {
                CHITIETDONHANG ctdh = new CHITIETDONHANG();
                ctdh.madonhang = ddh.madonhang;
                ctdh.mavali = item.imavali;
                ctdh.soluong = item.isoluong;
                ctdh.gia = Convert.ToInt32(item.dThanhtien);
                db.CHITIETDONHANGs.InsertOnSubmit(ctdh);
            }
            db.SubmitChanges();
            Session["Giohang"] = null;
            return RedirectToAction("Xacnhandonhang", "GioHang");
        }

        // Xác nhận đơn hàng
        public ActionResult Xacnhandonhang()
        {
            //Them Don hang
            DONDATHANG ddh = new DONDATHANG();
            KHACHHANG kh = (KHACHHANG)Session["KhachHangDangNhap"];
            List<GioHang> gh = Laygiohang();
            ddh.makh = kh.makh;
            ddh.ngaydat = DateTime.Now;
            ddh.ngaygiao = DateTime.Now.AddDays(5);
            ddh.tinhtrang = false;
            //ddh.mathanhtoan = false;
            db.DONDATHANGs.InsertOnSubmit(ddh);
            db.SubmitChanges();
            //Them chi tiet don hang
            foreach (var item in gh)
            {
                CHITIETDONHANG ctdh = new CHITIETDONHANG();
                ctdh.madonhang = ddh.madonhang;
                ctdh.mavali = item.imavali;
                ctdh.soluong = item.isoluong;
                ctdh.gia = Convert.ToInt32(item.dDongia);
                db.CHITIETDONHANGs.InsertOnSubmit(ctdh);
            }
            db.SubmitChanges();
            Session["Giohang"] = null;
            Session["count"] = "0";

            try
            {
                var senderemail = new MailAddress("01642027120q@gmail.com", "SHOP MIAAA");
                var receiveremail = new MailAddress(kh.email, "Reciver");


                var sub = "Xác Nhận Đơn Hàng";

                var body = "Tên khách hàng: " + kh.tenkhachhang;
                body += "\nNgày đặt: " + ddh.ngaydat;
                body += "\nNgày giao: " + ddh.ngaygiao;
                body += "\nTrân trọng cảm ơn quý khách hàng đã đến và lựa chọn sử dụng dịch vụ của chúng tôi.  ";
                body += "\nXin gửi lời cảm ơn chân thành và sâu sắc nhất đến quý khách hàng.";



                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(senderemail.Address, "AnhQuan23092001")
                };
                using (var mess = new MailMessage(senderemail, receiveremail)
                {
                    Subject = sub,
                    Body = body
                })
                {
                    smtp.Send(mess);
                }


            }
            catch (Exception)
            {
                ViewBag.Error = "Some Error";
            }

            return View();
        }
    }
}