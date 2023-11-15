using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProductMove_App.Service;
using ProductMove_Model;

namespace ProductMove_App.Pages.SanPhamManager
{
    public class LapHoaDonModel : PageModel
    {
        DateTime now = DateTime.Now;
        [BindProperty]
        public KhachHang khachHang { get; set; } = default!;
        [BindProperty]
        public HoaDon hoaDon { get; set; } = default!;
        [BindProperty]
        public Seri seri { get; set; } = default!;
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
        public XuatKho XuatKho { get; set; } = default!;

        public const string MAYMOI = "Máy mới";
        public async Task<IActionResult> OnPostAsync()
        {
            var req_sdt = khachHang.SoDienThoai;
            var khachhang = await KhachHangService.getKhachHangBySDT(req_sdt);
            var req_seri = await SeriService.getSeriByNameAsync(seri.tenSeri);
            if (req_seri?.tenSeri == null || req_seri?.Trangthaisanpham == "Máy đã bán")
            {
                ViewData["nodata"] = "msg_không tồn tại số seri";
                return Page();
            }
            if (khachhang.SoDienThoai == null)
            {
                //kiểm tra khách hàng có từng mua hàng hay chưa, nếu chưa có thì tiến hành tạo thông tin khách hàng mới.
                await KhachHangService.addKhachHangAsync(khachHang);
                //sau khi tạo mới thông tin khách hàng thì tiến hành tạo hóa đơn cho khách
                var req_khachhang = await KhachHangService.getKhachHangBySDT(req_sdt);
                var id_khachhang = req_khachhang.IdKhachHang;

                hoaDon.IdKhachHang = id_khachhang;
                hoaDon.NgayLapHoaDon = now.ToString("dd/mm/yyyy");
                var diachi_accountdangdangnhap = HttpContext.Session.GetString("diachi");
                hoaDon.Diachi = diachi_accountdangdangnhap!;
                hoaDon.KhachHang = null;
                hoaDon.idSeri = req_seri!.idSeri;
                hoaDon.Seri = null;
                await HoaDonService.newHoaDon(hoaDon);
                await updateKho();

            }
            else
            {
                //nếu khách hàng đã từng mua sản phẩm thì tiến hành lập hóa đơn
                var id_khachhang = khachhang.IdKhachHang;

                hoaDon.IdKhachHang = id_khachhang;
                hoaDon.NgayLapHoaDon = now.ToString("dd/mm/yyyy");
                var diachi_accountdangdangnhap = HttpContext.Session.GetString("diachi");
                hoaDon.Diachi = diachi_accountdangdangnhap!;
                hoaDon.KhachHang = null;
                hoaDon.idSeri = req_seri!.idSeri;
                await HoaDonService.newHoaDon(hoaDon);
                await updateKho();
            }
            return RedirectToPage("/Sanphammanager/Index");
        }
        public async Task<IActionResult> updateKho()
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
            var idsp = result_check.idSanPham;
            var chitietkho = await ChitietkhoService.getChiTietKhoByObject(idkho, MAYMOI, idsp);

            //nếu kiểm tra thành công có sản phẩm có số seri đó thì tiến hành lưu vào kho để bảo hành
            if (result_check.tenSeri != null && result_check.idKho == idkho)
            {
                Seri.idSeri = idseri;
                Seri.idKho = null;
                Seri.ThoiGianSanXuat = result_check.ThoiGianSanXuat;
                Seri.idSanPham = result_check.idSanPham;
                Seri.tenSeri = result_check.tenSeri;
                Seri.Trangthaisanpham = "Máy đã bán";
                await SeriService.updateSeriAsync(idseri, Seri);

                //sau khi lưu vào kho thì update lại số lượng sản phẩm trong kho
                //tăng số lượng trong sản phẩm trong kho
                Kho.idKho = idkho;
                Kho.SoLuongSanPham--;
                Kho.idTaiKhoan = (int)_id!;
                await KhoService.updateKhoAsync(idkho, Kho);

                //thêm thông tin thời gian nhập sản phẩm bảo hành 
                XuatKho.idKho = idkho;
                XuatKho.idSanPham = result_check.idSanPham;
                XuatKho.NgayXuat = now.ToString("dd/MM/yyyy");
                XuatKho.SoLuong = 1;
                XuatKho.SeriSanpham = Seri.tenSeri;
                await XuatKhoService.addSpXuatKhoAsync(XuatKho);

                //giảm số lượng trong bảng chi tiết kho
                ChiTietKho.idChiTietKho = chitietkho.idChiTietKho;
                ChiTietKho.idKho = chitietkho.idKho;
                ChiTietKho.TrangThaiSanPham = MAYMOI;
                ChiTietKho.SoLuong = chitietkho.SoLuong - 1;
                ChiTietKho.idSanPham = chitietkho.idSanPham;
                await ChitietkhoService.updateChiTietKhoAsync(chitietkho.idChiTietKho, ChiTietKho);

            }
            else
            {
                ViewData["nodata"] = "Sản phẩm không còn bảo hành hoặc không tồn tại số seri này";
                return Page();
            }
            return RedirectToPage("/Sanphammanager/Index");
        }
    }
}
