using Microsoft.EntityFrameworkCore;
using ProductMove_Model;

namespace ProductMove_API.Service.NhapKhoSerice
{
    public class NhapKhoService : INhapKhoService
    {
        private readonly ProductMove_DBContext _context;

        public NhapKhoService(ProductMove_DBContext context)
        {
            _context = context;
        }
        public async Task<int> addSpNhapKhoAsync(NhapKho nhapKho)
        {
            _context.NhapKho.Add(nhapKho);
            await _context.SaveChangesAsync();
            return nhapKho.idNhap;
        }

        public async Task deleteSpNhapKhoAsync(int id)
        {
            var delete = _context.NhapKho.SingleOrDefault(a => a.idNhap == id);
            if (delete != null)
            {
                _context.NhapKho.Remove(delete);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IList<NhapKho>> getAllSpPNhapKhoAsync()
        {
            return await _context.NhapKho.Include(s => s.SanPham).Include(s => s.Kho).ToListAsync();
        }

        public async Task<NhapKho> getSpNhapKhoAsync(int id)
        {
            var nhap = await _context.NhapKho!.FindAsync(id);
            return nhap!;
        }

        public async Task updateSpNhapKhoAsync(int id, NhapKho nhapKho)
        {
            if (id == nhapKho.idNhap)
            {
                _context.NhapKho.Update(nhapKho);
                await _context.SaveChangesAsync();
            }
        }
    }
}
