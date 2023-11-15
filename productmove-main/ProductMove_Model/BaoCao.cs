using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductMove_Model
{
    public class BaoCao
    {
        [Key]
        public int IdBaoCao { get; set; }
        public int LoaiBaoCao { get; set; }
        public string? ThoiGian {get; set; }
        public int IdKho { get; set; }
        
    }
}
