using Newtonsoft.Json;
using ProductMove_Model;
using RestSharp;

namespace ProductMove_App.Service
{
    public class ChitietkhoService
    {
        public static async Task<IList<ChiTietKho>> getAllSpChiTietKho()
        {
            var request = new RestRequest($"/Chitietkhos/getAllChiTietKho", Method.Get);
            var res = await Program.Client.ExecuteAsync(request);
            var data = res.Content!;
            var result = JsonConvert.DeserializeObject<IList<ChiTietKho>>(data)!;
            return result;
        }
        public static async Task<ChiTietKho> getAllSpByIdSPAsync(int? idsp)
        {
            var request = new RestRequest($"/Chitietkhos/getChiTietKhoByIDSP/{idsp}", Method.Get);
            var res = await Program.Client.ExecuteAsync(request);
            var data = res.Content!;
            var result = JsonConvert.DeserializeObject<ChiTietKho>(data)!;
            return result;
        }

        public static async Task<ChiTietKho> getChiTietKhoByObject(int? idkho, string ttsp, int idsp)
        {
            var request = new RestRequest($"/Chitietkhos/getChiTietKhoByObject/{idkho}/{ttsp}/{idsp}", Method.Get);
            var res = await Program.Client.ExecuteAsync(request);
            var data = res.Content!;
            var result = JsonConvert.DeserializeObject<ChiTietKho>(data)!;
            return result;
        }
        public static async Task<IList<ChiTietKho>> getChiTietKhoByidkhottsp(int? idkho, string ttsp)
        {
            var request = new RestRequest($"/Chitietkhos/getChiTietKhoByidkhottsp/{idkho}/{ttsp}", Method.Get);
            var res = await Program.Client.ExecuteAsync(request);
            var data = res.Content!;
            var result = JsonConvert.DeserializeObject<IList<ChiTietKho>>(data)!;
            return result;
        }
        public static async Task updateChiTietKhoAsync(int? id, ChiTietKho chiTietKho)
        {
            var request = new RestRequest($"/Chitietkhos/updateChiTietKho/{id}", Method.Put);
            request.AddJsonBody(chiTietKho);
            await Program.Client.ExecuteAsync(request);
        }
        public static async Task addChiTietKhoAsync(ChiTietKho chiTietKho)
        {
            var request = new RestRequest($"/Chitietkhos/addChiTietKho", Method.Post);
            request.AddJsonBody(chiTietKho);
            await Program.Client.ExecuteAsync(request);
        }
        public static async Task<IList<ChiTietKho>> getAllSpByTTSPAsync(string trangthaisanpham)
        {
            var request = new RestRequest($"/Chitietkhos/getAllSpByTTSP/{trangthaisanpham}", Method.Get);
            var res = await Program.Client.ExecuteAsync(request);
            var data = res.Content!;
            var result = JsonConvert.DeserializeObject<IList<ChiTietKho>>(data)!;
            return result;
        }
        public static async Task<IList<ChiTietKho>> getAllSpByIdKhoAsync(int idKho)
        {
            var request = new RestRequest($"/Chitietkhos/getChiTietKhoByIdKho/{idKho}", Method.Get);
            var res = await Program.Client.ExecuteAsync(request);
            var data = res.Content!;
            var result = JsonConvert.DeserializeObject<IList<ChiTietKho>>(data)!;
            return result;
        }
        public static async Task<ChiTietKho> getChiTietKhoByIDSP(int idsp)
        {
            var request = new RestRequest($"/Chitietkhos/getChiTietKhoByIDSP/{idsp}", Method.Get);
            var res = await Program.Client.ExecuteAsync(request);
            var data = res.Content!;
            var result = JsonConvert.DeserializeObject<ChiTietKho>(data)!;
            return result;
        }
        public static async Task deleteChiTietKho(int id)
        {
            var request = new RestRequest($"/Chitietkhos/deleteChiTietKho/{id}", Method.Delete);
            await Program.Client.ExecuteAsync(request);
        }

    }
}
