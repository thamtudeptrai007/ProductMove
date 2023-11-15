using ProductMove_Model;

namespace ProductMove_API.Service.XuatKhoService
{
    public interface IXuatKhoService
    {
        public Task<IList<XuatKho>> getAllSpPXuatKhoAsync();
        public Task<XuatKho> getSpXuatKhoAsync(int id);
        public Task<int> addSpXuatKhoAsync(XuatKho XuatKho);
        public Task updateSpXuatKhoAsync(int id, XuatKho XuatKho);
        public Task deleteSpXuatKhoAsync(int id);
    }
}
