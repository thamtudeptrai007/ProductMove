using ProductMove_Model;
namespace ProductMove_API.Service.LoaiSanPhamService
{
    public interface ILoaiSanPhamService
    {
        public Task<List<LoaiSanPham>> getAllLoaiSanPhamAsync();
        public Task<LoaiSanPham> getLoaiSanPhamAsync(int id);
        public Task<LoaiSanPham> getLoaiSanPhamByName(string name);
        public Task<int> addLoaiSanPhamAsync(LoaiSanPham loaiSanPham);
        public Task updateLoaiSanPhamAsync(int id, LoaiSanPham loaiSanPham);
        public Task deleteLoaiSanPhamAsync(int id);
    }
}
