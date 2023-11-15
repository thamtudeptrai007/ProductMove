using Newtonsoft.Json;
using ProductMove_Model;
using RestSharp;

namespace ProductMove_App.Service
{
    public class SanPhamService
    {
        public static async Task<IList<SanPham?>> GetAllSanPham()
        {
            var request = new RestRequest($"/Sanphams/getAllSanPham", Method.Get);
            var res = await Program.Client.ExecuteAsync(request);
            var data = res.Content!;
            var result = JsonConvert.DeserializeObject<IList<SanPham>>(data)!;
            return result!;
        }
        public static async Task<SanPham?> getSanPhamAsync(int? id)
        {
            var request = new RestRequest($"/Sanphams/getSanPhamById/{id}", Method.Get);
            var res = await Program.Client.ExecuteAsync(request);
            var data = res.Content!;
            var result = JsonConvert.DeserializeObject<SanPham>(data)!;
            return result!;
        }
        public static async Task addSanPhamAsync(SanPham sanPham)
        {
            var request = new RestRequest($"/sanphams/addSanPham", Method.Post);
            request.AddJsonBody(sanPham);
            await Program.Client.ExecuteAsync(request);
        }
        public static async Task deleteSanPhamAsync(int? id)
        {
            var request = new RestRequest($"/sanphams/deleteSanPham/{id}", Method.Delete);
            await Program.Client.ExecuteAsync(request);
        }
        public static async Task updateSanPhamAsync(int? id, SanPham sanPham)
        {
            var request = new RestRequest($"/sanphams/updateSanPham/{id}", Method.Put);
            request.AddJsonBody(sanPham);
            await Program.Client.ExecuteAsync(request);
        }
    }
}
