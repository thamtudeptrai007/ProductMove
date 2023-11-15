using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductMove_Model
{
    public class XuatKho
    {
        [Key]
        public int idXuat { get; set; }
        [ForeignKey("SanPham")]
        [Display(Name = "Sản Phẩm")]
        public int idSanPham { get; set; }
        [ForeignKey("Kho")]
        [Display(Name = "Kho")]
        public int idKho { get; set; }
        [Display(Name = "Ngày Xuất")]
        public string NgayXuat { get; set; } = default!;
        [Display(Name = "Số Lượng")]
        public int SoLuong { get; set; }
        public string SeriSanpham { get; set; } = default!;
        public SanPham? SanPham { get; set; } = default!;
        public Kho? Kho { get; set; } = default!;
    }
}
