using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProductMove_App.Data;
using ProductMove_Model;

namespace ProductMove_App.Pages.BaoCaoManager
{
    public class IndexModel : PageModel
    {
        private readonly ProductMove_App.Data.ProductMove_AppContext _context;

        public IndexModel(ProductMove_App.Data.ProductMove_AppContext context)
        {
            _context = context;
        }

        public IList<BaoCao> BaoCao { get; set; } = default!;
        IList<BaoCao> BaoCaoList = new List<BaoCao>();
        public async Task<IActionResult> OnGetAsync()
        {
            string loginCheck = HttpContext.Session.GetString("phanquyen");
            if (loginCheck == null)
            {
                return RedirectToPage("/UsersManager/Login");
            }
            if (_context.BaoCao != null)
            {
                int idKhoCheck = Convert.ToInt32(HttpContext.Session.GetInt32("idkho"));
                var result = await _context.BaoCao.ToListAsync();
                foreach (BaoCao baoCaos in result)
                {
                    if (baoCaos.IdKho == idKhoCheck)
                    {
                        BaoCaoList.Add(baoCaos);
                    }
                }
                BaoCao = BaoCaoList;
            }
            return Page();
        }
    }
}
