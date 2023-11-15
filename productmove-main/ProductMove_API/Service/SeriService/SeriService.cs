using Microsoft.EntityFrameworkCore;
using ProductMove_Model;

namespace ProductMove_API.Service.SeriService
{
    public class SeriService : ISeriService
    {
        private ProductMove_DBContext _context;

        public SeriService(ProductMove_DBContext context)
        {
            _context = context;
        }

        public async Task<int> addSeriAsync(Seri Seri)
        {
            _context.Seri.Add(Seri);
            await _context.SaveChangesAsync();
            return Seri.idSeri;
        }

        public async Task deleteSeriAsync(int id)
        {
            var delete = _context.Seri.SingleOrDefault(a => a.idSeri == id);
            if (delete != null)
            {
                _context.Seri.Remove(delete);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Seri>> getAllSeriAsync()
        {
            return await _context.Seri.ToListAsync();
        }

        public async Task<IList<Seri>> getList_IDkho_TTSP(int idkho, string ttsp)
        {
            var result = await _context.Seri!.ToListAsync();
            var select = (
                from cust in result
                where
                    cust.Trangthaisanpham == ttsp
                    && cust.idKho == idkho
                select cust
            ).ToList();
            return select;
        }

        public async Task<Seri> getSeriAsync(int id)
        {
            var seri = await _context.Seri!.FindAsync(id);
            return seri!;
        }

        public async Task<Seri> getSeriByNameAsync(string name)
        {
            var value = await _context.Seri!.FirstOrDefaultAsync(a => a.tenSeri.Equals(name));
            return value!;
        }

        public async Task<IList<Seri>> getSpByTTSPAsync(string ttsp)
        {
            var result = await _context.Seri!.ToListAsync();
            var select = from cust in result where cust.Trangthaisanpham == ttsp select cust;
            return select.ToList();
        }

        public async Task<IList<Seri>> getTopSeriAsync(int soluong, int idsp, int idkho)
        {
            var result = await _context.Seri!
                .ToListAsync();
            var select = (
                from cust in result
                where
                    cust.Trangthaisanpham == "Máy mới"
                    && cust.idSanPham == idsp
                    && cust.idKho == idkho
                select cust
            )
                .Take(soluong)
                .ToList();
            return select;
        }

        public async Task updateSeriAsync(int id, Seri Seri)
        {
            if (id == Seri.idSeri)
            {
                _context.Seri.Update(Seri);
                await _context.SaveChangesAsync();
            }
        }
    }
}
