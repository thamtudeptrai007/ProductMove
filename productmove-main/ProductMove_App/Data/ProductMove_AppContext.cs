using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProductMove_Model;

namespace ProductMove_App.Data
{
    public class ProductMove_AppContext : DbContext
    {
        public ProductMove_AppContext(DbContextOptions<ProductMove_AppContext> options)
            : base(options)
        {
        }

        public DbSet<TaiKhoan> TaiKhoan { get; set; } = default!;
        public DbSet<SanPham> SanPham { get; set; } = default!;
        public DbSet<Seri> Seri { get; set; } = default!;
        public DbSet<BaoCao> BaoCao { get; set; } = default!;
    }
}
