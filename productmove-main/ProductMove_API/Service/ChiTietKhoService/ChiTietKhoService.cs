using Microsoft.EntityFrameworkCore;
using ProductMove_Model;

namespace ProductMove_API.Service.ChiTietKhoService;

public class ChiTietKhoService : IChiTietKhoService
{
    private readonly ProductMove_DBContext _context;
    public ChiTietKhoService(ProductMove_DBContext context)
    {
        _context = context;
    }

    public async Task<int> addChiTietKhoAsync(ChiTietKho chiTietKho)
    {
        _context.ChiTietKho.Add(chiTietKho);
        await _context.SaveChangesAsync();
        return chiTietKho.idChiTietKho;
    }

    public async Task deleteChiTietKhoAsync(int id)
    {
        var deleteChiTietKho = _context.ChiTietKho.SingleOrDefault(a => a.idChiTietKho == id);
        if (deleteChiTietKho != null)
        {
            _context.ChiTietKho.Remove(deleteChiTietKho);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<List<ChiTietKho>> getAllChiTietKhoAsync()
    {
        var chiTietKhos = await _context.ChiTietKho!.Include(a => a.SanPham).ToListAsync();
        return chiTietKhos;
    }

    public async Task<List<ChiTietKho>> getAllSpByIdKhoAsync(int idKho)
    {
        var chiTietKhos = await _context.ChiTietKho!.Include(a => a.SanPham).ToListAsync();
        var result = from cust in chiTietKhos
                     where cust.idKho == idKho
                     select cust;
        return result.ToList();
    }

    public async Task<ChiTietKho> getAllSpByIdSPAsync(int idsp)
    {
        var data = await _context.ChiTietKho!.SingleOrDefaultAsync(a => a.idSanPham == idsp);
        return data!;
    }

    public async Task<List<ChiTietKho>> getAllSpByTTSPAsync(string trangthaisanpham)
    {
        var result = await _context.ChiTietKho!.Include(a => a.SanPham).Include(b => b.Kho).Include(c => c.Kho!.TaiKhoan).ToListAsync();
        var select = from cust in result
                     where cust.TrangThaiSanPham == trangthaisanpham
                     select cust;
        return select.ToList();
    }

    public async Task<ChiTietKho> getChiTietKhoAsync(int id)
    {
        var chiTietKhos = await _context.ChiTietKho!.FindAsync(id);
        return chiTietKhos!;
    }

    public async Task<ChiTietKho> getChiTietKhoByObjectAsync(int idkho, string ttsp, int idsp)
    {
        var data = await _context.ChiTietKho!.FirstOrDefaultAsync(a => a.idSanPham == idsp && a.idKho == idkho && a.TrangThaiSanPham == ttsp);
        return data!;
    }

    public async Task<ChiTietKho> getChiTietKhoByObject_2Async(int idkho, string ttsp)
    {
        var data = await _context.ChiTietKho!.FirstOrDefaultAsync(a => a.idKho == idkho && a.TrangThaiSanPham == ttsp);
        return data!;
    }

    public async Task updateChiTietKhoAsync(int id, ChiTietKho chiTietKho)
    {
        if (id == chiTietKho.idChiTietKho)
        {
            _context.ChiTietKho.Update(chiTietKho);
            await _context.SaveChangesAsync();
        }
    }
}
