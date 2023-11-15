using Newtonsoft.Json;
using ProductMove_Model;
using RestSharp;

namespace ProductMove_App.Service
{
    public class SeriService
    {
        public static async Task addSeriAsync(Seri Seri)
        {
            var request = new RestRequest($"/Seris/addSeri", Method.Post);
            request.AddJsonBody(Seri);
            await Program.Client.ExecuteAsync(request);
        }
        public static async Task<Seri> getSeriByNameAsync(string name)
        {
            var request = new RestRequest($"/Seris/getSeriByName/{name}", Method.Get);
            var res = await Program.Client.ExecuteAsync(request);
            var data = res.Content!;
            var result = JsonConvert.DeserializeObject<Seri>(data)!;
            return result;
        }
        public static async Task updateSeriAsync(int id, Seri Seri)
        {
            var request = new RestRequest($"/Seris/updateSeri/{id}", Method.Put);
            request.AddJsonBody(Seri);
            await Program.Client.ExecuteAsync(request);
        }
        public static async Task<List<Seri>> getSpByTTSPAsync(string ttsp)
        {
            var request = new RestRequest($"/Seris/getAllspByTTSP/{ttsp}", Method.Get);
            var res = await Program.Client.ExecuteAsync(request);
            var data = res.Content!;
            var result = JsonConvert.DeserializeObject<List<Seri>>(data)!;
            return result;
        }
        public static async Task<List<Seri>> getSpByTopAsync(string top, string idsp, string idkho)
        {
            var request = new RestRequest($"/Seris/getTopSpBySeri/{top}/{idsp}/{idkho}", Method.Get);
            var res = await Program.Client.ExecuteAsync(request);
            var data = res.Content!;
            var result = JsonConvert.DeserializeObject<List<Seri>>(data)!;
            return result;
        }
        public static async Task<List<Seri>> getList_IDkho_TTSP(int? idkho, string ttsp)
        {
            var request = new RestRequest($"/Seris/getList_IDkho_TTSP/{idkho}/{ttsp}", Method.Get);
            var res = await Program.Client.ExecuteAsync(request);
            var data = res.Content!;
            var result = JsonConvert.DeserializeObject<List<Seri>>(data)!;
            return result;
        }


    }
}
