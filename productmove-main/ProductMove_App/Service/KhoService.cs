using Newtonsoft.Json;
using ProductMove_Model;
using RestSharp;

namespace ProductMove_App.Service
{
    public class KhoService
    {
        public static async Task<IList<ChiTietKho>> getAllSpChiTietKho()
        {
            var request = new RestRequest($"/Chitietkhos/getAllChiTietKho", Method.Get);
            var res = await Program.Client.ExecuteAsync(request);
            var data = res.Content!;
            var result = JsonConvert.DeserializeObject<IList<ChiTietKho>>(data)!;
            return result;
        }
        public static async Task<Kho?> getKhoByIdTaiKhoanAsync(int? id)
        {
            var request = new RestRequest($"/Khos/getKhoByIdTaiKhoan/{id}", Method.Get);
            var res = await Program.Client.ExecuteAsync(request);
            var data = res.Content!;
            var result = JsonConvert.DeserializeObject<Kho>(data)!;
            return result;
        }
        public static async Task<Kho?> getKhoByIdKhoAsync(string id)
        {
            var request = new RestRequest($"/Khos/getKhoById/{id}", Method.Get);
            var res = await Program.Client.ExecuteAsync(request);
            var data = res.Content!;
            var result = JsonConvert.DeserializeObject<Kho>(data)!;
            return result;
        }
        public static async Task<IList<Kho>> getKhoListAsync()
        {
            var request = new RestRequest($"/Khos/getAllKho", Method.Get);
            var res = await Program.Client.ExecuteAsync(request);
            var data = res.Content!;
            var result = JsonConvert.DeserializeObject<IList<Kho>>(data)!;
            return result;
        }
        public static async Task updateKhoAsync(int? id, Kho kho)
        {
            var request = new RestRequest($"/Khos/updateKho/{id}", Method.Put);
            request.AddJsonBody(kho);
            await Program.Client.ExecuteAsync(request);
        }
        public static async Task addKhoAsync(Kho kho)
        {
            var request = new RestRequest($"/khos/addKho", Method.Post);
            request.AddJsonBody(kho);
            await Program.Client.ExecuteAsync(request);
        }
    }
}
