using ProductMove_Model;

namespace ProductMove_API.Service.BaoCaoService
{
    public interface IBaoCaoService
    {
        public Task<IList<BaoCao>> getAllBaoCaoAsync();
        public Task<BaoCao> getBaoCaoByIdAsync(int id);
        public Task<int> addBaoCaoAsync(BaoCao baoCao);
        public Task updateBaoCaoAsync(int id , BaoCao baoCao);
        public Task deleteBaoCaoAsync(int id);
    }
}
