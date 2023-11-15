using Newtonsoft.Json;
using ProductMove_Model;
using RestSharp;

namespace ProductMove_App.Service
{
    public class LoaiSanPhamService
    {
        public static async Task<IList<LoaiSanPham>> getAllLoaiSanPhamAsync()
        {
            var request = new RestRequest($"/LoaiSanphams/getAllLoaiSanPham", Method.Get);
            var res = await Program.Client.ExecuteAsync(request);
            var data = res.Content!;
            var result = JsonConvert.DeserializeObject<IList<LoaiSanPham>>(data)!;
            return result;
        }

        public static async Task<LoaiSanPham> getLoaiSanPhamByIDAsync(int? id)
        {
            var request = new RestRequest($"/loaisanphams/getLoaiSanPhamByID/{id}", Method.Get);
            var res = await Program.Client.ExecuteAsync(request);
            var data = res.Content!;
            var result = JsonConvert.DeserializeObject<LoaiSanPham>(data)!;
            return result;
        }
        public static async Task<LoaiSanPham?> getLoaiSanPhamByNameAsync(string? name)
        {
            var request = new RestRequest($"/loaisanphams/getLoaiSanPhamByName/{name}", Method.Get);
            var res = await Program.Client.ExecuteAsync(request);
            var data = res.Content!;
            var result = JsonConvert.DeserializeObject<LoaiSanPham>(data)!;
            if (result.TenLoaiSanPham == name)
            {
                return result;
            }
            return null;
        }
        public static async Task addLoaiSanPhamAsync(LoaiSanPham loaiSanPham)
        {
            var request = new RestRequest($"/Loaisanphams/addLoaiSanPham", Method.Post);
            request.AddJsonBody(loaiSanPham);
            await Program.Client.ExecuteAsync(request);
        }
        public static async Task deleteLoaiSanPhamAsync(int? id)
        {
            var request = new RestRequest($"/loaisanphams/deleteLoaiSanPham/{id}", Method.Delete);
            await Program.Client.ExecuteAsync(request);
        }
        public static async Task updateLoaiSanPhamAsync(int? id, LoaiSanPham loaiSanPham)
        {
            var request = new RestRequest($"/loaisanphams/updateLoaiSanPham/{id}", Method.Put);
            request.AddJsonBody(loaiSanPham);
            await Program.Client.ExecuteAsync(request);
        }
    }
}
