using Microsoft.EntityFrameworkCore;
using ProductMove_Model;

namespace ProductMove_API.Service.KhachHangService
{
    public class KhachHangService : IKhachHangService
    {
        private readonly ProductMove_DBContext _context;

        public KhachHangService(ProductMove_DBContext context)
        {
            _context = context;
        }
        public async Task<int> addNewKhachHangAsync(KhachHang khachHang)
        {
            _context.KhachHang.Add(khachHang);
            await _context.SaveChangesAsync();
            return khachHang.IdKhachHang;
        }

        public async Task<List<KhachHang>> getAllKhachHangAsync()
        {
            return await _context.KhachHang.ToListAsync();
        }

        public async Task<KhachHang> getKhachHangByIdAsync(int id)
        {
            var data = await _context.KhachHang!.FindAsync(id);
            return data!;
        }

        public async Task<KhachHang> getKhachHangBySDTAsync(string sdt)
        {
            var data = await _context.KhachHang!.Where(a => a.SoDienThoai == sdt).FirstOrDefaultAsync();
            return data!;
        }

        public async Task updateThongtinKhachHang(int id, KhachHang khachHang)
        {
            if (id == khachHang.IdKhachHang)
            {
                _context.KhachHang.Update(khachHang);
                await _context.SaveChangesAsync();
            }
        }
    }
}
