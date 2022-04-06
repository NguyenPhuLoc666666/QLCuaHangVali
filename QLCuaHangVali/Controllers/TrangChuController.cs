using QLCuaHangVali.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLCuaHangVali.Controllers
{
    public class TrangChuController : Controller
    {
        // GET: TrangChu
        ValiDBDataContext db = new ValiDBDataContext();
        private List<VALI> Vailimoi(int count)
        {
            return db.VALIs.OrderByDescending(a => a.ngaytao).Take(count).ToList();
        }


        public ActionResult Index()
        {
            var vali = Vailimoi(3);
            return View(vali);
        }

        //Danh mục trên header
        public ActionResult DanhMuc()
        {
            var Danhmuc = from dm in db.DANHMUCVALIs select dm;
            return PartialView(Danhmuc);
        }

        public ActionResult DanhMucVali(int id)
        {
            var vali =  db.VALIs.Where(a => a.madanhmuc == id).ToList();
            //var vali = from s in db.VALIs where s.mavali == id select s;

            return View(vali);
        }

        public ActionResult ThuongHieu()
        {
            var ThuongHieu = from dm in db.THUONGHIEUs select dm;
            return PartialView(ThuongHieu);
        }

        // Trang tất cả vali
        public ActionResult PageVali()
        {
            var vali = Vailimoi(12);
            return View(vali);
        }
     
        [HttpGet]
        public ActionResult LienHe()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LienHe(FormCollection collection, LIENHE kh)
        {
            //Gan cac gia tri nguoi dung nhap lieu
            var tenkhachhang = collection["name"];
            var tieude = collection["subject"];
            var sdt = collection["sodienthoai"];
            var email = collection["email"];
            var noidung = collection["message"];
            if (String.IsNullOrEmpty(tenkhachhang))
            {
                //ViewData["Loi1"] = "Họ tên khách hàng không được để trống";
            }
            else if (String.IsNullOrEmpty(tieude))
            {
                //ViewData["Loi2"] = "Phải nhập tên đăng nhập";
            }
            else if (String.IsNullOrEmpty(sdt))
            {
                //ViewData["Loi3"] = "Phải nhập mật khẩu";
            }
            else if (String.IsNullOrEmpty(email))
            {
                //ViewData["Loi4"] = "Phải nhập lại mật khẩu";
            }
            else if (String.IsNullOrEmpty(noidung))
            {
                //ViewData["Loi5"] = "Email không được bỏ trống";
            }
            else
            {
                //Gan cac gia tri cho doi tuong duoc tao moi(kh)
                kh.tenkhachhang = tenkhachhang;
                kh.tieude = tieude;
                kh.sdt = sdt;
                kh.email = email;
                kh.noidung = noidung;
                //kh.ngaysinh = DateTime.Parse(ngaysinh);
                db.LIENHEs.InsertOnSubmit(kh);
                db.SubmitChanges();
                ViewBag.Thongbao = "";
                return View();
            }
            return View();
        }

        // Chiết tiế từng vali
        public ActionResult Details(int id)
        {
            var vali = from s in db.VALIs
                       where s.mavali == id
                       select s;
            return View(vali.Single());
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


        [HttpPost]
        public ActionResult GioHangVali(FormCollection collection)
        {
            List<GioHang> lstGiohang = Laygiohang();
            //Gan cac gia tri nguoi dung nhap lieu
            var Vali = int.Parse(collection["vali"]);
            GioHang sanpham = lstGiohang.Find(n => n.imavali == Vali);
            sanpham = new GioHang(Vali);
            lstGiohang.Add(sanpham);
            return View();
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
            //if (lstGiohang.Count == 0)
            //{
            //    return RedirectToAction("Index", "TrangChu");
            //}
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