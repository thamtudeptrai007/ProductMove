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
    public class NhapKhoCSSXModel : PageModel
    {
        private readonly ProductMove_AppContext _context;
        DateTime now = DateTime.Now;
        public NhapKhoCSSXModel(ProductMove_AppContext context)
        {
            _context = context;
        }
        //Các biến random seri cho sản phẩm khi được cơ sở sản xuất tạo ra
        string value = "";
        Random random = new Random();
        char[] list = new char[] { 'p', 'r', 'o', 'd', 'u', 'c', 't', 'm', 'o', 'v', 'e', '1', '2', '3', '4', '5', '6', '7', '8', '9', '0' };

        [BindProperty]
        public NhapKho NhapKho { get; set; } = default!;
        [BindProperty]
        public SanPham SanPham { get; set; } = default!;
        [BindProperty]
        public Kho Kho { get; set; } = default!;
        [BindProperty]
        public Seri Seri { get; set; } = default!;
        [BindProperty]
        public ChiTietKho ChiTietKho { get; set; } = default!;
        public const string MAYMOI = "Máy mới";
        public IActionResult OnGet()
        {
            string loginCheck = HttpContext.Session.GetString("phanquyen");
            if (loginCheck == null)
            {
                return RedirectToPage("/UsersManager/Login");
            }
            ViewData["modelsp"] = new SelectList(_context.Set<SanPham>(), "idSanPham", "TenSanPham");
            return Page();
        }
        // Nhập kho sau khi cơ sở sản xuất tiến hành sản xuất ra máy mới
        public async Task<IActionResult> OnPostAsync()
        {
            //lấy thông tin kho từ tài khoản vừa đăng nhập
            var _id = HttpContext.Session.GetInt32("idtaikhoan");
            int? id = _id;
            var result = await KhoService.getKhoByIdTaiKhoanAsync(id);
            Kho = result!;
            //Lấy id kho để lưu lại thông tin sản phẩm vừa nhập đã vào kho đó
            int idkho = result!.idKho;

            //Lưu thông tin sản phẩm vừa nhập lại
            NhapKho.NgayNhap = now.ToString("dd/MM/yyyy");
            NhapKho.idKho = idkho;
            await NhapKhoService.addSpNhapKhoAsync(NhapKho);

            //với phân quyền là cơ sở sản xuất thì sẽ tiến hành tạo dữ liệu về seri sản phẩm
            var phanquyen = HttpContext.Session.GetString("phanquyen");


            if (phanquyen == "Cơ sở sản xuất")
            {
                //Kiểm tra số lượng sản phẩm nhập kho để tiến hành tạo seri cho từng máy
                for (int i = 1; i <= NhapKho.SoLuong; i++)
                {
                    var seri = ranDomSeri();
                    Seri.tenSeri = seri.ToUpper();
                    Seri.ThoiGianSanXuat = now.ToString("dd/MM/yyyy");
                    Seri.idSanPham = NhapKho.idSanPham;
                    Seri.idKho = idkho;
                    Seri.Trangthaisanpham = MAYMOI;
                    await SeriService.addSeriAsync(Seri);
                    value = "";
                }
                //kiểm tra xem trong kho đã tồn tại loại sản phẩm đó với trạng thái là máy mới hay chưa
                //nếu có thì tiến hành update số lượng mà khồng cần thêm chi tiết sản phẩm đó vào kho
                var idsanpham = NhapKho.idSanPham;
                var result_ctk = await ChitietkhoService.getChiTietKhoByObject(idkho, MAYMOI, idsanpham);

                if (result_ctk.idKho == idkho)
                {
                    //update số lượng
                    var idchitietkho = result_ctk.idChiTietKho;
                    ChiTietKho.idChiTietKho = result_ctk.idChiTietKho;
                    ChiTietKho.idKho = result_ctk.idKho;
                    ChiTietKho.TrangThaiSanPham = result_ctk.TrangThaiSanPham;
                    ChiTietKho.SoLuong = result_ctk.SoLuong + NhapKho.SoLuong;
                    ChiTietKho.idSanPham = NhapKho.idSanPham;
                    await ChitietkhoService.updateChiTietKhoAsync(idchitietkho, ChiTietKho);
                }
                else
                {
                    //thêm thông tin sản phẩm vào kho 
                    ChiTietKho.idKho = idkho;
                    ChiTietKho.TrangThaiSanPham = MAYMOI;
                    ChiTietKho.SoLuong = NhapKho.SoLuong;
                    ChiTietKho.idSanPham = NhapKho.idSanPham;
                    await ChitietkhoService.addChiTietKhoAsync(ChiTietKho);
                }

                //tăng số lượng trong sản phẩm trong kho
                Kho.idKho = idkho;
                Kho.SoLuongSanPham += NhapKho.SoLuong;
                Kho.idTaiKhoan = (int)id!;
                await KhoService.updateKhoAsync(idkho, Kho);
            }
            return RedirectToPage("./Index");
        }
        public string ranDomSeri()
        {
            //random ra 9 ký tự trong mã số seri của sản phẩm
            while (value.Length < 9)
            {
                value += list[random.Next(0, list.Count())];
            }
            return value;
        }
    }
}
