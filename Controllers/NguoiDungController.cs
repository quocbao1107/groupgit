using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanBanh.Models;
using System.Web.Security;
using System.Configuration;


namespace WebBanBanh.Controllers
{

    public class NguoiDungController : Controller
    {
        dbQLBanBanhDataContext data = new dbQLBanBanhDataContext();

        public ActionResult Index()
        {
            return View();
        }
       
        public ActionResult lienhe()
        {
            return View();
        }
        public ActionResult Dangky()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Dangky(KHACHHANG khachHang)
        {
            var checkuser = data.KHACHHANGs.Any(k => k.TaiKhoanKH == khachHang.TaiKhoanKH);
            var checkemail = data.KHACHHANGs.Any(k => k.EmailKH == khachHang.EmailKH);
            if (checkuser)
            {
                ModelState.AddModelError("", "Tài khoản đã được sử dụng");
                if (checkemail)
                    ModelState.AddModelError("", "Email đã có người sử dụng");
                return View("Dangky");
            }
            if (checkemail)
            {
                ModelState.AddModelError("", "Email đã có người sử dụng");
                if (checkuser)
                    ModelState.AddModelError("", "Tài khoản đã được sử dụng");
                return View("Dangky");
            }

            data.KHACHHANGs.InsertOnSubmit(khachHang);
            data.SubmitChanges();
            var dangNhap = new DangNhapModel();
            dangNhap.TaiKhoanKH = khachHang.TaiKhoanKH;
            dangNhap.MatKhauKH = khachHang.MatKhauKH;
            return View("Dangnhap", dangNhap);
        }
        [HttpGet]
        public ActionResult Dangnhap()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Dangnhap(FormCollection collection)
        {
            var tendn = collection["TaiKhoanKH"];
            var matkhau = collection["MatKhauKH"];
            if (String.IsNullOrEmpty(tendn))
            {
                ViewData["Loi1"] = "Phải nhập tên đăng nhập";
            }
            else if (String.IsNullOrEmpty(matkhau))
            {
                ViewData["Loi2"] = "Phải nhập mật khẩu";
            }
            else
            {
                KHACHHANG nd = data.KHACHHANGs.SingleOrDefault(n => n.TaiKhoanKH == tendn && n.MatKhauKH == matkhau);
                if (nd != null)
                {
                    Session["Name"] = nd.HoTenKH.ToString();
                    Session["TaiKhoan"] = nd;
                    if (Session["TaiKhoan"] == null)
                    {
                        Session["TaiKhoan"] = nd.TaiKhoanKH.ToString();
                    }
                    //return RedirectToAction("SanPham", "Clock");

                    else
                        ViewBag.Thongbao = "Tên đăng nhập hoặc mật khẩu không đúng.";
                }

            }
            return RedirectToAction("DatHang", "GioHang");
        }
        public ActionResult DangXuat()
        {
            Session.Abandon();
            FormsAuthentication.SignOut();
            return RedirectToAction("SanPham", "Candy");
        }
    }
}