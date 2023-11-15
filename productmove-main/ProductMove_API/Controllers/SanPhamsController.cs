using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductMove_API.Service.SanPhamService;
using ProductMove_Model;

namespace ProductMove_API.Controllers
{
    public class SanPhamsController : ConBase
    {
        private readonly ISanPhamService _repo;
        public SanPhamsController(ISanPhamService repo)
        {
            _repo = repo;
        }
        [HttpGet("getAllSanPham")]
        public async Task<IActionResult> getAllSanPham()
        {
            try
            {
                return Ok(await _repo.getAllSanPhamAsync());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("getSanPhamById/{id}")]
        public async Task<IActionResult> getSanPhamById(int id)
        {
            var sanpham = await _repo.getSanPhamAsync(id);
            return sanpham == null ? NotFound() : Ok(sanpham);
        }
        [HttpPost("addSanPham")]
        public async Task<IActionResult> addSanPham(SanPham Sanpham)
        {
            try
            {
                var newsp = await _repo.addSanPhamAsync(Sanpham);
                var sanpham = await _repo.getSanPhamAsync(newsp);
                return sanpham == null ? NotFound() : Ok(sanpham);
            }
            catch
            {
                return BadRequest();
            }

        }
        [HttpPut("updateSanPham/{id}")]
        public async Task<IActionResult> updateSanPham(int id, SanPham sanPham)
        {
            try
            {
                await _repo.updateSanPhamAsync(id, sanPham);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpDelete("deleteSanPham/{id}")]
        public async Task<IActionResult> deleteTaiKhoan(int id)
        {
            try
            {
                await _repo.deleteSanPhamAsync(id);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
