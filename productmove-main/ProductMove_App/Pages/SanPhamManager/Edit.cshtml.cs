using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ProductMove_App.Data;
using ProductMove_App.Service;
using ProductMove_Model;
using RestSharp;

namespace ProductMove_App.Pages.SanPhamManager
{
    public class EditModel : PageModel
    {
        private readonly ProductMove_AppContext _context;

        public EditModel(ProductMove_AppContext context)
        {
            _context = context;
        }

        [BindProperty]
        public SanPham SanPham { get; set; } = default!;

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
            var result = await SanPhamService.getSanPhamAsync(id);
            SanPham = result!;
            if (SanPham == null)
            {
                return NotFound();
            }
            ViewData["idLoaiSanPham"] = new SelectList(_context.Set<LoaiSanPham>(), "IdLoaiSanPham", "TenLoaiSanPham");
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
                await SanPhamService.updateSanPhamAsync(id, SanPham);
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            return RedirectToPage("./Index");
        }
    }
}
