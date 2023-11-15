using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using ProductMove_App.Service;
using ProductMove_Model;
using RestSharp;

namespace ProductMove_App.Pages.SanPhamManager
{
    public class NhapKhoCSSX_BHModel : PageModel
    {
        DateTime now = DateTime.Now;
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
        public Seri Seri { get; set; } = default!;
        [BindProperty]
        public Kho Kho { get; set; } = default!;
        [BindProperty]
        public ChiTietKho ChiTietKho { get; set; } = default!;
        [BindProperty]
        public NhapKho NhapKho { get; set; } = default!;
        public async Task<IActionResult> OnPostAsync()
        {
            //lấy thông tin kho từ tài khoản vừa đăng nhập
            var _id = HttpContext.Session.GetInt32("idtaikhoan");
            var result = await KhoService.getKhoByIdTaiKhoanAsync(_id);
            Kho = result!;
            //Lấy id kho để lưu lại thông tin sản phẩm vừa nhập đã vào kho đó
            int idkho = result!.idKho;
            var name = Seri.tenSeri;
            //Kiểm tra xem có thông tin máy nào có tên seri trùng hay không
            var result_check = await SeriService.getSeriByNameAsync(name);
            int idseri = result_check.idSeri;
            //nếu kiểm tra thành công có sản phẩm có số seri đó thì tiến hành lưu vào kho để bảo hành
            if (result_check.tenSeri != null)
            {
                Seri.idSeri = idseri;
                Seri.idKho = idkho;
                Seri.ThoiGianSanXuat = result_check.ThoiGianSanXuat;
                Seri.idSanPham = result_check.idSanPham;
                Seri.tenSeri = result_check.tenSeri;
                Seri.Trangthaisanpham = "Máy lỗi không sửa chữa được.";
                await SeriService.updateSeriAsync(idseri, Seri);

                //sau khi lưu vào kho thì update lại số lượng sản phẩm trong kho
                //tăng số lượng trong sản phẩm trong kho
                Kho.idKho = idkho;
                Kho.SoLuongSanPham++;
                Kho.idTaiKhoan = (int)_id!;
                await KhoService.updateKhoAsync(idkho, Kho);

                //thêm thông tin chi tiết sản phẩm vào kho.
                ChiTietKho.idKho = idkho;
                ChiTietKho.TrangThaiSanPham = "Máy lỗi không sửa chữa được.";
                ChiTietKho.SoLuong++;
                ChiTietKho.idSanPham = result_check.idSanPham;
                ChiTietKho.idSanPham = result_check.idSanPham;
                await ChitietkhoService.addChiTietKhoAsync(ChiTietKho);

                //thêm thông tin thời gian nhập sản phẩm bảo hành 
                NhapKho.idKho = idkho;
                NhapKho.idSanPham = result_check.idSanPham;
                NhapKho.NgayNhap = now.ToString("dd/MM/yyyy");
                NhapKho.SoLuong = 1;
                await NhapKhoService.addSpNhapKhoAsync(NhapKho);

                var data = await ChitietkhoService.getAllSpByTTSPAsync("Máy đang trong giai đoạn sửa chữa");
                if (data.Count() > 0)
                {
                    await ChitietkhoService.deleteChiTietKho(data[0].idChiTietKho);
                }
            }
            else
            {
                ViewData["noseri"] = "Check không tồn tại số seri của máy";
                return Page();
            }
            return RedirectToPage("./Index");
        }
    }
}
