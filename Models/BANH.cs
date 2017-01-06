using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebBanBanh.Models
{
    public class DangNhapModel
    {
        [Required(ErrorMessage = "Vui lòng nhập tên đăng nhập")]
        [StringLength(30, MinimumLength = 6, ErrorMessage = "Ít nhất là 6 ký tự")]
        public string TaiKhoanKH { get; set; }

        [Required(ErrorMessage = "Vui long nhập mật khẩu")]
        [StringLength(30, MinimumLength = 6, ErrorMessage = "Ít nhất là 6 ký tự")]
        public string MatKhauKH { get; set; }
    }
    public class DangKyModel
    {
        [Required(ErrorMessage = "Vui lòng nhập tên đăng nhập")]
        [StringLength(30, MinimumLength = 6, ErrorMessage = "Ít nhất là 6 ký tự")]
        public string HoTenKH { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập tài khoản")]
        [StringLength(30, MinimumLength = 6, ErrorMessage = "Ít nhất là 6 ký tự")]
        public string TaiKhoanKH { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
        [StringLength(30, MinimumLength = 6, ErrorMessage = "Ít nhất là 6 ký tự")]
        public string MatKhauKH { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập lại mật khẩu")]
        [Compare("MatKhauKH", ErrorMessage = "Nhập sai mật khẩu")]
        [StringLength(30, MinimumLength = 6, ErrorMessage = "Ít nhất là 6 ký tự")]
        public string MatKhauNhapLai { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập CMND")]
        [Range(0, Int32.MaxValue, ErrorMessage = "Bạn phải nhập số")]
        [StringLength(9, MinimumLength = 9, ErrorMessage = "Vui lòng nhập đúng 9 số")]
        public string CMND { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập Email")]
        [EmailAddress(ErrorMessage = "Email không đúng định dạng")]
        public string EmailKH { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập địa chỉ")]
        [StringLength(30, MinimumLength = 6, ErrorMessage = "Ít nhất là 6 ký tự")]
        public string DiaChiKH { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập điện thoại")]

        [MaxLength(11, ErrorMessage = "Số điện thoại ko hợp lệ")]
        [Range(0, Int64.MaxValue, ErrorMessage = "Bạn phải nhập số")]
        public string DienThoaiKH { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập ngày sinh")]
        public string NgaySinhKH { get; set; }
    }
    public class DangNhapAdminP
    {

        [Required(ErrorMessage = "Vui lòng nhập tên đăng nhập")]
        public string UserAdmin { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
        public string PassAdmin { get; set; }

    }
}