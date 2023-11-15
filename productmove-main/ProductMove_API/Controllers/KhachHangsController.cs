using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductMove_API.Service.KhachHangService;
using ProductMove_API.Service.KhoService;
using ProductMove_Model;

namespace ProductMove_API.Controllers
{
    public class KhachHangsController : ConBase
    {
        private readonly IKhachHangService _repo;

        public KhachHangsController(IKhachHangService repo)
        {
            _repo = repo;
        }

        [HttpGet("getAllKhachHang")]
        public async Task<IActionResult> getAllKhachHang()
        {
            try
            {
                return Ok(await _repo.getAllKhachHangAsync());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("getKhachHangById/{id}")]
        public async Task<IActionResult> getKhachHangByIdAsync(int id)
        {
            var kho = await _repo.getKhachHangByIdAsync(id);
            return kho == null ? NotFound() : Ok(kho);
        }

        [HttpGet("getKhachHangBySDT/{sdt}")]
        public async Task<IActionResult> getKhachHangBySDTAsync(string sdt)
        {
            var kho = await _repo.getKhachHangBySDTAsync(sdt);
            return kho == null ? NotFound() : Ok(kho);
        }

        [HttpPost("addKhachHang")]
        public async Task<IActionResult> addNewKhachHangAsync(KhachHang khachHang)
        {
            try
            {
                var req = await _repo.addNewKhachHangAsync(khachHang);
                var res = await _repo.getKhachHangByIdAsync(req);
                return res == null ? NotFound() : Ok(res);
            }
            catch
            {
                return BadRequest();
            }

        }
        [HttpPut("updateThongtinKhachHang/{id}")]
        public async Task<IActionResult> updateThongtinKhachHang(int id, KhachHang khachHang)
        {
            try
            {
                await _repo.updateThongtinKhachHang(id, khachHang);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
