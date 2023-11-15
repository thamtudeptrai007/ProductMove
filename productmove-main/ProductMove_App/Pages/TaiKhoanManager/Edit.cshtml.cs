using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ProductMove_App.Service;
using ProductMove_Model;

namespace ProductMove_App.Pages.TaiKhoanManager
{
    public class EditModel : PageModel
    {
        [BindProperty]
        public TaiKhoan TaiKhoan { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            string loginCheck = HttpContext.Session.GetString("phanquyen");
            if (loginCheck == null)
            {
                return RedirectToPage("/UsersManager/Login");
            }
            var result = await TaiKhoanService.getTaiKhoanByIDAsync(id);
            TaiKhoan = result;
            if (TaiKhoan.PhanQuyen == "Nhà phân phối")
            {
                ViewData["npp"] = "True";
            }
            if (TaiKhoan.PhanQuyen == "Cơ sở sản xuất")
            {
                ViewData["cssx"] = "True";
            }
            else
            {
                ViewData["ttbh"] = "True";
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            var phanquyen = Request.Form["number"];
            string check = JsonConvert.SerializeObject(phanquyen);
            var validateUserName = IsUsername(TaiKhoan.TenDangNhap);
            if (TaiKhoan.TenDangNhap == null ||
                TaiKhoan.DiaChi == null ||
                TaiKhoan.MatKhau == null ||
                TaiKhoan.Mail == null ||
                check.Equals("[\"checked\"]") ||
                validateUserName == true)
            {
                ViewData["checkUername"] = "Checked true";
                return Page();
            }
            try
            {
                TaiKhoan.PhanQuyen = phanquyen;
                await TaiKhoanService.updateTaiKhoanAsync(id, TaiKhoan);
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
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
