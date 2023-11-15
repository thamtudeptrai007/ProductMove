using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ProductMove_App.Data;
using ProductMove_App.Service;
using ProductMove_Model;
using RestSharp;


namespace ProductMove_App.Pages.LoaiSanPhamManager
{
    public class EditModel : PageModel
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
            if (id == null)
            {
                return NotFound();
            }

            var result = await LoaiSanPhamService.getLoaiSanPhamByIDAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            LoaiSanPham = result;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            try
            {
                await LoaiSanPhamService.updateLoaiSanPhamAsync(id, LoaiSanPham);
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return RedirectToPage("./Index");
        }
    }
}
