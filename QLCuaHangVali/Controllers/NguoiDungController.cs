﻿using QLCuaHangVali.Models;
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
            return PartialView();
        }
        [HttpPost]
        public ActionResult DangKy(FormCollection collection, KHACHHANG kh)
        {
            //Gan cac gia tri nguoi dung nhap lieu
            var hoten = collection["name"];
            var tendn = collection["username"];
            var dienthoai = collection["sodienthoai"];
            var matkhau = collection["password"];
            var matkhaunhaplai = collection["ConfirmPassword"];
            var email = collection["Email"];
            //var anhdaidien = collection["anhdaidien"];
            //var diachi = collection["diachi"];
            //var ngaysinh = String.Format("{0:MM/dd/yyyy}", collection["ngaysinh"]);
            if (String.IsNullOrEmpty(hoten))
            {
                ViewData["Loi1"] = "Họ tên khách hàng không được để trống";
            }
            else if (String.IsNullOrEmpty(tendn))
            {
                ViewData["Loi2"] = "Phải nhập tên đăng nhập";
            }
            else if (String.IsNullOrEmpty(matkhau))
            {
                ViewData["Loi3"] = "Phải nhập mật khẩu";
            }
            else if (String.IsNullOrEmpty(matkhaunhaplai))
            {
                ViewData["Loi4"] = "Phải nhập lại mật khẩu";
            }
            else if (String.IsNullOrEmpty(email))
            {
                ViewData["Loi5"] = "Email không được bỏ trống";
            }
            else if (String.IsNullOrEmpty(dienthoai))
            {
                ViewData["Loi6"] = "Phải nhập điện thoại";
            }
            else
            {
                //Gan cac gia tri cho doi tuong duoc tao moi(kh)
                kh.tenkhachhang = hoten;
                kh.taikhoankh = tendn;
                kh.matkhau = matkhau;
                kh.email = email;
                //kh.diachi = diachi;
                kh.sodienthoai = dienthoai;
                //kh.ngaysinh = DateTime.Parse(ngaysinh);
                data.KHACHHANGs.InsertOnSubmit(kh);
                data.SubmitChanges();
                ViewBag.Thongbao = "";
                return PartialView("DangNhap");
            }
            return PartialView();
        }

        //Chức năng đăng nhập của khách hàng 
        [HttpGet]
        public ActionResult DangNhap()
        {
            ViewBag.LoiDanhNhap = "";
            ViewBag.Thongbao = "";
            return PartialView();
        }
        [HttpPost]
        public ActionResult DangNhap(FormCollection collection)
        {

            var tendn = collection["username"];
            var matkhau = collection["password"];
            if (String.IsNullOrEmpty(tendn))
            {
                ViewData["Loi1"] = "Phải Nhập Tài khoản";
                return null;
            }
            else if (String.IsNullOrEmpty(matkhau))
            {
                ViewData["Loi2"] = "Phải nhập mật khẩu";
                return null;
            }
            else
            {
                // Gán giá trị cho đối tượng được tạo mới (ad)
                KHACHHANG ad = data.KHACHHANGs.SingleOrDefault(n => n.taikhoankh == tendn && n.matkhau == matkhau);
                if (ad != null)
                {
                    // ViewBag.Thongbao = " Chúc mừng đăng nhập thành công";
                    Session["TaiKhoanKH"] = ad.tenkhachhang;
                    Session["KhachHangDangNhap"] = ad;
                    ViewBag.Thongbao = "Tên đăng nhập hoặc mật khẩu không đúng"; // Cho nhập lại một lần nữa bởi bì lần 1 k load
                    return PartialView();
                }
                else
                {
                    ViewBag.Thongbao = "Tên đăng nhập hoặc mật khẩu không đúng";
                }
            }
            //ViewBag.Thongbao = ""; .............
            return PartialView();
        }

        public ActionResult DangXuat()
        {
            Session["TaiKhoanKH"] = "";
            return RedirectToAction("Index", "TrangChu");
        }



    }
}