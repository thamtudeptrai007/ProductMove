using Microsoft.EntityFrameworkCore;
using ProductMove_Model;

namespace ProductMove_API.Service.BaoCaoService
{
    public class BaoCaoService : IBaoCaoService
    {
        private readonly ProductMove_DBContext _context;
        public BaoCaoService(ProductMove_DBContext context)
        {
            _context = context;
        }
        public async Task<int> addBaoCaoAsync(BaoCao baoCao)
        {
            _context.BaoCao.Add(baoCao);
            await _context.SaveChangesAsync();
            return baoCao.IdBaoCao;
        }

        public async Task deleteBaoCaoAsync(int id)
        {
            var deleteBaoCao = _context.BaoCao.SingleOrDefault(a => a.IdBaoCao == id);
            if(deleteBaoCao != null)
            {
                _context.BaoCao.Remove(deleteBaoCao);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IList<BaoCao>> getAllBaoCaoAsync()
        {
            var baocaos = await _context.BaoCao!.ToListAsync();
            return baocaos;
        }

        public async Task<BaoCao> getBaoCaoByIdAsync(int id)
        {
            var baocao = await _context.BaoCao!.FindAsync(id);
            return baocao!;
        }

        public async Task updateBaoCaoAsync(int id, BaoCao baoCao)
        {
            if(id == baoCao.IdBaoCao)
            {
                _context.BaoCao.Update(baoCao);
                await _context.SaveChangesAsync();
            }
        }
    }
}
