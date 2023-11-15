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

namespace ProductMove_App.Pages.LoaiSanPhamManager
{
    public class IndexModel : PageModel
    {
        public IndexModel() { }
        public IList<LoaiSanPham> LoaiSanPham { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            string loginCheck = HttpContext.Session.GetString("phanquyen");
            if (loginCheck == null)
            {
                return RedirectToPage("/UsersManager/Login");
            }
            var result = await LoaiSanPhamService.getAllLoaiSanPhamAsync();
            LoaiSanPham = result;
            return Page();
        }
    }
}
