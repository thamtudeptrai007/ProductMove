using Microsoft.EntityFrameworkCore;
using ProductMove_Model;

namespace ProductMove_API.Service.XuatKhoService
{
    public class XuatKhoService : IXuatKhoService
    {
        private readonly ProductMove_DBContext _context;

        public XuatKhoService(ProductMove_DBContext context)
        {
            _context = context;
        }
        public async Task<int> addSpXuatKhoAsync(XuatKho XuatKho)
        {
            _context.XuatKho.Add(XuatKho);
            await _context.SaveChangesAsync();
            return XuatKho.idXuat;
        }

        public async Task deleteSpXuatKhoAsync(int id)
        {
            var delete = _context.XuatKho.SingleOrDefault(a => a.idXuat == id);
            if (delete != null)
            {
                _context.XuatKho.Remove(delete);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IList<XuatKho>> getAllSpPXuatKhoAsync()
        {
            return await _context.XuatKho.Include(s => s.SanPham).Include(s => s.Kho).ToListAsync();
        }

        public async Task<XuatKho> getSpXuatKhoAsync(int id)
        {
            var xuat = await _context.XuatKho!.FindAsync(id);
            return xuat!;
        }

        public async Task updateSpXuatKhoAsync(int id, XuatKho XuatKho)
        {
            if (id == XuatKho.idXuat)
            {
                _context.XuatKho.Update(XuatKho);
                await _context.SaveChangesAsync();
            }
        }
    }
}
