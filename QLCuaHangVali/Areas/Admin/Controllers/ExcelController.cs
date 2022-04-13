using OfficeOpenXml;
using QLCuaHangVali.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLCuaHangVali.Areas.Admin.Controllers
{
    public class ExcelController : Controller
    {

        // GET: Admin/Export
        ValiDBDataContext db = new ValiDBDataContext();
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(FormCollection form)
        {
            String message = "";
            var result = ExportData();
            if (result)
            {
                message = "Xuất file thành công!";
            }
            else
            {
                message = "Xuất file thất bại!";
            }
            ViewBag.message = message;
            return View();
        }

        private bool ExportData()
        {
            bool result = false;
            try
            {
                String fileName = "C:\\Users\\Admin\\Desktop\\ValiExport.xlsx";
                ExcelPackage pck = new ExcelPackage(new System.IO.FileInfo(fileName));
                //create new sheet
                var ws = pck.Workbook.Worksheets.Add("Vali");
                int startRow = 2;
                ws.Cells[1, 1].Value = "Mã Vali";
                ws.Cells[1, 2].Value = "Tên Vali";
                ws.Cells[1, 3].Value = "Thương hiệu";
                ws.Cells[1, 4].Value = "Size";
                ws.Cells[1, 5].Value = "Danh mục" ;
                ws.Cells[1, 6].Value = "Đường dẫn ảnh";
                ws.Cells[1, 7].Value = "Mô tả";
                ws.Cells[1, 8].Value = "Từ Khóa";
                ws.Cells[1, 9].Value = "Số lượng tồn";
                ws.Cells[1, 10].Value = "Đơn giá";
                ws.Cells[1, 11].Value = "Giá khuyến mãi";
                ws.Cells[1, 12].Value = "Ngày tạo";
                ws.Cells[1, 13].Value = "Trạng thái";
                ws.Cells[1, 14].Value = "Chất liệu";
                ws.Cells[1, 15].Value = "Kích thước";
                ws.Cells[1, 16].Value = "Thể tích";
                ws.Cells[1, 17].Value = "Bánh xe";
                ws.Cells[1, 18].Value = "Khóa";
                ws.Cells[1, 19].Value = "Tiện ích";
                ws.Cells[1, 20].Value = "Video";

                var vali = db.VALIs.ToList();

                foreach (var item in vali)
                {
                    ws.Cells[startRow, 1].Value = item.mavali;
                    ws.Cells[startRow, 2].Value = item.tenvali;
                    ws.Cells[startRow, 3].Value = item.THUONGHIEU.tenthuonghieu;
                    ws.Cells[startRow, 4].Value = item.SIZEVALI.tensize;
                    ws.Cells[startRow, 5].Value = item.DANHMUCVALI.tendanhmuc;
                    ws.Cells[startRow, 6].Value = item.anhvali;
                    ws.Cells[startRow, 7].Value = item.mota;
                    ws.Cells[startRow, 8].Value = item.tukhoa;
                    ws.Cells[startRow, 9].Value = item.soluongton;
                    ws.Cells[startRow, 10].Value = item.gia;
                    ws.Cells[startRow, 11].Value = item.giakhuyenmai;
                    ws.Cells[startRow, 12].Value = item.ngaytao.ToString();
                    ws.Cells[startRow, 13].Value = item.trangthai;
                    ws.Cells[startRow, 14].Value = item.chatlieu;
                    ws.Cells[startRow, 15].Value = item.kichthuoc;
                    ws.Cells[startRow, 16].Value = item.thetich;
                    ws.Cells[startRow, 17].Value = item.banhxe;
                    ws.Cells[startRow, 18].Value = item.khoa;
                    ws.Cells[startRow, 19].Value = item.tienich;
                    ws.Cells[startRow, 20].Value = item.linkvideo;
                    startRow++;
                }
                //save file
                pck.Save();
                result = true;
            }
            catch
            {
            }
            return result;
        }

        public ActionResult Indexdh()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Indexdh(FormCollection form)
        {
            String messagedh = "";
            var result1 = ExportDonHang();
            if (result1)
            {
                messagedh = "Xuất file thành công!";
            }
            else
            {
                messagedh = "Xuất file thất bại!";
            }
            ViewBag.messagedh = messagedh;
            return View();
        }

        private bool ExportDonHang()
        {
            bool result1 = false;
            try
            {
                String fileName = "C:\\Users\\Admin\\Desktop\\myExportDH.xlsx";
                ExcelPackage pck = new ExcelPackage(new System.IO.FileInfo(fileName));
                //create new sheet
                var ws = pck.Workbook.Worksheets.Add("DonHang");

                int startRow = 2;
                var  Ctdh = db.CHITIETDONHANGs.ToList();
                ws.Cells[1, 1].Value = " Tên Khách Hàng";
                ws.Cells[1, 2].Value = "Số điện thoại";
                ws.Cells[1, 3].Value = "Ngày đặt";
                ws.Cells[1, 4].Value = "Tên Vali";
                ws.Cells[1, 5].Value = "Số lượng mua";
                ws.Cells[1, 6].Value = "Đơn giá";
                ws.Cells[1, 7].Value = "Tổng Tiền";
                foreach (var item in Ctdh)
                {

                    ws.Cells[startRow, 1].Value = item.DONDATHANG.KHACHHANG.tenkhachhang;
                    ws.Cells[startRow, 2].Value = item.DONDATHANG.KHACHHANG.sodienthoai;
                    ws.Cells[startRow, 3].Value = item.DONDATHANG.ngaydat.ToString();
                    ws.Cells[startRow, 4].Value = item.VALI.tenvali;
                    ws.Cells[startRow, 5].Value = item.soluong;
                    ws.Cells[startRow, 6].Value = item.gia;
                    ws.Cells[startRow, 7].Value = item.soluong * item.gia;
                    startRow++;
                }
                //save file
                pck.Save();
                result1 = true;
            }
            catch
            {
            }
            return result1;
        }
    }
}