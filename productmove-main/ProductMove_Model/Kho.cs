using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductMove_Model
{
    public class Kho
    {
        [Key]
        public int idKho { get; set; }
        [Display(Name = "Số Lượng Sản Phẩm")]
        public int SoLuongSanPham { get; set; }
        [ForeignKey("TaiKhoan")]
        [Display(Name = "Tài Khoản")]
        public int idTaiKhoan { get; set; }
        public ICollection<NhapKho>? NhapKho { get; set; } = default!;
        public TaiKhoan? TaiKhoan { get; set; } = default!;
        public ICollection<ChiTietKho>? ChiTietKho { get; set; } = default!;
        public ICollection<Seri>? Seri { get; set; } = default!;
        public ICollection<XuatKho>? XuatKho { get; set; } = default!;
    }
}
