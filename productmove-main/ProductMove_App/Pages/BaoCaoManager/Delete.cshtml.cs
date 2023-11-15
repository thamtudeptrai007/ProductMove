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
    public class DeleteModel : PageModel
    {
        private readonly ProductMove_AppContext _context;

        public DeleteModel(ProductMove_AppContext context)
        {
            _context = context;
        }

        [BindProperty]
        public BaoCao BaoCao { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id_)
        {
            string loginCheck = HttpContext.Session.GetString("phanquyen");
            if (loginCheck == null)
            {
                return RedirectToPage("/UsersManager/Login");
            }

            if (id_ == null || _context.BaoCao == null)
            {
                return NotFound();
            }

            var baocao = await _context.BaoCao.FirstOrDefaultAsync(m => m.IdBaoCao == id_);

            if (baocao == null)
            {
                return NotFound();
            }
            else
            {
                BaoCao = baocao;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id_)
        {
            if (id_ == null || _context.BaoCao == null)
            {
                return NotFound();
            }
            var baocao = await _context.BaoCao.FindAsync(id_);

            if (baocao != null)
            {
                BaoCao = baocao;
                _context.BaoCao.Remove(BaoCao);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
