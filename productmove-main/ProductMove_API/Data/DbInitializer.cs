using Microsoft.CodeAnalysis.Scripting;
using ProductMove_Model;

namespace ProductMove_API.Data
{
    public class DbInitializer
    {
        public static void Initialize(ProductMove_DBContext context)
        {
            //Look for any students
            if (context.TaiKhoan.Any() ||
                context.LoaiSanPham.Any() ||
                context.SanPham.Any())
            {
                return; // DB has been seeded
            }

            var loaisanpham = new LoaiSanPham[]
            {
                new LoaiSanPham{TenLoaiSanPham ="Máy Gaming"},
                new LoaiSanPham{TenLoaiSanPham ="Học tập - Văn phòng"},
                new LoaiSanPham{TenLoaiSanPham ="Mỏng nhẹ"},
                new LoaiSanPham{TenLoaiSanPham ="Đồ họa - Kỹ thuật"},
                new LoaiSanPham{TenLoaiSanPham ="Cao cấp sang trọng"},
            };
            context.LoaiSanPham.AddRangeAsync(loaisanpham);
            context.SaveChanges();

            var taikhoan = new TaiKhoan[]
            {
                new TaiKhoan
                { TenDangNhap = "Admin", MatKhau = BCrypt.Net.BCrypt.HashPassword("admin"),PhanQuyen ="ADMIN",DiaChi="Hà Nội, Việt Nam",Mail ="ADMIN@admin.com"},
            };
            context.TaiKhoan.AddRangeAsync(taikhoan);
            context.SaveChanges();

            var sanpham = new SanPham[]
            {
                new SanPham{
                    idLoaiSanPham =1,
                    TenSanPham ="Laptop Asus TUF Gaming FX506LHB",
                    GiaThanh="20.990.000",
                    MoTaSanPham="CPU:\r\n\r\ni510300H2.5GHz\r\nRAM:\r\n\r\n8 GBDDR4 2 khe (1 khe 8 GB + 1 khe rời)2933 MHz\r\nỔ cứng:\r\n\r\n512 GB SSD NVMe PCIe (Có thể tháo ra, lắp thanh khác tối đa 1 TB)\r\nMàn hình:\r\n\r\n15.6\"Full HD (1920 x 1080) 144Hz\r\nCard màn hình:\r\n\r\nCard rờiGTX 1650 4GB\r\nCổng kết nối:\r\n\r\nHDMILAN (RJ45)USB 2.0Jack tai nghe 3.5 mm2 x USB 3.21x USB 3.2 Gen 2 Type-C support DisplayPort / power delivery / G-SYNC\r\nĐặc biệt:\r\n\r\nCó đèn bàn phím\r\nHệ điều hành:\r\n\r\nWindows 11 Home SL\r\nThiết kế:\r\n\r\nVỏ nhựa\r\nKích thước, khối lượng:\r\n\r\nDài 359 mm - Rộng 256 mm - Dày 24.9 mm - Nặng 2.3 kg\r\nThời điểm ra mắt:\r\n\r\n2021",
                    ThoiGianBaoHanh ="2"
                },
                new SanPham{
                    idLoaiSanPham =2,
                    TenSanPham ="Laptop Asus VivoBook A415EA",
                    GiaThanh="16.590.000",
                    MoTaSanPham="CPU:\r\n\r\ni51135G72.4GHz\r\nRAM:\r\n\r\n8 GBDDR4 (Onboard)3200 MHz\r\nỔ cứng:\r\n\r\n512 GB SSD NVMe PCIe (Có thể tháo ra, lắp thanh khác tối đa 1 TB)\r\nMàn hình:\r\n\r\n14\"Full HD (1920 x 1080)\r\nCard màn hình:\r\n\r\nCard tích hợpIntel Iris Xe\r\nCổng kết nối:\r\n\r\nUSB Type-C2 x USB 2.0HDMIJack tai nghe 3.5 mm1 x USB 3.2\r\nHệ điều hành:\r\n\r\nWindows 11 Home SL\r\nThiết kế:\r\n\r\nVỏ nhựa - nắp lưng bằng kim loại\r\nKích thước, khối lượng:\r\n\r\nDài 324 mm - Rộng 215 mm - Dày 17.9 mm - Nặng 1.4 kg\r\nThời điểm ra mắt:\r\n\r\n2021",
                    ThoiGianBaoHanh ="2"
                },
                new SanPham{
                    idLoaiSanPham =3,
                    TenSanPham ="Laptop Asus ZenBook UX325EA",
                    GiaThanh="27.190.000",
                    MoTaSanPham="CPU:\r\n\r\ni71165G72.8GHz\r\nRAM:\r\n\r\n16 GBLPDDR4X (Onboard)4267 MHz\r\nỔ cứng:\r\n\r\n512 GB SSD NVMe PCIe (Có thể tháo ra, lắp thanh khác tối đa 1 TB)\r\nMàn hình:\r\n\r\n13.3\"Full HD (1920 x 1080) OLED\r\nCard màn hình:\r\n\r\nCard tích hợpIntel Iris Xe\r\nCổng kết nối:\r\n\r\nHDMI2 x Thunderbolt 4 USB-C1 x USB 3.2\r\nĐặc biệt:\r\n\r\nCó đèn bàn phím\r\nHệ điều hành:\r\n\r\nWindows 11 Home SL\r\nThiết kế:\r\n\r\nVỏ kim loại nguyên khối\r\nKích thước, khối lượng:\r\n\r\nDài 304.2 mm - Rộng 203 mm - Dày 13.9 mm - Nặng 1.14 kg\r\nThời điểm ra mắt:\r\n\r\n2021",
                    ThoiGianBaoHanh ="2"
                },
                new SanPham{
                    idLoaiSanPham =4,
                    TenSanPham ="Laptop Asus ROG Strix G513RM",
                    GiaThanh="42.190.000",
                    MoTaSanPham="CPU:\r\n\r\ni712700H2.30 GHz\r\nRAM:\r\n\r\n16 GBLPDDR5 (8 GB Onboard + 8 GB Onboard)5200 MHz\r\nỔ cứng:\r\n\r\n512 GB SSD NVMe PCIe (Có thể tháo ra, lắp thanh khác tối đa 1 TB)\r\nMàn hình:\r\n\r\n13.4\"WUXGA (1920 x 1200)120Hz\r\nCard màn hình:\r\n\r\nCard rờiRTX 3050 4GB\r\nCổng kết nối:\r\n\r\nUSB 2.0Jack tai nghe 3.5 mmThunderbolt 4 USB-C1x USB 3.2 Gen 2 Type-C support DisplayPort / power delivery / G-SYNC1x ROG XG Mobile Interface\r\nĐặc biệt:\r\n\r\nCó màn hình cảm ứngCó đèn bàn phím\r\nHệ điều hành:\r\n\r\nWindows 11 Home SL\r\nThiết kế:\r\n\r\nVỏ nhựa - nắp lưng bằng kim loại\r\nKích thước, khối lượng:\r\n\r\nDài 302 mm - Rộng 204 mm - Dày 12 mm - Nặng 1.18 kg\r\nThời điểm ra mắt:\r\n\r\n2022",
                    ThoiGianBaoHanh ="2"
                },
                new SanPham{
                    idLoaiSanPham =5,
                    TenSanPham ="Laptop Asus Zenbook 14 OLED",
                    GiaThanh="31.990.000",
                    MoTaSanPham="CPU:\r\n\r\ni71260P2.1GHz\r\nRAM:\r\n\r\n16 GBLPDDR5 (Onboard)4800 MHz\r\nỔ cứng:\r\n\r\n512 GB SSD NVMe PCIe\r\nMàn hình:\r\n\r\n14\"2.8K (2880 x 1800) - OLED 16:1090Hz\r\nCard màn hình:\r\n\r\nCard tích hợpIntel Iris Xe\r\nCổng kết nối:\r\n\r\nHDMIJack tai nghe 3.5 mm2 x Thunderbolt 41 x USB 3.2\r\nĐặc biệt:\r\n\r\nCó đèn bàn phím\r\nHệ điều hành:\r\n\r\nWindows 11 Home SL\r\nThiết kế:\r\n\r\nVỏ kim loại\r\nKích thước, khối lượng:\r\n\r\nDài 313.6 mm - Rộng 220.6 mm - Dày 16.9 mm - Nặng 1.39 kg\r\nThời điểm ra mắt:\r\n\r\n2022",
                    ThoiGianBaoHanh ="2"
                },
            };
            context.SanPham.AddRangeAsync(sanpham);
            context.SaveChanges();
        }
    }
}