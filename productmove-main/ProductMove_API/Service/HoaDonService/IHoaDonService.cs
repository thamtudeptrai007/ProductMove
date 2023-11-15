using ProductMove_Model;

namespace ProductMove_API.Service.HoaDonService
{
    public interface IHoaDonService
    {
        public Task<List<HoaDon>> getAllHoaDon();
        public Task<HoaDon> getHoaDonByid(int id);
        public Task<int> addNewHoadon(HoaDon hoaDon);
        public Task updateHoadon(int id, HoaDon hoaDon);
        public Task deleteHoadon(int id);
        public Task<HoaDon> getidhoadonnew();
    }
}
