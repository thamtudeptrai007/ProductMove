using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ProductMove_App.Pages.SanPhamManager
{
    public class KhachHangModel : PageModel
    {
        public async Task<IActionResult> OnGet()
        {
            string loginCheck = HttpContext.Session.GetString("phanquyen");
            if (loginCheck == null)
            {
                return RedirectToPage("/UsersManager/Login");
            }
            return Page();
        }
    }
}
