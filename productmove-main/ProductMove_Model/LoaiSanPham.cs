using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductMove_Model
{
    public class LoaiSanPham
    {
        [Key]
        public int IdLoaiSanPham { get; set; }
        [Display(Name = "Tên Loại Sản Phẩm")]
        public string TenLoaiSanPham { get; set; } = default!;
        public ICollection<SanPham>? SanPham { get; set; } = default!;
    }
}
