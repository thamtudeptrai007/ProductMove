using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductMove_Model
{
    public class HoaDon
    {
        [Key]
        public int IdHoaDon { get; set; }
        [Display(Name = "Ngày Lập Hóa Đơn")]
        public string NgayLapHoaDon { get; set; } = default!;
        [Display(Name = "Địa Chỉ")]
        public string Diachi { get; set; } = default!;
        [ForeignKey("KhachHang")]
        [Display(Name = "Khách Hàng")]
        public int IdKhachHang { get; set; }
        [ForeignKey("Seri")]
        public int idSeri { get; set; }
        public KhachHang? KhachHang { get; set; } = default!;
        public Seri? Seri { get; set; } = default!;

    }
}
