using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ProductMove_Model;

namespace ProductMove_API.Service.HoaDonService
{
    public class HoaDonService : IHoaDonService
    {
        private readonly ProductMove_DBContext _context;

        public HoaDonService(ProductMove_DBContext context)
        {
            _context = context;
        }
        public async Task<int> addNewHoadon(HoaDon hoaDon)
        {
            _context.HoaDon.Add(hoaDon);
            await _context.SaveChangesAsync();
            return hoaDon.IdHoaDon;
        }

        public async Task deleteHoadon(int id)
        {
            var delete = _context.HoaDon.SingleOrDefault(a => a.IdHoaDon == id);
            if (delete != null)
            {
                _context.HoaDon.Remove(delete);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<HoaDon>> getAllHoaDon()
        {
            return await _context.HoaDon.ToListAsync();
        }

        public async Task<HoaDon> getHoaDonByid(int id)
        {
            var data = await _context.HoaDon!.FindAsync(id);
            return data!;
        }

        public async Task<HoaDon> getidhoadonnew()
        {
            var result = await _context.HoaDon.OrderByDescending(x => x.IdHoaDon).FirstAsync();
            return result;
        }

        public async Task updateHoadon(int id, HoaDon hoaDon)
        {
            if (id == hoaDon.IdHoaDon)
            {
                _context.HoaDon.Update(hoaDon);
                await _context.SaveChangesAsync();
            }
        }
    }
}
