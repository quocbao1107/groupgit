using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebBanBanh.Models
{
    public class DeleteHelper
    {
        dbQLBanBanhDataContext db = new dbQLBanBanhDataContext();
        public void DeleteChiTietTheoDDH(int maDDH)
        {
            var chiTiets = db.CHITIETDDHs.Where(m => m.MaDDH == maDDH).ToList();
            foreach (var item in chiTiets)
            {
                db.CHITIETDDHs.DeleteOnSubmit(item);
            }
            db.SubmitChanges();
        }
        public void DeleteChiTietTheoSP(int maSP)
        {
            var chiTiets = db.CHITIETDDHs.Where(d => d.MaSP == maSP);
            foreach (var item in chiTiets)
            {
                db.CHITIETDDHs.DeleteOnSubmit(item);
            }
            db.SubmitChanges();
        }
        public void DeleteDonDatHang(int maDDH)
        {
            DeleteChiTietTheoDDH(maDDH);
            var donDatHang = db.DONDATHANGs.FirstOrDefault(d => d.MaDDH == maDDH);
            db.DONDATHANGs.DeleteOnSubmit(donDatHang);
            db.SubmitChanges();
        }
        public void DeleteSanPham(int maSP)
        {
            DeleteChiTietTheoSP(maSP);
            var sanPhams = db.SANPHAMs.FirstOrDefault(m => m.MaSP == maSP);
            db.SANPHAMs.DeleteOnSubmit(sanPhams);
            db.SubmitChanges();
        }
    }
}