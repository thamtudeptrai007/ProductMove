using Newtonsoft.Json;
using ProductMove_Model;
using RestSharp;

namespace ProductMove_App.Service
{
    public class KhachHangService
    {
        public static async Task updateThongTinKhachHang(int? id, KhachHang khachhang)
        {
            var request = new RestRequest($"/KhachHangs/updateThongtinKhachHang/{id}", Method.Put);
            request.AddJsonBody(khachhang);
            await Program.Client.ExecuteAsync(request);
        }
        public static async Task addKhachHangAsync(KhachHang khachhang)
        {
            var request = new RestRequest($"/KhachHangs/addKhachHang", Method.Post);
            request.AddJsonBody(khachhang);
            await Program.Client.ExecuteAsync(request);
        }
        public static async Task<IList<KhachHang>> getAllkhachHang()
        {
            var request = new RestRequest($"/KhachHangs/getAllKhachHang", Method.Get);
            var res = await Program.Client.ExecuteAsync(request);
            var data = res.Content!;
            var result = JsonConvert.DeserializeObject<IList<KhachHang>>(data)!;
            return result;
        }
        public static async Task<KhachHang> getKhachHangById(int? id)
        {
            var request = new RestRequest($"/KhachHangs/getKhachHangById/{id}", Method.Get);
            var res = await Program.Client.ExecuteAsync(request);
            var data = res.Content!;
            var result = JsonConvert.DeserializeObject<KhachHang>(data)!;
            return result;
        }
        public static async Task<KhachHang> getKhachHangBySDT(string? id)
        {
            var request = new RestRequest($"/KhachHangs/getKhachHangBySDT/{id}", Method.Get);
            var res = await Program.Client.ExecuteAsync(request);
            var data = res.Content!;
            var result = JsonConvert.DeserializeObject<KhachHang>(data)!;
            return result;
        }
    }
}
