using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using ProductMove_App.Data;
using ProductMove_App.Service;
using ProductMove_Model;
using RestSharp;

namespace ProductMove_App.Pages.SanPhamManager
{
    public class CreateModel : PageModel
    {
        private readonly ProductMove_AppContext _context;

        public CreateModel(ProductMove_App.Data.ProductMove_AppContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            string loginCheck = HttpContext.Session.GetString("phanquyen");
            if (loginCheck == null)
            {
                return RedirectToPage("/UsersManager/Login");
            }
            ViewData["idLoaiSanPham"] = new SelectList(_context.Set<LoaiSanPham>(), "IdLoaiSanPham", "TenLoaiSanPham");
            return Page();
        }

        [BindProperty]
        public SanPham SanPham { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            await SanPhamService.addSanPhamAsync(SanPham);
            return RedirectToPage("./Index");
        }
    }
}
