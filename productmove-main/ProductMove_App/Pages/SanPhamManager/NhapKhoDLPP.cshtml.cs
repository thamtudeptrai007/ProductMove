using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NuGet.Packaging;
using ProductMove_App.Data;
using ProductMove_App.Service;
using ProductMove_Model;
using RestSharp;
using System.Collections;
using System.Data;

namespace ProductMove_App.Pages.SanPhamManager
{
    public class NhapKhoDLPPModel : PageModel
    {
        private readonly ProductMove_AppContext _context;

        public NhapKhoDLPPModel(ProductMove_AppContext context)
        {
            _context = context;
        }
        public IList<SanPham> SanPhams { get; set; } = default!;
        public IList<ChiTietKho> ChiTietKho { get; set; } = default!;
        public IList<Inputdata> Inputdatas = new List<Inputdata>();

        [BindProperty]
        public Kho Kho { get; set; } = default!;
        [BindProperty]
        public ChiTietKho chiTietKho { get; set; } = default!;
        [BindProperty]
        public ChiTietKho chiTietKho2 { get; set; } = default!;
        [BindProperty]
        public ChiTietKho chiTietKho3 { get; set; } = default!;
        [BindProperty]
        public Kho Kho_up { get; set; } = default!;
        [BindProperty]
        public Seri Seri { get; set; } = default!;

        [BindProperty]
        public NhapKho NhapKho { get; set; } = default!;
        [BindProperty]
        public SanPham SanPham { get; set; } = default!;
        public List<Seri> SeriList { get; set; } = default!;
        public string ttsp = "Máy mới";
        public class Inputdata
        {
            public string tensp { get; set; } = default!;
            public string diachi { get; set; } = default!;
            public int soluong { get; set; } = default!;
            public int idKho { get; set; } = default!;
            public int idChiTietKho { get; set; } = default!;
            public int idSanPham { get; set; } = default!;

        }

        public const string MAYMOI = "Máy mới";

