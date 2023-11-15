using Microsoft.EntityFrameworkCore;
using ProductMove_Model;
using System.Text.RegularExpressions;

namespace ProductMove_API.Service.TaiKhoanService
{
    public class TaiKhoanService : ITaiKhoanService
    {
        private readonly ProductMove_DBContext _context;

        public TaiKhoanService(ProductMove_DBContext context)
        {
            _context = context;
        }
        public async Task<int> addTaiKhoanAsync(TaiKhoan taiKhoan)
        {
            _context.TaiKhoan.Add(taiKhoan);
            await _context.SaveChangesAsync();
            return taiKhoan.idTaiKhoan;
        }

        public async Task deleteTaiKhoanAsync(int id)
        {
            var deleteTaiKhoan = _context.TaiKhoan.SingleOrDefault(a => a.idTaiKhoan == id);
            if (deleteTaiKhoan != null)
            {
                _context.TaiKhoan.Remove(deleteTaiKhoan);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<TaiKhoan>> getAllTaiKhoanAsync()
        {
            var taikhoans = await _context.TaiKhoan!.ToListAsync();
            return taikhoans;

        }

        public async Task<List<TaiKhoan>> getAllTaiKhoanbyPhanquyenAsync(string phanquyen)
        {
            var result = await _context.TaiKhoan!.ToListAsync();
            var select = from cust in result
                         where cust.PhanQuyen == phanquyen
                         select cust;
            return select.ToList();
        }

        public async Task<TaiKhoan> getTaiKhoanAsync(int id)
        {
            var taikhoans = await _context.TaiKhoan!.FindAsync(id);
            return taikhoans!;
        }

        public async Task<TaiKhoan> getTaiKhoanByNameAsync(string name)
        {
            var taikhoans = await _context.TaiKhoan!.FirstOrDefaultAsync(a => a.TenDangNhap.Equals(name));
            return taikhoans!;
        }

        public async Task updateTaiKhoanAsync(int id, TaiKhoan taiKhoan)
        {
            if (id == taiKhoan.idTaiKhoan)
            {
                _context.TaiKhoan.Update(taiKhoan);
                await _context.SaveChangesAsync();
            }
        }
    }
}
