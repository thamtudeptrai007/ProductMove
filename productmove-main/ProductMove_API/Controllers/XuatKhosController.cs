using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductMove_API.Service.NhapKhoSerice;
using ProductMove_API.Service.XuatKhoService;
using ProductMove_Model;

namespace ProductMove_API.Controllers
{
    public class XuatKhosController : ConBase
    {
        private readonly IXuatKhoService _repo;

        public XuatKhosController(IXuatKhoService repo)
        {
            _repo = repo;
        }
        [HttpGet("getAllSpXuatKho")]
        public async Task<IActionResult> getAllSpXuatKho()
        {
            try
            {
                return Ok(await _repo.getAllSpPXuatKhoAsync());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("getSpXuatKhoById/{id}")]
        public async Task<IActionResult> getSpXuatKhoById(int id)
        {
            var kho = await _repo.getSpXuatKhoAsync(id);
            return kho == null ? NotFound() : Ok(kho);
        }
        [HttpPost("addSpXuatKho")]
        public async Task<IActionResult> addSpXuatKho(XuatKho xuatKho)
        {
            try
            {
                var newsp = await _repo.addSpXuatKhoAsync(xuatKho);
                var xuatkho = await _repo.getSpXuatKhoAsync(newsp);
                return xuatkho == null ? NotFound() : Ok(xuatKho);
            }
            catch
            {
                return BadRequest();
            }

        }
        [HttpPut("updateSpXuatKho/{id}")]
        public async Task<IActionResult> updateSpXuatKho(int id, XuatKho xuatKho)
        {
            try
            {
                await _repo.updateSpXuatKhoAsync(id, xuatKho);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpDelete("deleteSpXuatKho/{id}")]
        public async Task<IActionResult> deleteSpXuatKho(int id)
        {
            try
            {
                await _repo.deleteSpXuatKhoAsync(id);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
