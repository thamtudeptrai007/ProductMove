using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductMove_Model
{
    public class Seri
    {
        [Key]
        public int idSeri { get; set; }
        [Display(Name = "Seri Sản Phẩm")]
        public string tenSeri { get; set; } = default!;
        [Display(Name = "Thời Gian Sản Xuất")]
        public string ThoiGianSanXuat { get; set; } = default!;
        [ForeignKey("SanPham")]
        [Display(Name = "Sản Phẩm")]
        public int idSanPham { get; set; }
        [ForeignKey("Kho")]
        [Display(Name = "Kho")]
        public int? idKho { get; set; }
        [Display(Name = "Trạng Thái Sản Phẩm")]
        public string Trangthaisanpham { get; set; } = default!;
        public SanPham? SanPham { get; set; } = default!;
        public Kho? Kho { get; set; } = default!;
        public ICollection<HoaDon>? HoaDon { get; set; } = default!;

    }
}
