using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using ProductMove_App.Service;
using ProductMove_Model;
using RestSharp;

namespace ProductMove_App.Pages.TaiKhoanManager
{
    public class ThongTinTaiKhoanModel : PageModel
    {
        public ThongTinTaiKhoanModel() { }
        public List<ChiTietKho> ChiTietKho { get; set; } = default!;
        public async Task<IActionResult> OnGet(int id, string pq)
        {
            string loginCheck = HttpContext.Session.GetString("phanquyen");
            if (loginCheck == null)
            {
                return RedirectToPage("/UsersManager/Login");
            }
            ViewData["ttchitiet"] = pq;
            var result = await KhoService.getKhoByIdTaiKhoanAsync(id);
            Kho = result!;
            int idkho = result!.idKho;

            var _request = new RestRequest($"/Chitietkhos/getChiTietKhoByIdKho/{idkho}", Method.Get);
            var _res = await Program.Client.ExecuteAsync(_request);
            var _data = _res.Content!;
            var _result = JsonConvert.DeserializeObject<List<ChiTietKho>>(_data)!;
            ChiTietKho = _result!;
            int tongsp = 0, tongspmoi = 0, tongspbh = 0, tongsptrkh = 0;
            foreach (ChiTietKho ctk in _result)
            {
                tongsp++;
                switch (ctk.TrangThaiSanPham)
                {
                    case "Sản phẩm mới": tongspmoi++; break;
                    case "Sản phẩm bảo hành": tongspbh++; break;
                    case "Sản phẩm trả khách": tongsptrkh++; break;
                }
            }
            ViewData["tongsp"] = tongsp;
            ViewData["tongspmoi"] = tongspmoi;
            ViewData["tongspbh"] = tongspbh;
            ViewData["tongsptrkh"] = tongsptrkh;
            return Page();
        }
        public Kho Kho { get; set; } = default!;
    }
}
