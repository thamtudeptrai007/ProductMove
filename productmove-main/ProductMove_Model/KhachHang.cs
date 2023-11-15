using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductMove_Model
{
    public class KhachHang
    {
        [Key]
        public int IdKhachHang { get; set; }
        [Display(Name = "Tên Khách Hàng")]
        public string TenKhachHang { get; set; } = default!;
        [Display(Name = "Số Điện Thoại")]
        public string SoDienThoai { get; set; } = default!;
        [Display(Name = "Địa Chỉ")]
        public string DiaChi { get; set; } = default!;
        public ICollection<HoaDon>? HoaDon { get; set; } = default!;
    }
}
