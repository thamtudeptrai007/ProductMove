using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductMove_Model;

namespace ProductMove_API.Service.SanPhamService
{
    public class SanPhamService : ISanPhamService
    {
        private readonly ProductMove_DBContext _context;
        public SanPhamService(ProductMove_DBContext context)
        {
            _context = context;
        }
        public IList<SanPham> SanPham { get; set; } = default!;
        public async Task<int> addSanPhamAsync(SanPham sanPham)
        {
            _context.SanPham.Add(sanPham);
            await _context.SaveChangesAsync();
            return sanPham.idSanPham;
        }

        public async Task deleteSanPhamAsync(int id)
        {
            var delete = _context.SanPham.SingleOrDefault(a => a.idSanPham == id);
            if (delete != null)
            {
                _context.SanPham.Remove(delete);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IList<SanPham>> getAllSanPhamAsync()
        {
            var sanpham = await _context.SanPham
                .Include(s => s.LoaiSanPham).ToListAsync();
            return sanpham;
        }

        public async Task<SanPham> getSanPhamAsync(int id)
        {
            var sanpham = await _context.SanPham!.FindAsync(id);
            return sanpham!;
        }
        public async Task updateSanPhamAsync(int id, SanPham sanPham)
        {
            if (id == sanPham.idSanPham)
            {
                _context.SanPham.Update(sanPham);
                await _context.SaveChangesAsync();
            }
        }
    }
}
