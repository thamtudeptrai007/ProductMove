using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductMove_Model
{
    public class SanPham
    {
        [Key]
        public int idSanPham { get; set; }
        [ForeignKey("LoaiSanPham")]
        [Display(Name = "Loại Sản Phẩm")]
        public int idLoaiSanPham { get; set; } = default!;
        [Display(Name = "Tên Sản Phẩm")]
        public string TenSanPham { get; set; } = default!;
        [Display(Name = "Giá Thành")]
        public string GiaThanh { get; set; } = default!;
        [Display(Name = "Mô Tã Sản Phẩm")]
        public string MoTaSanPham { get; set; } = default!;
        [Display(Name = "Thời Gian Bảo Hành")]
        public string ThoiGianBaoHanh { get; set; } = default!;
        public ICollection<NhapKho>? NhapKho { get; set; } = default!;
        public ICollection<XuatKho>? XuatKho { get; set; } = default!;
        public LoaiSanPham? LoaiSanPham { get; set; } = default!;
        public ICollection<ChiTietKho>? ChiTietKho { get; set; } = default!;
        public ICollection<Seri>? Seri { get; set; } = default!;

    }
}
