using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductMove_API.Service.KhoService;
using ProductMove_Model;

namespace ProductMove_API.Controllers
{
    public class KhosController : ConBase
    {
        private IKhoService _repo;

        public KhosController(IKhoService repo)
        {
            _repo = repo;
        }
        [HttpGet("getAllKho")]
        public async Task<IActionResult> getAllKho()
        {
            try
            {
                return Ok(await _repo.getAllKhoAsync());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("getKhoById/{id}")]
        public async Task<IActionResult> getKhoById(int id)
        {
            var kho = await _repo.getKhoAsync(id);
            return kho == null ? NotFound() : Ok(kho);
        }
        [HttpGet("getKhoByIdTaiKhoan/{id}")]
        public async Task<IActionResult> getKhoByIdTaiKhoanAsync(int id)
        {
            var kho = await _repo.getKhoByIdTaiKhoanAsync(id);
            return kho == null ? NotFound() : Ok(kho);
        }
        [HttpPost("addKho")]
        public async Task<IActionResult> addKho(Kho Kho)
        {
            try
            {
                var newsp = await _repo.addKhoAsync(Kho);
                var kho = await _repo.getKhoAsync(newsp);
                return kho == null ? NotFound() : Ok(kho);
            }
            catch
            {
                return BadRequest();
            }

        }
        [HttpPut("updateKho/{id}")]
        public async Task<IActionResult> updateKho(int id, Kho kho)
        {
            try
            {
                await _repo.updateKhoAsync(id, kho);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpDelete("deleteKho/{id}")]
        public async Task<IActionResult> deleteKho(int id)
        {
            try
            {
                await _repo.deleteKhoAsync(id);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
