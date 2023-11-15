using Newtonsoft.Json;
using ProductMove_Model;
using RestSharp;

namespace ProductMove_App.Service
{
    public class NhapKhoService
    {
        public static async Task addSpNhapKhoAsync(NhapKho nhapKho)
        {
            var request = new RestRequest($"/nhapkhos/addSpNhapKho", Method.Post);
            request.AddJsonBody(nhapKho);
            await Program.Client.ExecuteAsync(request);
        }
        public static async Task<IList<NhapKho>> getAllSpXuatKhoByIDKho()
        {
            var request = new RestRequest($"/nhapkhos/getAllSpNhapKho", Method.Get);
            var res = await Program.Client.ExecuteAsync(request);
            var data = res.Content!;
            var result = JsonConvert.DeserializeObject<IList<NhapKho>>(data)!;
            return result;
        }
    }
}
