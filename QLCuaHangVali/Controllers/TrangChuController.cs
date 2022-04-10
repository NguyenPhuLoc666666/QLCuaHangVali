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
    }
}