using Microsoft.EntityFrameworkCore;
using ProductMove_Model;

namespace ProductMove_API.Service.LoaiSanPhamService
{
    public class LoaiSanPhamService : ILoaiSanPhamService
    {
        private readonly ProductMove_DBContext _context;
        public LoaiSanPhamService(ProductMove_DBContext context)
        {
            _context = context;
        }
        public async Task<int> addLoaiSanPhamAsync(LoaiSanPham loaiSanPham)
        {
            _context.LoaiSanPham.Add(loaiSanPham);
            await _context.SaveChangesAsync();
            return loaiSanPham.IdLoaiSanPham;
        }

        public async Task deleteLoaiSanPhamAsync(int id)
        {
            var deleteloaiSanPham = _context.LoaiSanPham.SingleOrDefault(a => a.IdLoaiSanPham == id);
            if (deleteloaiSanPham != null)
            {
                _context.LoaiSanPham.Remove(deleteloaiSanPham);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<LoaiSanPham>> getAllLoaiSanPhamAsync()
        {
            var loaiSanPhams = await _context.LoaiSanPham!.ToListAsync();
            return loaiSanPhams;
        }

        public async Task<LoaiSanPham> getLoaiSanPhamAsync(int id)
        {
            var loaiSanPhams = await _context.LoaiSanPham!.FindAsync(id);
            return loaiSanPhams!;
        }

        public async Task<LoaiSanPham> getLoaiSanPhamByName(string name)
        {
            var loaisanphams = await _context.LoaiSanPham!.FirstOrDefaultAsync(a => a.TenLoaiSanPham.Equals(name));
            return loaisanphams!;
        }

        public async Task updateLoaiSanPhamAsync(int id, LoaiSanPham loaiSanPham)
        {
            if (id == loaiSanPham.IdLoaiSanPham)
            {
                _context.LoaiSanPham.Update(loaiSanPham);
                await _context.SaveChangesAsync();
            }
        }
    }
}
