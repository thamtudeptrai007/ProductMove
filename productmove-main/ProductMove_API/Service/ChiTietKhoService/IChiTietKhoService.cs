using ProductMove_Model;
namespace ProductMove_API.Service.ChiTietKhoService
{
    public interface IChiTietKhoService
    {
        public Task<List<ChiTietKho>> getAllChiTietKhoAsync();
        public Task<List<ChiTietKho>> getAllSpByIdKhoAsync(int idKho);
        public Task<List<ChiTietKho>> getAllSpByTTSPAsync(string trangthaisanpham);
        public Task<ChiTietKho> getAllSpByIdSPAsync(int idsp);
        public Task<ChiTietKho> getChiTietKhoAsync(int id);
        public Task<ChiTietKho> getChiTietKhoByObjectAsync(int idkho, string ttsp, int idsp);
        public Task<ChiTietKho> getChiTietKhoByObject_2Async(int idkho, string ttsp);
        public Task<int> addChiTietKhoAsync(ChiTietKho chiTietKho);
        public Task updateChiTietKhoAsync(int id, ChiTietKho chiTietKho);
        public Task deleteChiTietKhoAsync(int id);
    }
}
