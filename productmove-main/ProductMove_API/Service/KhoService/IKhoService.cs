using ProductMove_Model;

namespace ProductMove_API.Service.KhoService
{
    public interface IKhoService
    {
        public Task<List<Kho>> getAllKhoAsync();
        public Task<Kho> getKhoAsync(int id);
        public Task<Kho> getKhoByIdTaiKhoanAsync(int id);
        public Task<int> addKhoAsync(Kho kho);
        public Task updateKhoAsync(int id, Kho kho);
        public Task deleteKhoAsync(int id);
    }
}
