using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductMove_API.Service.LoaiSanPhamService;
using ProductMove_Model;

namespace ProductMove_API.Controllers
{

    public class LoaiSanPhamsController : ConBase
    {
        private readonly ILoaiSanPhamService _repo;
        public LoaiSanPhamsController(ILoaiSanPhamService loaiSanPhamService)
        {
            _repo = loaiSanPhamService;
        }
        [HttpGet("getAllLoaiSanPham")]
        public async Task<IActionResult> getAllLoaiSanPham()
        {
            try
            {
                return Ok(await _repo.getAllLoaiSanPhamAsync());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("getLoaiSanPhamByID/{id}")]
        public async Task<IActionResult> getLoaiSanPhamById(int id)
        {
            var loaiSanPham = await _repo.getLoaiSanPhamAsync(id);
            return loaiSanPham == null ? NotFound() : Ok(loaiSanPham);
        }
        [HttpGet("getLoaiSanPhamByName/{name}")]
        public async Task<IActionResult> getLoaiSanphamByName(string name)
        {
            var loaiSanPham = await _repo.getLoaiSanPhamByName(name);
            return loaiSanPham == null ? NotFound() : Ok(loaiSanPham);
        }
        [HttpPost("addLoaiSanPham")]
        public async Task<IActionResult> addLoaiSanPham(LoaiSanPham loaiSanPham)
        {
            try
            {
                var newLoaiSanPham = await _repo.addLoaiSanPhamAsync(loaiSanPham);
                var loaisanpham = await _repo.getLoaiSanPhamAsync(newLoaiSanPham);
                return loaisanpham == null ? NotFound() : Ok(loaisanpham);
            }
            catch
            {
                return BadRequest();
            }

        }
        [HttpPut("updateLoaiSanPham/{id}")]
        public async Task<IActionResult> updateLoaiSanPham(int id, LoaiSanPham loaiSanPham)
        {
            try
            {
                await _repo.updateLoaiSanPhamAsync(id, loaiSanPham);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpDelete("deleteLoaiSanPham/{id}")]
        public async Task<IActionResult> deleteLoaiSanPham(int id)
        {
            try
            {
                await _repo.deleteLoaiSanPhamAsync(id);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
