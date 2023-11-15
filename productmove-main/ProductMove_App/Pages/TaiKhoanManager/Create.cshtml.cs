using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using ProductMove_App.Service;
using ProductMove_Model;

namespace ProductMove_App.Pages.TaiKhoanManager
{
    public class CreateModel : PageModel
    {
        public IActionResult OnGet()
        {
            string loginCheck = HttpContext.Session.GetString("phanquyen");
            if (loginCheck == null)
            {
                return RedirectToPage("/UsersManager/Login");
            }
            return Page();
        }

        [BindProperty]
        public TaiKhoan TaiKhoan { get; set; } = default!;
        [BindProperty]
        public Kho Kho { get; set; } = default!;


        public async Task<IActionResult> OnPostAsync()
        {
            var phanquyen = Request.Form["number"];
            string check = JsonConvert.SerializeObject(phanquyen);
            var validateUserName = IsUsername(TaiKhoan.TenDangNhap);
            if (TaiKhoan.TenDangNhap == null ||
                TaiKhoan.DiaChi == null ||
                TaiKhoan.MatKhau == null ||
                check.Equals("[\"checked\"]") ||
                validateUserName == true)
            {
                ViewData["checkUername"] = "Checked true";
                return Page();
            }
            //lấy tên đăng nhập vừa nhập vào để tiến hành kiểm tra
            string taikhoan = TaiKhoan.TenDangNhap;

            //Kiểm tra xem tên đăng nhập đã tồn tại hay chưa
            var result = await TaiKhoanService.getTaiKhoanByNameAsync(taikhoan);
            if (result != null)
            {
                ViewData["checkdb"] = "Checked true";
                return Page();
            }

            //Tạo tài khoản mới
            TaiKhoan.PhanQuyen = phanquyen;
            TaiKhoan.Mail = taikhoan + "" + "@gmail.com";
            TaiKhoan.MatKhau = BCrypt.Net.BCrypt.HashPassword(TaiKhoan.MatKhau);
            await TaiKhoanService.addTaiKhoanAsync(TaiKhoan);

            //Lấy thông tin tài khoản vừa tạo
            var result_data = await TaiKhoanService.getTaiKhoanByNameAsync(taikhoan);
            int idTaiKhoan = result_data!.idTaiKhoan;

            //Tạo Kho
            Kho.idTaiKhoan = idTaiKhoan;
            Kho.SoLuongSanPham = 0;
            Kho.TaiKhoan = null;
            await KhoService.addKhoAsync(Kho);
            return RedirectToPage("./Index");
        }
        public static bool IsUsername(string username)
        {
            string pattern = " ";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(username);
        }
    }
}
