using Microsoft.EntityFrameworkCore;
using ProductMove_Model;

namespace ProductMove_API.Service.KhoService
{
    public class KhoService : IKhoService
    {
        private readonly ProductMove_DBContext _context;

        public KhoService(ProductMove_DBContext context)
        {
            _context = context;
        }
        public async Task<int> addKhoAsync(Kho kho)
        {
            _context.Kho.Add(kho);
            await _context.SaveChangesAsync();
            return kho.idKho;
        }

        public async Task deleteKhoAsync(int id)
        {
            var delete = _context.Kho.SingleOrDefault(a => a.idKho == id);
            if (delete != null)
            {
                _context.Kho.Remove(delete);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Kho>> getAllKhoAsync()
        {
            return await _context.Kho.ToListAsync();
        }

        public async Task<Kho> getKhoAsync(int id)
        {
            var kho = await _context.Kho!.FindAsync(id);
            return kho!;
        }

        public async Task<Kho> getKhoByIdTaiKhoanAsync(int id)
        {
            var kho = await _context.Kho!.Where(a => a.idTaiKhoan == id).FirstOrDefaultAsync();
            return kho!;
        }

        public async Task updateKhoAsync(int id, Kho kho)
        {
            if (id == kho.idKho)
            {
                _context.Kho.Update(kho);
                await _context.SaveChangesAsync();
            }
        }
    }
}
