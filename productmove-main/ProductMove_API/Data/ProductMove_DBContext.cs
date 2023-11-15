using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProductMove_Model;

public class ProductMove_DBContext : DbContext
{
    public ProductMove_DBContext(DbContextOptions<ProductMove_DBContext> options)
        : base(options)
    {
    }

    public DbSet<ChiTietKho> ChiTietKho { get; set; } = default!;
    public DbSet<Kho> Kho { get; set; } = default!;
    public DbSet<LoaiSanPham> LoaiSanPham { get; set; } = default!;
    public DbSet<NhapKho> NhapKho { get; set; } = default!;
    public DbSet<SanPham> SanPham { get; set; } = default!;
    public DbSet<TaiKhoan> TaiKhoan { get; set; } = default!;
    public DbSet<XuatKho> XuatKho { get; set; } = default!;
    public DbSet<Seri> Seri { get; set; } = default!;
    public DbSet<KhachHang> KhachHang { get; set; } = default!;
    public DbSet<HoaDon> HoaDon { get; set; } = default!;
    public DbSet<BaoCao> BaoCao { get; set; } = default!;


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ChiTietKho>().ToTable("ChiTietKho");
        modelBuilder.Entity<Kho>().ToTable("Kho");
        modelBuilder.Entity<LoaiSanPham>().ToTable("LoaiSanPham");
        modelBuilder.Entity<NhapKho>().ToTable("NhapKho");
        modelBuilder.Entity<SanPham>().ToTable("SanPham");
        modelBuilder.Entity<TaiKhoan>().ToTable("TaiKhoan");
        modelBuilder.Entity<XuatKho>().ToTable("XuatKho");
        modelBuilder.Entity<Seri>().ToTable("Seri");
        modelBuilder.Entity<KhachHang>().ToTable("KhachHang");
        modelBuilder.Entity<HoaDon>().ToTable("HoaDon");
        modelBuilder.Entity<BaoCao>().ToTable("BaoCao");
    }
}
