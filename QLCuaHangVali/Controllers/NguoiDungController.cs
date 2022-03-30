using QLCuaHangVali.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLCuaHangVali.Controllers
{
    public class NguoiDungController : Controller
    {
        // GET: NguoiDung
        ValiDBDataContext data = new ValiDBDataContext();
        [HttpGet]
        public ActionResult DangKy()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DangKy(FormCollection collection, KHACHHANG kh)
        {
            var tenKhachHang = collection["tenkhachhang"];
            var soDienThoai = collection["sodienthoai"];
            var email = collection["email"];
            var diaChi = collection["diachi"];
            var taiKhoanKH = collection["taikhoankh"];
            var matKhau = collection["matkhau"];
            var MatKhauXacNhan = collection["MatKhauXacNhan"];
            var ngaySinh = String.Format("{0:MM/dd/yyyy}", collection["ngaysinh"]);
            if (String.IsNullOrEmpty(MatKhauXacNhan))
            {
                ViewData["NhapMKXN"] = "Phải nhập mật khẩu xác nhận!";

            }
            else
            {
                if (!matKhau.Equals(MatKhauXacNhan))
                {
                    ViewData["MatKhauGiongNhau"] = "Mật khẩu và mật khẩu xác nhận phải giống nhau";
                }
                else
                {
                    kh.tenkhachhang = tenKhachHang;
                    kh.sodienthoai = soDienThoai;
                    kh.email = email;
                    kh.diachi = diaChi;
                    kh.taikhoankh = taiKhoanKH;
                    kh.matkhau = matKhau;              
                    kh.ngaysinh = DateTime.Parse(ngaySinh);
                    data.KHACHHANGs.InsertOnSubmit(kh);
                    data.SubmitChanges();

                    return RedirectToAction("Index", "Vali");
                }
            }
            return this.DangKy();
        }

        //dang nhap

    }
}