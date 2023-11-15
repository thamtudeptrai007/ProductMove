using ProductMove_Model;

namespace ProductMove_API.Service.SeriService
{
    public interface ISeriService
    {
        public Task<List<Seri>> getAllSeriAsync();
        public Task<IList<Seri>> getSpByTTSPAsync(string ttsp);
        public Task<Seri> getSeriAsync(int id);
        public Task<IList<Seri>> getTopSeriAsync(int soluong, int idsp, int idkho);
        public Task<IList<Seri>> getList_IDkho_TTSP(int idkho, string ttsp);
        public Task<Seri> getSeriByNameAsync(string name);
        public Task<int> addSeriAsync(Seri Seri);
        public Task updateSeriAsync(int id, Seri Seri);
        public Task deleteSeriAsync(int id);
    }
}
