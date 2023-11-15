using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ProductMove_App.Data;
using ProductMove_App.Service;
using ProductMove_Model;
using RestSharp;

namespace ProductMove_App.Pages.SanPhamManager
{
    public class DetailsModel : PageModel
    {
        private readonly ProductMove_AppContext _context;

        public DetailsModel(ProductMove_AppContext context)
        {
            _context = context;
        }

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
            if (result == null)
            {
                return NotFound();
            }
            else
            {
                SanPham = result;
            }
            return Page();
        }
    }
}
