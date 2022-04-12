using QLCuaHangVali.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using System.Net.Mail;
using System.Net;

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
        //hàm trang giới thiệu
        public ActionResult GioiThieu()
        {
            return View("GioiThieu");
        }

        public ActionResult Index(int? page)
        {
            if (page == null) page = 1;
            var Danhmuc = db.DANHMUCVALIs.ToList();
            var Vali = db.VALIs.ToList();
            List<VALI> Valitheodanhmuc = new List<VALI>();
            foreach (var item in Danhmuc)
            {
                int sttitem1 = 0;
                foreach (var item1 in Vali)
                {
                    if (item1.DANHMUCVALI.madanhmuc == item.madanhmuc)
                    {
                        sttitem1++;
                        if (sttitem1 <= 3)
                        {
                            Valitheodanhmuc.Add(item1);
                        }
                    }
                }
            }
            int pageSize = 12;
            int pageNum = page ?? 1;
            return View(Valitheodanhmuc.ToPagedList(pageNum, pageSize));
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
                // code gưir email khi khách hàng liên hệ
                try
                {
                    var senderemail = new MailAddress("01642027120q@gmail.com", "SHOP MIAAA");
                    var receiveremail = new MailAddress(kh.email, "Reciver");
                    var receiveremail1 = new MailAddress("01642027120q@gmail.com", "Reciver");

                    var sub = "Cảm Ơn Sự Phản Hồi Của Bạn";
                    var sub1 = "Phản Hồi Khách Hàng ";
                    var body = "Trân trọng cảm ơn quý khách hàng đã đến và lựa chọn sử dụng dịch vụ của chúng tôi. Xin gửi lời cảm ơn chân thành và sâu sắc nhất đến quý khách hàng. ";
                    body += "\nChúng tôi đã ghi nhận được sự phản hồi từ bạn!!!";
                    var body1 = "Tên khách hàng: " + kh.tenkhachhang;
                    body1 += "\nTiêu đề: " + kh.tieude;
                    body1 += "\nNội dung: " + kh.noidung;

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
                    //
                    using (var mess = new MailMessage(senderemail, receiveremail1)
                    {
                        Subject = sub1,
                        Body = body1
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
          

            return View();
        }

        // Chiết tiế từng vali
        public ActionResult Details(int id)
        {
            var vali = from s in db.VALIs
                       where s.mavali == id
                       select s;
            VALI vl = db.VALIs.FirstOrDefault(n => n.mavali == id);
            Session["CapNhatSoLuong"] = vl.soluongton;
            return View(vali.Single());
        }

        public ActionResult ValiIndex(int? page)
        {

            if (page == null) page = 1;
            var Danhmuc = db.DANHMUCVALIs.ToList();
            var Vali = db.VALIs.ToList();
            List<VALI> Valitheodanhmuc = new List<VALI>();
            foreach (var item in Danhmuc)
            {
                int sttitem1 = 0;
                foreach (var item1 in Vali)
                {
                    if (item1.DANHMUCVALI.madanhmuc == item.madanhmuc)
                    {
                        sttitem1++;
                        if (sttitem1 <= 3)
                        {
                            Valitheodanhmuc.Add(item1);
                        }
                    }
                }
            }
            int pageSize = 12;
            int pageNum = page ?? 1;
            return View(Valitheodanhmuc.ToPagedList(pageNum, pageSize));
        }
    }
}