using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanBanh.Models;
using PagedList;
using PagedList.Mvc;
namespace SHOPDONGHO.Controllers
{
    public class CandyController : Controller
    {
        // GET: Candy
        dbQLBanBanhDataContext data = new dbQLBanBanhDataContext();

        private List<SANPHAM> laysanpham(int count)
        {
            return data.SANPHAMs.OrderByDescending(a => a.NgayCapNhat).Take(count).ToList();
        }
        public ActionResult SanPhamMoi()
        {
            var spmoi = laysanpham(5);
            return View(spmoi);
        }
        public ActionResult SanPham(int? page)
        {
            int pageSize = 12;
            int pageNum = (page ?? 1);
            var all_tt = from tt in data.SANPHAMs select tt;
            return View(all_tt.ToPagedList(pageNum, pageSize));

        }
        public ActionResult TheoNhaSanXuat()
        {
            var loainsx = from ll in data.NHASANXUATs select ll;
            return PartialView(loainsx);
        }
        public ActionResult TheoLoaiSP()
        {
            var loaisp = from loai in data.LOAISPs select loai;
            return PartialView(loaisp);
        }
        public ActionResult SPLoai(int id)
        {
            var dh = from s in data.SANPHAMs where s.MaLoaiSP == id select s;
            return View(dh);
        }
        public ActionResult SPNhaSanXuat(int id)
        {
            var dd = from s in data.SANPHAMs where s.MaNSX == id select s;
            return View(dd);
        }
        public ActionResult ChitietSP(int id)
        {
            //var ctsp = from s in data.SANPHAMs where s.MaSP == id select s;
            //return View(ctsp.Single());
            var CT_SP = data.SANPHAMs.First(ctsp => ctsp.MaSP == id);
            ViewBag.dhcl = data.SANPHAMs.Where(cl => cl.MaLoaiSP == CT_SP.MaLoaiSP && cl.MaSP != CT_SP.MaSP).Take(4).ToList();
            return View(CT_SP);
        }

    }
}