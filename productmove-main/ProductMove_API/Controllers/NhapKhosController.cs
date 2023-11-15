using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductMove_API.Service.NhapKhoSerice;
using ProductMove_Model;

namespace ProductMove_API.Controllers
{
    public class NhapKhosController : ConBase
    {
        private readonly INhapKhoService _repo;

        public NhapKhosController(INhapKhoService repo)
        {
            _repo = repo;
        }
        [HttpGet("getAllSpNhapKho")]
        public async Task<IActionResult> getAllSpNhapKho()
        {
            try
            {
                return Ok(await _repo.getAllSpPNhapKhoAsync());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("getSpNhapKhoById/{id}")]
        public async Task<IActionResult> getSpNhapKhoById(int id)
        {
            var kho = await _repo.getSpNhapKhoAsync(id);
            return kho == null ? NotFound() : Ok(kho);
        }
        [HttpPost("addSpNhapKho")]
        public async Task<IActionResult> addSpNhapKho(NhapKho nhapKho)
        {
            try
            {
                var newsp = await _repo.addSpNhapKhoAsync(nhapKho);
                var nhapkho = await _repo.getSpNhapKhoAsync(newsp);
                return nhapkho == null ? NotFound() : Ok(nhapKho);
            }
            catch
            {
                return BadRequest();
            }

        }
        [HttpPut("updateSpNhapKho/{id}")]
        public async Task<IActionResult> updateSpNhapKho(int id, NhapKho nhapKho)
        {
            try
            {
                await _repo.updateSpNhapKhoAsync(id, nhapKho);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpDelete("deleteSpNhapKho/{id}")]
        public async Task<IActionResult> deleteSpNhapKho(int id)
        {
            try
            {
                await _repo.deleteSpNhapKhoAsync(id);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
