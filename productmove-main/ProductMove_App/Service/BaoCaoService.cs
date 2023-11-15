using Newtonsoft.Json;
using ProductMove_Model;
using RestSharp;

namespace ProductMove_App.Service
{
    public class BaoCaoService
    {
        public static async Task addBaoCaoAsync(BaoCao baoCao)
        {
            var request = new RestRequest($"/baocaos/addBaoCao", Method.Post);
            request.AddJsonBody(baoCao);
            await Program.Client.ExecuteAsync(request);
        }
        public static async Task<BaoCao> getBaoCaoByID(int id)
        {
            var request = new RestRequest($"/baocaos/getBaoCaoById/{id}", Method.Get);
            var res = await Program.Client.ExecuteAsync(request);
            var data = res.Content!;
            var result = JsonConvert.DeserializeObject<BaoCao>(data)!;
            return result!;
        }
    }
}
