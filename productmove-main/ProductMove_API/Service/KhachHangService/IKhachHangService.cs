using ProductMove_Model;

namespace ProductMove_API.Service.KhachHangService
{
    public interface IKhachHangService
    {
        public Task<List<KhachHang>> getAllKhachHangAsync();
        public Task<KhachHang> getKhachHangByIdAsync(int id);
        public Task<KhachHang> getKhachHangBySDTAsync(string sdt);
        public Task<int> addNewKhachHangAsync(KhachHang khachHang);
        public Task updateThongtinKhachHang(int id, KhachHang khachHang);
    }
}
