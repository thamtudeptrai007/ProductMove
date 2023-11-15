using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ProductMove_App.Data;
using ProductMove_App.Service;
using ProductMove_Model;

namespace ProductMove_App.Pages.TaiKhoanManager
{
    public class IndexModel : PageModel
    {
        public IList<TaiKhoan> TaiKhoan { get; set; } = default!;
        public Kho Kho { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            string loginCheck = HttpContext.Session.GetString("phanquyen");
            if (loginCheck == null)
            {
                return RedirectToPage("/UsersManager/Login");
            }
            var result = await TaiKhoanService.getAllTaiKhoanAsync();
            TaiKhoan = result;

            int sanxuat = 0, baohanh = 0, phanphoi = 0;
            foreach (TaiKhoan tk in result)
            {
                switch (tk.PhanQuyen)
                {
                    case "Cơ sở sản xuất": sanxuat++; break;
                    case "Nhà phân phối": phanphoi++; break;
                    case "Trung tâm bảo hành": baohanh++; break;
                }
            }
            ViewData["cssx"] = sanxuat;
            ViewData["pp"] = phanphoi;
            ViewData["bh"] = baohanh;
            return Page();
        }

    }
}