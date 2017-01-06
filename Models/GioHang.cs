using WebBanBanh.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;


namespace WebBanBanh.Models
{
    public class GioHang
    {
        dbQLBanBanhDataContext data = new dbQLBanBanhDataContext();
        public int iMaSP { set; get; }
        public string sTenSP { set; get; }
        public string sAnhBia { set; get; }
        public Double dGiaBan { set; get; }
        public int sSoLuong { set; get; }
        public Double dThanhTien { get { return sSoLuong * dGiaBan; } }
        public GioHang(int Masp)
        {
            iMaSP = Masp;
            SANPHAM sp = data.SANPHAMs.Single(n => n.MaSP == iMaSP);
            sTenSP = sp.TenSP;
            sAnhBia = sp.AnhBia;
            dGiaBan = double.Parse(sp.GiaBan.ToString());
            sSoLuong = 1;
        }
    }
    public class NguoiNhan
    {
        [Required(ErrorMessage = "Vui lòng nhập tên người nhận")]
        public string TenNguoiNhan { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập sô điện thoại người nhận")]
        public string SdtNguoiNhan { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập địa chỉ giao hàng")]
        public string DCGiaoHang { get; set; }

        public DateTime NgayGiao { get; set; }
    }
}