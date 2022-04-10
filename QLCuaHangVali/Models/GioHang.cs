using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLCuaHangVali.Models
{
    public class GioHang
    {
        ValiDBDataContext db = new ValiDBDataContext();
        public int imavali { set; get; }
        public string itenvali { set; get; }
        public string ianhvali { set; get; }
        public int masize { set; get; }
        public int isoluong { set; get; }
        public int soluongton { set; get; }
        public double dDongia { get; set; }
        public double dThanhtien
        {
            get { return isoluong * dDongia; }
        }
        //khoi tao gio hang theo mavali
        public GioHang(int id)
        {
            imavali = id;
            VALI vali = db.VALIs.Single(n => n.mavali == imavali);
            itenvali = vali.tenvali;
            ianhvali = vali.anhvali;
            dDongia = double.Parse(vali.gia.ToString());
            isoluong = 1;
        }
    }
}