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

namespace ProductMove_App.Pages.LoaiSanPhamManager
{
    public class CreateModel : PageModel
    {
        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public LoaiSanPham LoaiSanPham { get; set; } = default!;
        public async Task<IActionResult> OnPostAsync()
        {
            string loginCheck = HttpContext.Session.GetString("phanquyen");
            if (loginCheck == null)
            {
                return RedirectToPage("/UsersManager/Login");
            }
            if (!ModelState.IsValid)
            {
                return Page();
            }
            string name = LoaiSanPham.TenLoaiSanPham;
            //Kiểm tra xem loại sản phẩm đã tồn tại hay chưa
            var result = await LoaiSanPhamService.getLoaiSanPhamByNameAsync(name);
            if (result != null)
            {
                ViewData["checkdb"] = "Checked true";
                return Page();
            }

            await LoaiSanPhamService.addLoaiSanPhamAsync(LoaiSanPham);
            return RedirectToPage("./Index");
        }
    }
}
