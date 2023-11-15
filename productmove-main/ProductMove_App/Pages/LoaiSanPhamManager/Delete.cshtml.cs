using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProductMove_App.Service;
using ProductMove_Model;

namespace ProductMove_App.Pages.LoaiSanPhamManager
{
    public class DeleteModel : PageModel
    {
        [BindProperty]
        public LoaiSanPham LoaiSanPham { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            string loginCheck = HttpContext.Session.GetString("phanquyen");
            if (loginCheck == null)
            {
                return RedirectToPage("/UsersManager/Login");
            }
            var result = await LoaiSanPhamService.getLoaiSanPhamByIDAsync(id);

            if (result == null)
            {
                return NotFound();
            }
            else
            {
                LoaiSanPham = result;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            await LoaiSanPhamService.deleteLoaiSanPhamAsync(id);
            return RedirectToPage("./Index");
        }
    }
}
