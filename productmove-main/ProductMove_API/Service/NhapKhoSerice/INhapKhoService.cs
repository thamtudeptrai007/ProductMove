using ProductMove_Model;

namespace ProductMove_API.Service.NhapKhoSerice
{
    public interface INhapKhoService
    {
        public Task<IList<NhapKho>> getAllSpPNhapKhoAsync();
        public Task<NhapKho> getSpNhapKhoAsync(int id);
        public Task<int> addSpNhapKhoAsync(NhapKho nhapKho);
        public Task updateSpNhapKhoAsync(int id, NhapKho nhapKho);
        public Task deleteSpNhapKhoAsync(int id);
    }
}
