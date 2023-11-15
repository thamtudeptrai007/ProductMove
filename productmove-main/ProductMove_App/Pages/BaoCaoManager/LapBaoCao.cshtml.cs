using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProductMove_App.Service;
using ProductMove_Model;

namespace ProductMove_App.Pages.BaoCaoManager
{
    public class LapBaoCaoModel : PageModel
    {

        public async Task<IActionResult> OnGet()
        {
            string loginCheck = HttpContext.Session.GetString("phanquyen");
            if (loginCheck == null)
            {
                return RedirectToPage("/UsersManager/Login");
            }
            return Page();
        }
        [BindProperty]
        public BaoCao baoCao { get; set; } = default!;
        public async Task<IActionResult> OnPostAsync()
        {
            var idKho = HttpContext.Session.GetInt32("idkho");
            baoCao.LoaiBaoCao = Convert.ToInt32(Request.Form["LoaiBaoCao"]);
            if (baoCao.LoaiBaoCao == 2)
            {
                baoCao.ThoiGian = Request.Form["ThoiGian"].ToString();
            }
            else
            {
                baoCao.ThoiGian = Request.Form["thang"].ToString() + "/" + Request.Form["ThoiGian"].ToString();
            }
            baoCao.IdKho = Convert.ToInt32(idKho);
            if (!ModelState.IsValid)
            {
                return Page();
            }
            await BaoCaoService.addBaoCaoAsync(baoCao);
            return RedirectToPage("./Index");
        }

    }
}
