using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProductMove_App.Service;
using ProductMove_Model;
using System.Globalization;

namespace ProductMove_App.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ProductMove_App.Data.ProductMove_AppContext _context;

        public IndexModel(ProductMove_App.Data.ProductMove_AppContext context)
        {
            _context = context;
        }
        public IList<ChiTietKho> chiTietKhos { get; set; } = default!;
        public IList<TaiKhoan> taiKhoan { get; set; } = default!;
        public IList<Kho> khos { get; set; } = default!;
        public IList<XuatKho> xuatKhos { get; set; } = default!;
        public IList<NhapKho> nhapKhos { get; set; } = default!;
        public IList<TaiKhoan> dstaikhoansx { get; set; } = new List<TaiKhoan>();
        public IList<int> dsKhosx = new List<int>();
        public IList<TaiKhoan> dstaikhoanpp = new List<TaiKhoan>();
        public IList<int> dsKhopp = new List<int>();
        public IList<TaiKhoan> dstaikhoanbh = new List<TaiKhoan>();
        public IList<int> dsKhobh = new List<int>();

        public IList<XuatKho> dsBanHangsx = new List<XuatKho>();
        public IList<NhapKho> dsSpLoi = new List<NhapKho>();

        public async Task OnGet()
        {
            if (HttpContext.Session.GetString("phanquyen") == "ADMIN")
            {
                chiTietKhos = await ChitietkhoService.getAllSpChiTietKho();
                taiKhoan = await TaiKhoanService.getAllTaiKhoanAsync();
                khos = await KhoService.getKhoListAsync();
                foreach (TaiKhoan taiKhoan in taiKhoan)
                {
                    switch (taiKhoan.PhanQuyen)
                    {
                        case "Cơ sở sản xuất":
                            {
                                dstaikhoansx.Add(taiKhoan);
                                break;
                            }
                        case "Nhà phân phối":
                            {
                                dstaikhoanpp.Add(taiKhoan);
                                break;
                            }
                        case "Trung tâm bảo hành":
                            {
                                dstaikhoanbh.Add(taiKhoan);
                                break;
                            }
                    }
                }

            }
            if (HttpContext.Session.GetString("phanquyen") == "Cơ sở sản xuất")
            {
                var idkho = HttpContext.Session.GetInt32("idkho");
                xuatKhos = await XuatKhoService.getAllSpXuatKhoByIDKho();
                nhapKhos = await NhapKhoService.getAllSpXuatKhoByIDKho();
                foreach (XuatKho xuatKho in xuatKhos)
                {
                    if (xuatKho.idKho == idkho)
                    {
                        dsBanHangsx.Add(xuatKho);
                    }
                }
                foreach (NhapKho nhapKho in nhapKhos)
                {
                    if (nhapKho.idKho == idkho)
                    {
                        dsSpLoi.Add(nhapKho);
                    }
                }

            }
        }
        public async Task<IActionResult> OnPostAsync()
        {
            int loaiThongke = Convert.ToInt32(Request.Form["LoaiBaoCao"]);
            if (loaiThongke == 1)
            {
                int thang = Convert.ToInt32(Request.Form["thang"]);
                int nam = Convert.ToInt32(Request.Form["ThoiGian"]);
                var idkho = HttpContext.Session.GetInt32("idkho");
                xuatKhos = await XuatKhoService.getAllSpXuatKhoByIDKho();
                nhapKhos = await NhapKhoService.getAllSpXuatKhoByIDKho();
                foreach (XuatKho xuatKho in xuatKhos)
                {
                    if (xuatKho.idKho == idkho)
                    {
                        int thang_ = DateTime.ParseExact(xuatKho.NgayXuat, "dd/MM/yyyy", CultureInfo.InvariantCulture).Month;
                        int nam_ = DateTime.ParseExact(xuatKho.NgayXuat, "dd/MM/yyyy", CultureInfo.InvariantCulture).Year;
                        if (nam == nam_ && thang == thang_)
                        {
                            dsBanHangsx.Add(xuatKho);
                        }

                    }
                }
                foreach (NhapKho nhapKho in nhapKhos)
                {
                    if (nhapKho.idKho == idkho)
                    {
                        int thang_ = DateTime.ParseExact(nhapKho.NgayNhap, "dd/MM/yyyy", CultureInfo.InvariantCulture).Month;
                        int nam_ = DateTime.ParseExact(nhapKho.NgayNhap, "dd/MM/yyyy", CultureInfo.InvariantCulture).Year;
                        if (nam == nam_ && thang == thang_)
                        {
                            dsSpLoi.Add(nhapKho);
                        }
                    }
                }
            }
            else if (loaiThongke == 2)
            {

            }
            else
            {

            }
            return Page();
        }
        public IActionResult OnGetLogout()

        {
            HttpContext.Session.Clear();
            return RedirectToPage("/UsersManager/Login");
        }
    }
}