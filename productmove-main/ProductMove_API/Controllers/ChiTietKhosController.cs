using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductMove_API.Service.ChiTietKhoService;
using ProductMove_Model;

namespace ProductMove_API.Controllers
{

    public class ChiTietKhosController : ConBase
    {
        private readonly IChiTietKhoService _repo;
        public ChiTietKhosController(IChiTietKhoService chiTietKhoService)
        {
            _repo = chiTietKhoService;
        }
        [HttpGet("getAllChiTietKho")]
        public async Task<IActionResult> getAllChiTietKho()
        {
            try
            {
                return Ok(await _repo.getAllChiTietKhoAsync());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("getChiTietKhoByID/{id}")]
        public async Task<IActionResult> getChiTietKhoById(int id)
        {
            var chiTietKho = await _repo.getChiTietKhoAsync(id);
            return chiTietKho == null ? NotFound() : Ok(chiTietKho);
        }
        [HttpGet("getChiTietKhoByIDSP/{id}")]
        public async Task<IActionResult> getChiTietKhoByIdSP(int id)
        {
            var chiTietKho = await _repo.getAllSpByIdSPAsync(id);
            return chiTietKho == null ? NotFound() : Ok(chiTietKho);
        }
        [HttpGet("getChiTietKhoByObject/{idkho}/{ttsp}/{idsp}")]
        public async Task<IActionResult> getChiTietKhoByObjectAsync(int idkho, string ttsp, int idsp)
        {
            var chiTietKho = await _repo.getChiTietKhoByObjectAsync(idkho, ttsp, idsp);
            return chiTietKho == null ? NotFound() : Ok(chiTietKho);
        }
        [HttpGet("getChiTietKhoByidkhottsp/{idkho}/{ttsp}")]
        public async Task<IActionResult> getChiTietKhoByObjectAsync(int idkho, string ttsp)
        {
            var chiTietKho = await _repo.getChiTietKhoByObject_2Async(idkho, ttsp);
            return chiTietKho == null ? NotFound() : Ok(chiTietKho);
        }

        [HttpGet("getChiTietKhoByIdKho/{idkho}")]
        public async Task<IActionResult> getChiTietKhoByIdKho(int idkho)
        {
            var chiTietKho = await _repo.getAllSpByIdKhoAsync(idkho);
            return chiTietKho == null ? NotFound() : Ok(chiTietKho);
        }

        [HttpGet("getAllSpByTTSP/{ttsp}")]
        public async Task<IActionResult> getChiTietKhoByTTSP(string ttsp)
        {
            var chiTietKho = await _repo.getAllSpByTTSPAsync(ttsp);
            return chiTietKho == null ? NotFound() : Ok(chiTietKho);
        }
        [HttpPost("addChiTietKho")]
        public async Task<IActionResult> addChiTietKho(ChiTietKho chiTietKho)
        {
            try
            {
                var newChiTietKho = await _repo.addChiTietKhoAsync(chiTietKho);
                var chitietkho = await _repo.getChiTietKhoAsync(newChiTietKho);
                return chitietkho == null ? NotFound() : Ok(chitietkho);
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPut("updateChiTietKho/{id}")]
        public async Task<IActionResult> updateChiTietKho(int id, ChiTietKho chiTietKho)
        {
            try
            {
                await _repo.updateChiTietKhoAsync(id, chiTietKho);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpDelete("deleteChiTietKho/{id}")]
        public async Task<IActionResult> deleteChiTietKho(int id)
        {
            try
            {
                await _repo.deleteChiTietKhoAsync(id);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
