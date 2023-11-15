using ProductMove_Model;

namespace ProductMove_API.Service.TaiKhoanService
{
    public interface ITaiKhoanService
    {
        public Task<List<TaiKhoan>> getAllTaiKhoanAsync();
        public Task<List<TaiKhoan>> getAllTaiKhoanbyPhanquyenAsync(string phanquyen);
        public Task<TaiKhoan> getTaiKhoanAsync(int id);
        public Task<TaiKhoan> getTaiKhoanByNameAsync(string id);
        public Task<int> addTaiKhoanAsync(TaiKhoan taiKhoan);
        public Task updateTaiKhoanAsync(int id, TaiKhoan taiKhoan);
        public Task deleteTaiKhoanAsync(int id);

    }
}
