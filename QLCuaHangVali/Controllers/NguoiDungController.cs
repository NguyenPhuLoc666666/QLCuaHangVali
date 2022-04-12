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
                    Session["MaKH"] = ad.makh;
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
        //tai khoan khach hang
        public ActionResult EditInfor(int id)
        {
            KHACHHANG find = data.KHACHHANGs.FirstOrDefault(m => m.makh == id);
            if (find == null)
                return HttpNotFound();

            return View(find);
        }
        [HttpPost]
        public ActionResult EditInfor(int id, FormCollection collection)
        {
            //id = Convert.ToInt32(Session["MaKH"]);
            var th = data.KHACHHANGs.First(m => m.makh == id);
            var tenkhachhang = collection["tenkhachhang"];
            var sodienthoai = collection["sodienthoai"];
            var email = collection["email"];
            var diachi = collection["diachi"];
            var ngaysinh = Convert.ToDateTime(collection["ngaysinh"]);
            var anhdaidien = collection["anhdaidien"];



            th.makh = id;
            if (string.IsNullOrEmpty(tenkhachhang))
            {
                ViewData["Error"] = "Don't empty!";
            }
            else
            {
                th.tenkhachhang = tenkhachhang.ToString();
                Session["TaiKhoanKH"] = th.tenkhachhang;
                th.sodienthoai = sodienthoai.ToString();
                th.email = email.ToString();
                th.diachi = diachi.ToString();
                th.ngaysinh = ngaysinh;
                th.anhdaidien = anhdaidien;


                UpdateModel(th);
                data.SubmitChanges();
                return RedirectToAction("Index", "TrangChu");
            }
            return this.EditInfor(id);
        }

        public string ProcessUpload(HttpPostedFileBase file)
        {
            if (file == null)
            {
                return "";
            }
            file.SaveAs(Server.MapPath("~/Content/AnhKhachHang/" + file.FileName));

            return file.FileName;
        }

        //đổi mật khẩu - hàm get
        public ActionResult ChangePass(int id)
        {
            KHACHHANG find = data.KHACHHANGs.FirstOrDefault(m => m.makh == id);
            if (find == null)
                return HttpNotFound();

            return View(find);
        }
        //đổi mật khẩu - hàm post
        [HttpPost]
        public ActionResult ChangePass(int id, FormCollection collection)
        {
            if (System.Web.HttpContext.Current.Session["TaiKhoanKH"].Equals(""))
            {
                System.Web.HttpContext.Current.Response.Redirect("~/TrangChu/Index");
            }
            var th = data.KHACHHANGs.First(m => m.makh == id);
            var tenkhachhang = collection["tenkhachhang"];
            var matkhauCu = collection["passCu"];
            var matkhauMoi = collection["passMoi"];
            if (matkhauCu == th.matkhau)
            {
                th.matkhau = matkhauMoi;
                UpdateModel(th);
                data.SubmitChanges();
                return RedirectToAction("Index", "TrangChu");
            }
            else
            {
                Session["ThongBao"] = "Mật khẩu cũ không chính xác!!!";

            }
            th.makh = id;

            return this.ChangePass(id);
        }

        //action send email
        public ActionResult SendEmail(string email, FormCollection collection)
        {

            var email1 = email;
            Random random = new Random();
            string maXT = "";
            for (int i = 1; i <= 1; i++)
            {
                //sử dụng random.Next(b) để hiển thị các số ngẫu nhiên giới hạn là b

                maXT = (random.Next(999999 - (100000 - 1)) + 100000).ToString();
                Session["XacThuc"] = maXT;
            }
            try
            {
                var senderemail = new MailAddress("01642027120q@gmail.com", "SHOP MIAAA");
                var receiveremail = new MailAddress(email1, "Reciver");


                var sub = "Mã Xác Nhận Quên Mật Khẩu";

                var body = "Chào bạn " + email1;
                body += "\nBạn đã yêu cầu gửi mã OTP để lấy lại mật khẩu?";
                body += "\nĐây là mã xác nhận của bạn!!!!";
                body += "\nMã xác nhận của bạn: " + maXT;

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

            return Json(new { Message = "Thành công", JsonRequestBehavior.AllowGet });
        }

        //hàm quên mật khẩu - otp gửi qua email
        public ActionResult PassEmail(FormCollection collection)
        {
            var maOTP = collection["maOTP"];
            var email = collection["email"];
            var taikhoan = collection["taikhoan"];
            var matKhauMoi = collection["matKhauMoi"];
            var repass = collection["repass"];
            //var XacThuc = Session["XacThuc"];
            if (Session["XacThuc"].Equals(maOTP))
            {
                if (matKhauMoi == repass)
                {
                    var th = data.KHACHHANGs.First(m => m.email == email && m.taikhoankh == taikhoan);
                    th.matkhau = matKhauMoi;
                    UpdateModel(th);
                    data.SubmitChanges();
                    return RedirectToAction("Index", "TrangChu");
                }
                else
                {

                }

            }
            return View();
        }

        //hàm xác thực tài khoản = email
        public ActionResult XacThuc(FormCollection collection)
        {
             
            var taikhoan = collection["taikhoan"];
            var maOTP = collection["maOTP"];
            var email = collection["email"];

            if (Session["XacThuc"].Equals(maOTP))
            {
                var th = data.KHACHHANGs.First(m => m.email == email && m.taikhoankh == taikhoan);
                th.trangthai = true;

                UpdateModel(th);
                data.SubmitChanges();
                return RedirectToAction("Index", "TrangChu");
                 
            }

            return View();
        }


    }
}