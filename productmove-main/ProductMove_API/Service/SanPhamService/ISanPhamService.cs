using ProductMove_Model;

namespace ProductMove_API.Service.SanPhamService
{
    public interface ISanPhamService
    {
        public Task<IList<SanPham>> getAllSanPhamAsync();
        public Task<SanPham> getSanPhamAsync(int id);
        public Task<int> addSanPhamAsync(SanPham sanPham);
        public Task updateSanPhamAsync(int id, SanPham sanPham);
        public Task deleteSanPhamAsync(int id);
    }
}
