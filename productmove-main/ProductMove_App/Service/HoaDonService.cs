using Newtonsoft.Json;
using ProductMove_Model;
using RestSharp;

namespace ProductMove_App.Service
{
    public class HoaDonService
    {
        public static async Task updateChitietHoaDon(int? id, HoaDon HoaDon)
        {
            var request = new RestRequest($"/HoaDons/updateHoadon/{id}", Method.Put);
            request.AddJsonBody(HoaDon);
            await Program.Client.ExecuteAsync(request);
        }

        public static async Task newHoaDon(HoaDon HoaDon)
        {
            var request = new RestRequest($"/HoaDons/addNewHoadon", Method.Post);
            request.AddJsonBody(HoaDon);
            await Program.Client.ExecuteAsync(request);
        }
        public static async Task<IList<HoaDon>> getAllHoaDon()
        {
            var request = new RestRequest($"/HoaDons/getAllHoaDon", Method.Get);
            var res = await Program.Client.ExecuteAsync(request);
            var data = res.Content!;
            var result = JsonConvert.DeserializeObject<IList<HoaDon>>(data)!;
            return result;
        }
        public static async Task<HoaDon> GetHoaDonByID(int? id)
        {
            var request = new RestRequest($"/HoaDons/getHoaDonByid/{id}", Method.Get);
            var res = await Program.Client.ExecuteAsync(request);
            var data = res.Content!;
            var result = JsonConvert.DeserializeObject<HoaDon>(data)!;
            return result;
        }
        public static async Task<HoaDon> GetIDHoaDonNew()
        {
            var request = new RestRequest($"/HoaDons/getidhoadonnew", Method.Get);
            var res = await Program.Client.ExecuteAsync(request);
            var data = res.Content!;
            var result = JsonConvert.DeserializeObject<HoaDon>(data)!;
            return result;
        }
    }
}
