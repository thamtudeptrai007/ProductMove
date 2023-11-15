using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductMove_API.Service.TaiKhoanService;
using ProductMove_Model;

namespace ProductMove_API.Controllers
{
    public class TaiKhoansController : ConBase
    {
        private readonly ITaiKhoanService _repo;

        public TaiKhoansController(ITaiKhoanService taiKhoanService)
        {
            _repo = taiKhoanService;
        }
        [HttpGet("getAllTaiKhoan")]
        public async Task<IActionResult> getAllTaikhoan()
        {
            try
            {
                return Ok(await _repo.getAllTaiKhoanAsync());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("getTaiKhoanByID/{id}")]
        public async Task<IActionResult> getTaiKhoanById(int id)
        {
            var taikhoan = await _repo.getTaiKhoanAsync(id);
            return taikhoan == null ? NotFound() : Ok(taikhoan);
        }
        [HttpGet("getTaiKhoanByName/{name}")]
        public async Task<IActionResult> getTaiKhoanByName(string name)
        {
            var taikhoan = await _repo.getTaiKhoanByNameAsync(name);
            return taikhoan == null ? NotFound() : Ok(taikhoan);
        }
        [HttpGet("getTaiKhoanByPhanQuyen/{phanquyen}")]
        public async Task<IActionResult> getTaiKhoanByPhanQuyen(string phanquyen)
        {
            var taikhoan = await _repo.getAllTaiKhoanbyPhanquyenAsync(phanquyen);
            return taikhoan == null ? NotFound() : Ok(taikhoan);
        }
        [HttpPost("addTaiKhoan")]
        public async Task<IActionResult> addTaiKhoan(TaiKhoan taiKhoan)
        {
            try
            {
                var newTaikhoan = await _repo.addTaiKhoanAsync(taiKhoan);
                var taikhoan = await _repo.getTaiKhoanAsync(newTaikhoan);
                return taikhoan == null ? NotFound() : Ok(taikhoan);
            }
            catch
            {
                return BadRequest();
            }

        }
        [HttpPut("updateTaiKhoan/{id}")]
        public async Task<IActionResult> updateTaiKhoan(int id, TaiKhoan taiKhoan)
        {
            try
            {
                await _repo.updateTaiKhoanAsync(id, taiKhoan);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpDelete("deletetaiKhoan/{id}")]
        public async Task<IActionResult> deleteTaiKhoan(int id)
        {
            try
            {
                await _repo.deleteTaiKhoanAsync(id);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
