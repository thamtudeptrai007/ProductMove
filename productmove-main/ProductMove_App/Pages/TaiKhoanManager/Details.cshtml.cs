using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProductMove_App.Service;
using ProductMove_Model;

namespace ProductMove_App.Pages.TaiKhoanManager
{
    public class DetailsModel : PageModel
    {
        public TaiKhoan TaiKhoan { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            string loginCheck = HttpContext.Session.GetString("phanquyen");
            if (loginCheck == null)
            {
                return RedirectToPage("/UsersManager/Login");
            }
            if (id == null)
            {
                return NotFound();
            }
            var result = await TaiKhoanService.getTaiKhoanByIDAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            else
            {
                TaiKhoan = result;
            }
            return Page();
        }
    }
}
