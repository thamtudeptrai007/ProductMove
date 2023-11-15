using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using ProductMove_App.Service;
using ProductMove_Model;
using RestSharp;

namespace ProductMove_App.Pages.SanPhamManager
{
    public class IndexModel : PageModel
    {
        private readonly ProductMove_App.Data.ProductMove_AppContext _context;

        public IndexModel(ProductMove_App.Data.ProductMove_AppContext context)
        {
            _context = context;
        }

        public IList<SanPham> SanPham { get; set; } = default!;
        public IList<ChiTietKho> ChiTietKho { get; set; } = default!;
        public Kho Kho { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            string loginCheck = HttpContext.Session.GetString("phanquyen");
            if (loginCheck == null)
            {
                return RedirectToPage("/UsersManager/Login");
            }
            var _phanquyen = HttpContext.Session.GetString("phanquyen")!.ToString();
            if (_phanquyen == "ADMIN")
            {
                var result = await SanPhamService.GetAllSanPham()!;
                SanPham = result!;
            }
            else
            {
                //Lấy thông tin kho từ tài khoản đang đăng nhập
                var _id = HttpContext.Session.GetInt32("idtaikhoan");
                var result = await KhoService.getKhoByIdTaiKhoanAsync(_id);
                Kho = result!;
                int? idkho = result!.idKho;


                //Lấy thông tin các sản phẩm nằm trong kho 
                var _request = new RestRequest($"/Chitietkhos/getChiTietKhoByIdKho/{idkho}", Method.Get);
                var _res = await Program.Client.ExecuteAsync(_request);
                var _data = _res.Content!;
                var _result = JsonConvert.DeserializeObject<List<ChiTietKho>>(_data)!;
                ChiTietKho = _result!;
            }
            return Page();
        }
        public IActionResult OnGetLogout()
        {
            HttpContext.Session.Clear();
            return RedirectToPage("/UsersManager/Login");
        }
    }
}