        public async Task<IActionResult> OnGet()
        {
            string loginCheck = HttpContext.Session.GetString("phanquyen");
            if (loginCheck == null)
            {
                return RedirectToPage("/UsersManager/Login");
            }
            var data = await ChitietkhoService.getAllSpByTTSPAsync(ttsp);
            for (int i = 0; i < data.Count(); i++)
            {
                var pq = data[i].Kho!.TaiKhoan!.PhanQuyen;
                if (pq == "Cơ sở sản xuất")
                {
                    Inputdata data_ = new Inputdata();
                    data_.idChiTietKho = data[i].idChiTietKho;
                    data_.idKho = data[i].idKho;
                    data_.diachi = data[i].Kho!.TaiKhoan!.DiaChi;
                    data_.soluong = data[i].SoLuong;
                    data_.idSanPham = data[i].idSanPham;
                    data_.tensp = data[i].SanPham!.TenSanPham;
                    Inputdatas.Add(data_);
                }
            }

            return Page();
        }
        public async Task<IActionResult> OnPostAsync(string idsp, string idkhos, string idctk)
        {
            //lấy thông tin kho từ tài khoản vừa đăng nhập
            var _id = HttpContext.Session.GetInt32("idtaikhoan");
            var result = await KhoService.getKhoByIdTaiKhoanAsync(_id);
            Kho = result!;
            //Lấy id kho để lưu lại thông tin sản phẩm vừa nhập đã vào kho đó
            int idkho_tk = result!.idKho;
            int soluong_res = result.SoLuongSanPham;



            var soluong = Request.Form["soluongnhap"];
            string soluongnhap = soluong;
            var gettop = await SeriService.getSpByTopAsync(soluongnhap, idsp, idkhos);
            for (var i = 0; i < gettop.Count(); i++)
            {
                var idseri = gettop[i].idSeri;
                var tenseri = gettop[i].tenSeri;
                var thoigiansanxuat = gettop[i].ThoiGianSanXuat;
                var idsanpham = gettop[i].idSanPham;
                var idkho = gettop[i].idKho;
                var ttsp = gettop[i].Trangthaisanpham;

                //chuyển sản phẩm đến kho của đại lý phân phối
                Seri.idSeri = idseri;
                Seri.idKho = idkho_tk;
                Seri.ThoiGianSanXuat = thoigiansanxuat;
                Seri.idSanPham = idsanpham;
                Seri.tenSeri = tenseri;
                Seri.Trangthaisanpham = ttsp;
                await SeriService.updateSeriAsync(idseri, Seri);
            }
            //giảm số sản phẩm của kho cơ sở sản xuất đã lấy sản phẩm
            int soluongnhap_cv = Int32.Parse(soluongnhap);
            var data = await KhoService.getKhoByIdKhoAsync(idkhos);
            Kho.idKho = data!.idKho;
            Kho.SoLuongSanPham = data.SoLuongSanPham - soluongnhap_cv;
            Kho.idTaiKhoan = data.idTaiKhoan;
            int idkho_cv = Int32.Parse(idkhos);
            int idsp_cv = Int32.Parse(idsp);
            int idctk_cv = Int32.Parse(idctk);
            await KhoService.updateKhoAsync(idkho_cv, Kho);

            //Tăng số lượng của kho đang đăng nhập
            Kho_up.idKho = idkho_tk;
            Kho_up.SoLuongSanPham = soluong_res + soluongnhap_cv;
            Kho_up.idTaiKhoan = (int)_id!;
            await KhoService.updateKhoAsync(idkho_tk, Kho_up);

            //update lại chi tiết kho của kho cơ sở sản xuất đã lấy sản phẩm
            var chitietkho = await ChitietkhoService.getChiTietKhoByObject(idkho_cv, MAYMOI, idsp_cv);
            //nếu số lượng nhập vào bằng với số lượng có trong kho cơ sở sản xuất thì tiến hành chuyển toàn bộ sang kho của tài khoản đăng nhập
            if (soluongnhap_cv == chitietkho.SoLuong)
            {
                var thongtinsanpham = await ChitietkhoService.getChiTietKhoByObject(idkho_tk, MAYMOI, idsp_cv);
                if (thongtinsanpham.idKho == idkho_tk && thongtinsanpham.TrangThaiSanPham == MAYMOI)
                {
                    await ChitietkhoService.deleteChiTietKho(idctk_cv);
                    chiTietKho.idChiTietKho = thongtinsanpham.idChiTietKho;
                    chiTietKho.idKho = thongtinsanpham.idKho;
                    chiTietKho.TrangThaiSanPham = thongtinsanpham.TrangThaiSanPham;
                    chiTietKho.SoLuong = thongtinsanpham.SoLuong + soluongnhap_cv;
                    chiTietKho.idSanPham = thongtinsanpham.idSanPham;
                    await ChitietkhoService.updateChiTietKhoAsync(thongtinsanpham.idChiTietKho, chiTietKho);
                }
                else
                {
                    chiTietKho.idChiTietKho = chitietkho.idChiTietKho;
                    chiTietKho.idKho = idkho_tk;
                    chiTietKho.TrangThaiSanPham = MAYMOI;
                    chiTietKho.SoLuong = chitietkho.SoLuong;
                    chiTietKho.idSanPham = chitietkho.idSanPham;
                    await ChitietkhoService.updateChiTietKhoAsync(chitietkho.idChiTietKho, chiTietKho);
                }
            }
            //nếu số lượng nhập vượt quá số lượng sản phẩm trong kho đang có thì tiến hành thông báo lỗi
            if (soluongnhap_cv > chitietkho.SoLuong)
            {
                ChiTietKho = await ChitietkhoService.getAllSpByTTSPAsync(ttsp);
                ViewData["err"] = "Err";
                return Page();
            }
            //nếu số lượng nhập nhỏ hơn số lượng sản phẩm trong kho thì tiến hành thêm chi tiết kho mới và giảm số lượng đang có của kho được lấy sản phẩm
            if (soluongnhap_cv < chitietkho.SoLuong)
            {
                chiTietKho2.idChiTietKho = chitietkho.idChiTietKho;
                chiTietKho2.idKho = chitietkho.idKho;
                chiTietKho2.TrangThaiSanPham = MAYMOI;
                chiTietKho2.SoLuong = chitietkho.SoLuong - soluongnhap_cv;
                chiTietKho2.idSanPham = chitietkho.idSanPham;
                await ChitietkhoService.updateChiTietKhoAsync(chitietkho.idChiTietKho, chiTietKho2);

                var thongtinsanpham = await ChitietkhoService.getChiTietKhoByObject(idkho_tk, MAYMOI, idsp_cv);

                if (thongtinsanpham.idKho == idkho_tk && thongtinsanpham.TrangThaiSanPham == MAYMOI)
                {
                    chiTietKho2.idChiTietKho = thongtinsanpham.idChiTietKho;
                    chiTietKho2.idKho = thongtinsanpham.idKho;
                    chiTietKho2.TrangThaiSanPham = thongtinsanpham.TrangThaiSanPham;
                    chiTietKho2.SoLuong = thongtinsanpham.SoLuong + soluongnhap_cv;
                    chiTietKho2.idSanPham = thongtinsanpham.idSanPham;
                    await ChitietkhoService.updateChiTietKhoAsync(thongtinsanpham.idChiTietKho, chiTietKho2);
                }
                else
                {
                    ChiTietKho chiTiets = new ChiTietKho();
                    chiTiets.idKho = idkho_tk;
                    chiTiets.TrangThaiSanPham = MAYMOI;
                    chiTiets.SoLuong = soluongnhap_cv;
                    chiTiets.idSanPham = idsp_cv;
                    await ChitietkhoService.addChiTietKhoAsync(chiTiets);
                }
            }
            var data_ = await ChitietkhoService.getAllSpByTTSPAsync(ttsp);
            for (int i = 0; i < data_.Count(); i++)
            {
                var pq = data_[i].Kho!.TaiKhoan!.PhanQuyen;
                if (pq == "Cơ sở sản xuất")
                {
                    Inputdata dataS = new Inputdata();
                    dataS.idChiTietKho = data_[i].idChiTietKho;
                    dataS.idKho = data_[i].idKho;
                    dataS.diachi = data_[i].Kho!.TaiKhoan!.DiaChi;
                    dataS.soluong = data_[i].SoLuong;
                    dataS.idSanPham = data_[i].idSanPham;
                    dataS.tensp = data_[i].SanPham!.TenSanPham;
                    Inputdatas.Add(dataS);
                }
            }
            return Page();
        }
    }
}
