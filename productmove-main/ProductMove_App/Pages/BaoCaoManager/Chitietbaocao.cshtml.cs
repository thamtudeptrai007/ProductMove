using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using ProductMove_App.Service;
using ProductMove_Model;
using RestSharp;
using System;
using System.Globalization;

namespace ProductMove_App.Pages.BaoCaoManager
{
    public class ChitietbaocaoModel : PageModel
    {
        private readonly ProductMove_App.Data.ProductMove_AppContext _context;
        public ChitietbaocaoModel(ProductMove_App.Data.ProductMove_AppContext context)
        {
            _context = context;
        }
        public IList<NhapKho> NhapKho { get; set; } = default!;
        public IList<XuatKho> XuatKho { get; set; } = default!;
        public IList<ChiTietKho> ChiTietKho { get; set; } = default!;
        public BaoCao BaoCao { get; set; } = default!;

        public DateTime Date { get; set; } = default!;
        public async Task<IActionResult> OnGetAsync(int? id_)
        {
            string loginCheck = HttpContext.Session.GetString("phanquyen");
            if (loginCheck == null)
            {
                return RedirectToPage("/UsersManager/Login");
            }

            BaoCao = await BaoCaoService.getBaoCaoByID((int)id_!);
            if (BaoCao.LoaiBaoCao != 2)
            {
                ViewData["quy"] = DateTime.ParseExact(BaoCao.ThoiGian!, "MM/yyyy", CultureInfo.InvariantCulture).Month;
                ViewData["nam"] = DateTime.ParseExact(BaoCao.ThoiGian!, "MM/yyyy", CultureInfo.InvariantCulture).Year;
            }

            var idKho = BaoCao.IdKho;

            var request = new RestRequest($"/NhapKhos/getAllSpNhapKho", Method.Get);
            var res = await Program.Client.ExecuteAsync(request);
            var data = res.Content!;
            var result = JsonConvert.DeserializeObject<IList<NhapKho>>(data)!;

            var request_ = new RestRequest($"/XuatKhos/getAllSpXuatKho", Method.Get);
            var res_ = await Program.Client.ExecuteAsync(request_);
            var data_ = res_.Content!;
            var result_ = JsonConvert.DeserializeObject<IList<XuatKho>>(data_)!;

            int id = (int)idKho!;
            var rs = await ChitietkhoService.getAllSpByIdKhoAsync(id);
            ChiTietKho = rs;

            IList<NhapKho> nhap = new List<NhapKho>();
            IList<XuatKho> xuat = new List<XuatKho>();
            if (BaoCao != null)
            {
                if (BaoCao.LoaiBaoCao == 1)
                {
                    Date = DateTime.ParseExact(BaoCao.ThoiGian!, "MM/yyyy", CultureInfo.InvariantCulture);
                    int month = Date.Month;
                    int year = Date.Year;
                    foreach (NhapKho listNhapKho in result)
                    {
                        int monthcheck = DateTime.ParseExact(listNhapKho.NgayNhap, "dd/MM/yyyy", CultureInfo.InvariantCulture).Month;
                        int yearcheck = DateTime.ParseExact(listNhapKho.NgayNhap, "dd/MM/yyyy", CultureInfo.InvariantCulture).Year;
                        if (monthcheck == month)
                        {
                            if (yearcheck == year)
                            {
                                if (idKho == listNhapKho.idKho)
                                {
                                    nhap.Add(listNhapKho);
                                }
                            }

                        }
                    }
                    NhapKho = nhap;
                    foreach (XuatKho listxuatKho in result_)
                    {
                        int monthcheck_ = DateTime.ParseExact(listxuatKho.NgayXuat, "dd/MM/yyyy", CultureInfo.InvariantCulture).Month;
                        int yearcheck_ = DateTime.ParseExact(listxuatKho.NgayXuat, "dd/MM/yyyy", CultureInfo.InvariantCulture).Year;
                        if (monthcheck_ == month)
                        {
                            if (yearcheck_ == year)
                            {
                                if (idKho == listxuatKho.idKho)
                                {
                                    xuat.Add(listxuatKho);
                                }
                            }

                        }
                    }
                    XuatKho = xuat;
                }
                else if (BaoCao.LoaiBaoCao == 2)
                {
                    int year = Convert.ToInt32(BaoCao.ThoiGian);
                    foreach (NhapKho listNhapKho in result)
                    {
                        int yearcheck = DateTime.ParseExact(listNhapKho.NgayNhap, "dd/MM/yyyy", CultureInfo.InvariantCulture).Year;

                        if (yearcheck == year)
                        {
                            if (idKho == listNhapKho.idKho)
                            {
                                nhap.Add(listNhapKho);
                            }
                        }
                    }
                    NhapKho = nhap;
                    foreach (XuatKho listxuatKho in result_)
                    {
                        int yearcheck_ = DateTime.ParseExact(listxuatKho.NgayXuat, "dd/MM/yyyy", CultureInfo.InvariantCulture).Year;

                        if (yearcheck_ == year)
                        {
                            if (idKho == listxuatKho.idKho)
                            {
                                xuat.Add(listxuatKho);
                            }
                        }
                    }
                    XuatKho = xuat;
                }
                else
                {
                    Date = DateTime.ParseExact(BaoCao.ThoiGian!, "MM/yyyy", CultureInfo.InvariantCulture);
                    int quy = Date.Month;
                    int month = 0;
                    int year = Date.Year;
                    switch (quy)
                    {
                        case 1: { month = 1; break; }
                        case 2: { month = 4; break; }
                        case 3: { month = 7; break; }
                        case 4: { month = 10; break; }
                        default: break;
                    }
                    for (int i = month; i < (month + 3); i++)
                    {
                        foreach (NhapKho listNhapKho in result)
                        {
                            int monthcheck = DateTime.ParseExact(listNhapKho.NgayNhap, "dd/MM/yyyy", CultureInfo.InvariantCulture).Month;
                            int yearcheck = DateTime.ParseExact(listNhapKho.NgayNhap, "dd/MM/yyyy", CultureInfo.InvariantCulture).Year;
                            if (monthcheck == i)
                            {
                                if (yearcheck == year)
                                {
                                    if (idKho == listNhapKho.idKho)
                                    {
                                        nhap.Add(listNhapKho);
                                    }
                                }

                            }
                        }

                        foreach (XuatKho listxuatKho in result_)
                        {
                            int monthcheck_ = DateTime.ParseExact(listxuatKho.NgayXuat, "dd/MM/yyyy", CultureInfo.InvariantCulture).Month;
                            int yearcheck_ = DateTime.ParseExact(listxuatKho.NgayXuat, "dd/MM/yyyy", CultureInfo.InvariantCulture).Year;
                            if (monthcheck_ == i)
                            {
                                if (yearcheck_ == year)
                                {
                                    if (idKho == listxuatKho.idKho)
                                    {
                                        xuat.Add(listxuatKho);
                                    }
                                }

                            }
                        }

                    }
                    NhapKho = nhap;
                    XuatKho = xuat;
                }
            }
            return Page();

        }

    }
}
