using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductMove_Model
{
    public class TaiKhoan
    {
        [Key]
        public int idTaiKhoan { get; set; }
        [Required(ErrorMessage = "Dữ liệu nhập vào không hợp lệ")]
        [Display(Name = "Tên đăng nhập")]
        public string TenDangNhap { get; set; } = default!;
        [Required(ErrorMessage = "Dữ liệu nhập vào không hợp lệ")]
        [Display(Name = "Mật khẩu")]
        public string MatKhau { get; set; } = default!;
        [Required(ErrorMessage = "Vui lòng chọn phân quyền")]
        [Display(Name = "Quyền truy cập")]
        [ForeignKey("PhanQuyen")]
        public string PhanQuyen { get; set; } = default!;
        [Required(ErrorMessage = "Dữ liệu nhập vào không hợp lệ")]
        [Display(Name = "Địa chỉ")]
        public string DiaChi { get; set; } = default!;
        [Required(ErrorMessage = "Dữ liệu nhập vào không hợp lệ")]
        public string Mail { get; set; } = default!;
        public ICollection<Kho>? Khos { get; set; } = default!;
    }
}