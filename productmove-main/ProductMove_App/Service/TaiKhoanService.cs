using Newtonsoft.Json;
using ProductMove_Model;
using RestSharp;

namespace ProductMove_App.Service
{
    public class TaiKhoanService
    {
        public static async Task<IList<TaiKhoan>> getAllTaiKhoanAsync()
        {
            var request = new RestRequest($"/taikhoans/getAllTaiKhoan", Method.Get);
            var res = await Program.Client.ExecuteAsync(request);
            var data = res.Content!;
            var result = JsonConvert.DeserializeObject<IList<TaiKhoan>>(data)!;
            return result;
        }
        public static async Task<IList<TaiKhoan>> getTaiKhoanByPhanquyenAsync(string phanquyen)
        {
            var request = new RestRequest($"/taikhoans/getTaiKhoanByPhanQuyen/{phanquyen}", Method.Get);
            var res = await Program.Client.ExecuteAsync(request);
            var data = res.Content!;
            var result = JsonConvert.DeserializeObject<IList<TaiKhoan>>(data)!;
            return result;
        }
        public static async Task<TaiKhoan> getTaiKhoanByIDAsync(int? id)
        {
            var request = new RestRequest($"/taikhoans/getTaiKhoanByID/{id}", Method.Get);
            var res = await Program.Client.ExecuteAsync(request);
            var data = res.Content!;
            var result = JsonConvert.DeserializeObject<TaiKhoan>(data)!;
            return result;
        }
        public static async Task<TaiKhoan?> getTaiKhoanByNameAsync(string name)
        {
            var request = new RestRequest($"/taikhoans/getTaiKhoanByName/{name}", Method.Get);
            var res = await Program.Client.ExecuteAsync(request);
            var data = res.Content!;
            var result = JsonConvert.DeserializeObject<TaiKhoan>(data)!;
            if (result.TenDangNhap == null)
            {
                return null;
            }
            if (result.TenDangNhap.ToLower() == name.ToLower())
            {
                return result;
            }
            return null;
        }
        public static async Task addTaiKhoanAsync(TaiKhoan taiKhoan)
        {
            var request = new RestRequest($"/taikhoans/addTaiKhoan", Method.Post);
            request.AddJsonBody(taiKhoan);
            await Program.Client.ExecuteAsync(request);
        }
        public static async Task updateTaiKhoanAsync(int? id, TaiKhoan taiKhoan)
        {
            var request = new RestRequest($"/taikhoans/updateTaiKhoan/{id}", Method.Put);
            request.AddJsonBody(taiKhoan);
            await Program.Client.ExecuteAsync(request);
        }
        public static async Task deleteTaiKhoanAsync(int? id)
        {
            var request = new RestRequest($"/taikhoans/deletetaiKhoan/{id}", Method.Delete);
            await Program.Client.ExecuteAsync(request);
        }
    }
}
