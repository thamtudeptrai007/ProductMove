using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductMove_Model
{
    public class ChiTietKho
    {
        [Key]
        public int idChiTietKho { get; set; }
        [ForeignKey("Kho")]
        [Display(Name = "Kho")]
        public int idKho { get; set; }
        [Display(Name = "Trạng Thái Sản Phẩm")]
        public string TrangThaiSanPham { get; set; } = default!;
        [Display(Name = "Số Lượng")]
        public int SoLuong { get; set; }
        [Display(Name = "Kho")]
        public Kho? Kho { get; set; } = default!;
        [ForeignKey("SanPham")]
        [Display(Name = "Mã Sản Phẩm")]
        public int idSanPham { get; set; }
        [Display(Name = "Sản Phẩm")]
        public SanPham? SanPham { get; set; } = default!;
       

    }
}
