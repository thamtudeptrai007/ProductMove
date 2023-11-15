using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProductMove_App.Service;
using ProductMove_Model;

namespace ProductMove_App.Pages.UsersManager
{
    public class LoginModel : PageModel
    {
        public LoginModel()
        {
        }
        [BindProperty]
        public TaiKhoan? taiKhoan { get; set; } = default!;
        public string? message;
        public async Task<IActionResult> OnPostAsync()
        {
            string taikhoan = taiKhoan!.TenDangNhap;
            string matkhau = taiKhoan.MatKhau;
            var result = await TaiKhoanService.getTaiKhoanByNameAsync(taikhoan);


            if (result != null)
            {
                if (BCrypt.Net.BCrypt.Verify(matkhau, result.MatKhau))
                {
                    int id = result.idTaiKhoan;
                    var result1 = await KhoService.getKhoByIdTaiKhoanAsync(id);
                    HttpContext.Session.SetString("tendangnhap", result.TenDangNhap);
                    HttpContext.Session.SetInt32("idtaikhoan", result.idTaiKhoan);
                    HttpContext.Session.SetString("phanquyen", result.PhanQuyen);
                    HttpContext.Session.SetString("diachi", result.DiaChi);
                    ViewData["pq"] = HttpContext.Session.GetString("phanquyen");
                    HttpContext.Session.SetInt32("idkho", result1!.idKho);
                    if (HttpContext.Session.GetString("phanquyen").Equals("ADMIN"))
                    {
                        return RedirectToPage("/Index");
                    }
                    else
                    {
                        return RedirectToPage("/SanPhamManager/Index");
                    }
                }
                else
                {
                    ViewData["saipass"] = "True";
                    return Page();
                }
            }
            else
            {
                ViewData["saipass"] = "True";
                return Page();
            }
        }
        public void OnGet()
        {
            HttpContext.Session.Clear();
        }
    }
}
