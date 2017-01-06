using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanBanh.Models;
using System.IO;
using PagedList;
using PagedList.Mvc;
using System.Web.Security;
using System.Web.UI.WebControls;


namespace WebBanBanh.Controllers
{
    public class AdminController : Controller
    {
        dbQLBanBanhDataContext db = new dbQLBanBanhDataContext();
        // GET: Admin
        public ActionResult Index()
        {
            if (Session["UserAdmin"] == null)
                return RedirectToAction("Login", "Admin");
            return View();
        }
        public ActionResult SanPham(int? page)
        {
            if (Session["UserAdmin"] == null)
                return RedirectToAction("Login", "Admin");
            int pageNumber = (page ?? 1);
            int pageSize = 7;
            var sanPhams = db.SANPHAMs.OrderByDescending(d => d.MaSP);
            //return View(db.SANPHAMs.ToList());
            return View(sanPhams.ToPagedList(pageNumber, pageSize));

        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(ADMIN admin)
        {
            var checkuseradmin = db.ADMINs.Any(k => k.UserAdmin == admin.UserAdmin);
            var checkpassadmin = db.ADMINs.Any(k => k.PassAdmin == admin.PassAdmin);

            if (!checkuseradmin)
            {
                ModelState.AddModelError("", "Tài khoản không đúng");
                if (!checkpassadmin)
                    ModelState.AddModelError("", "Mật khẩu không đúng");
                return View("Login");
            }
            if (!checkpassadmin)
            {
                ModelState.AddModelError("", "Mật khẩu không đúng");
                if (!checkuseradmin)
                    ModelState.AddModelError("", "Tài khoản không đúng");
                return View("Login");
            }
            var tenUser = db.ADMINs.FirstOrDefault(a => a.UserAdmin == admin.UserAdmin).HoTen;
            Session["UserAdmin"] = tenUser;
            return RedirectToAction("Index", "Admin");
        }
        public ActionResult Logout()
        {
            Session.Abandon();
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Admin");
        }
        [HttpGet]
        public ActionResult ThemmoiSP()
        {
            if (Session["UserAdmin"] == null)
                return RedirectToAction("Login", "Admin");
            ViewBag.MaNSX = new SelectList(db.NHASANXUATs.ToList().OrderBy(n => n.TenNSX), "MaNSX", "TenNSX");
            ViewBag.MaLoaiSP = new SelectList(db.LOAISPs.ToList().OrderBy(n => n.TenLoaiSP), "MaLoaiSP", "TenLoaiSP");
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ThemmoiSP(SANPHAM sanPham, HttpPostedFileBase fileUpload)
        {
            if (Session["UserAdmin"] == null)
                return RedirectToAction("Login", "Admin");
            ViewBag.MaNSX = new SelectList(db.NHASANXUATs.ToList().OrderBy(n => n.TenNSX), "MaNSX", "TenNSX");
            ViewBag.MaLoaiSP = new SelectList(db.LOAISPs.ToList().OrderBy(n => n.TenLoaiSP), "MaLoaiSP", "TenLoaiSP");
            if (fileUpload == null)
            {
                ViewBag.Thongbao = "Vui lòng chọn ảnh bìa";
                return View();
            }
            else
            {
                if (ModelState.IsValid)
                {
                    var fileName = Path.GetFileName(fileUpload.FileName);
                    var path = Path.Combine(Server.MapPath("~/HinhSanPham"), fileName);
                    if (System.IO.File.Exists(path))
                        ViewBag.Thongbao = "Hình ảnh đã tồn tại";
                    else
                    {
                        fileUpload.SaveAs(path);
                    }
                    sanPham.AnhBia = fileName;
                    sanPham.NgayCapNhat = DateTime.Now;
                    db.SANPHAMs.InsertOnSubmit(sanPham);
                    db.SubmitChanges();
                }
                return RedirectToAction("SanPham");
            }
        }
        [HttpGet]
        public ActionResult SuaSP(int maSP)
        {
            if (Session["UserAdmin"] == null)
                return RedirectToAction("Login", "Admin");
            SANPHAM sanPham = db.SANPHAMs.SingleOrDefault(n => n.MaSP == maSP);
            if (sanPham == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            ViewBag.MaNSX = new SelectList(db.NHASANXUATs.ToList().OrderBy(n => n.TenNSX), "MaNSX", "TenNSX", sanPham.MaNSX);
            ViewBag.MaLoaiSP = new SelectList(db.LOAISPs.ToList().OrderBy(n => n.TenLoaiSP), "MaLoaiSP", "TenLoaiSP", sanPham.MaLoaiSP);

            return View(sanPham);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult SuaSP(SANPHAM sanPham, HttpPostedFileBase fileUpload, string anhBia)
        {
            if (Session["UserAdmin"] == null)
                return RedirectToAction("Login", "Admin");
            ViewBag.MaNSX = new SelectList(db.NHASANXUATs.ToList().OrderBy(n => n.TenNSX), "MaNSX", "TenNSX");
            ViewBag.MaLoaiSP = new SelectList(db.LOAISPs.ToList().OrderBy(n => n.TenLoaiSP), "MaLoaiSP", "TenLoaiSP");
            if (1 > 3)
            {
                return null;

            }
            else
            {
                if (ModelState.IsValid)
                {

                    var sp = db.SANPHAMs.FirstOrDefault(s => s.MaSP == sanPham.MaSP);
                    if (fileUpload != null)
                    {
                        var fileName = Path.GetFileName(fileUpload.FileName);
                        var path = Path.Combine(Server.MapPath("~/HinhSanPham"), fileName);
                        if (System.IO.File.Exists(path))
                            ViewBag.Thongbao = "Hình ảnh đã tồn tại";
                        else
                        {
                            fileUpload.SaveAs(path);
                        }
                        sp.AnhBia = fileName;
                    }
                    sp.TenSP = sanPham.TenSP;
                    sp.GiaBan = sanPham.GiaBan;
                    sp.MoTa = sanPham.MoTa;
                    sp.NgayCapNhat = sanPham.NgayCapNhat;
                    sp.SoLuongTon = sanPham.SoLuongTon;
                    sp.PhanTramKM = sanPham.PhanTramKM;
                    sp.MaNSX = sanPham.MaNSX;
                    sp.MaLoaiSP = sanPham.MaLoaiSP;

                    db.SubmitChanges();
                }
                return RedirectToAction("SanPham");
            }
        }
        public ActionResult ChitietSP(int maSP)
        {
            if (Session["UserAdmin"] == null)
                return RedirectToAction("Login", "Admin");
            SANPHAM sanPham = db.SANPHAMs.SingleOrDefault(n => n.MaSP == maSP);
            ViewBag.MaSP = sanPham.MaSP;
            if (sanPham == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(sanPham);
        }
        public ActionResult XoaSP(int maSP)
        {
            if (Session["UserAdmin"] == null)
                return RedirectToAction("Login", "Admin");
            SANPHAM sanPham = db.SANPHAMs.SingleOrDefault(n => n.MaSP == maSP);
            ViewBag.MaSP = sanPham.MaSP;
            if (sanPham == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(sanPham);
        }
        [HttpPost, ActionName("XoaSP")]
        public ActionResult Xacnhanxoa(int maSP)
        {
            var deleteHelper = new DeleteHelper();
            deleteHelper.DeleteSanPham(maSP);
            db.SubmitChanges();
            return RedirectToAction("SanPham");
        }
        public ActionResult DonDatHang()
        {
            if (Session["UserAdmin"] == null)
                return RedirectToAction("Login", "Admin");
            var all_DDH = from DDH in db.DONDATHANGs select DDH;
            return View(all_DDH);
        }
        public ActionResult ChitietDDH(int maDDH)
        {
            if (Session["UserAdmin"] == null)
                return RedirectToAction("Login", "Admin");
            var donDatHang = db.DONDATHANGs.SingleOrDefault(n => n.MaDDH == maDDH);
            ViewBag.chiTiets = donDatHang.CHITIETDDHs;
            ViewBag.Tongtien = db.CHITIETDDHs.Where(c => c.MaDDH == maDDH).Sum(t => t.DonGia * t.SoLuongBan);
            if (donDatHang == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(donDatHang);
        }
        public ActionResult SuaDDH(int maDDH)
        {
            if (Session["UserAdmin"] == null)
                return RedirectToAction("Login", "Admin");
            DONDATHANG ddh = db.DONDATHANGs.SingleOrDefault(m => m.MaDDH == maDDH);
            return View(ddh);
        }
        [HttpPost]
        public ActionResult SuaDDH(DONDATHANG d)
        {
            if (Session["UserAdmin"] == null)
                return RedirectToAction("Login", "Admin");
            DONDATHANG ddh = db.DONDATHANGs.SingleOrDefault(m => m.MaDDH == d.MaDDH);
            ddh.NgayDat = d.NgayDat;
            ddh.TinhTrangGiaoHang = d.TinhTrangGiaoHang;
            ddh.TenNguoiNhan = d.TenNguoiNhan;
            ddh.SdtNguoiNhan = d.SdtNguoiNhan;
            ddh.DCGiaoHang = d.DCGiaoHang;
            ddh.NgayGiao = d.NgayGiao;
            UpdateModel(ddh);
            db.SubmitChanges();
            return RedirectToAction("DonDatHang");
        }
        public ActionResult XoaDDH(int maDDH)
        {
            if (Session["UserAdmin"] == null)
                return RedirectToAction("Login", "Admin");
            DONDATHANG donDatHang = db.DONDATHANGs.SingleOrDefault(n => n.MaDDH == maDDH);
            ViewBag.MaDDH = donDatHang.MaDDH;
            if (donDatHang == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(donDatHang);
        }
        [HttpPost, ActionName("XoaDDH")]
        public ActionResult XacnhanxoaDDH(int maDDH)
        {
            var deleteHelper = new DeleteHelper();
            deleteHelper.DeleteDonDatHang(maDDH);
            return RedirectToAction("DonDatHang");
        }
       
        public ActionResult Search()
        {
            var model = new List<SANPHAM>();
            return View(model);
        }

        [HttpPost]
        public ActionResult Search(String Keywords = "")
        {
            var model = db.SANPHAMs
                .Where(p => p.TenSP.Contains(Keywords));
            return View(model);
        }
    }
}
