using Newtonsoft.Json;
using ProductMove_Model;
using RestSharp;

namespace ProductMove_App.Service
{
    public class XuatKhoService
    {
        public static async Task addSpXuatKhoAsync(XuatKho xuatKho)
        {
            var request = new RestRequest($"/xuatkhos/addSpXuatKho", Method.Post);
            request.AddJsonBody(xuatKho);
            await Program.Client.ExecuteAsync(request);
        }
        public static async Task<IList<XuatKho>> getAllSpXuatKhoByIDKho()
        {
            var request = new RestRequest($"/xuatkhos/getAllSpXuatKho", Method.Get);
            var res = await Program.Client.ExecuteAsync(request);
            var data = res.Content!;
            var result = JsonConvert.DeserializeObject<IList<XuatKho>>(data)!;
            return result;
        }
    }
}
