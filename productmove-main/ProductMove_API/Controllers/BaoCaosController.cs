using Microsoft.AspNetCore.Mvc;
using ProductMove_API.Service.BaoCaoService;
using ProductMove_Model;

namespace ProductMove_API.Controllers
{
    public class BaoCaosController : ConBase
    {
        private readonly IBaoCaoService _repo;
        public BaoCaosController(IBaoCaoService baoCaoService)
        {
            _repo = baoCaoService;
        }

        [HttpGet("getAllBaoCao")]
        public async Task<IActionResult> getgetAllBaoCao()
        {
            try
            {
                return Ok(await _repo.getAllBaoCaoAsync());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("getBaoCaoById/{id}")]
        public async Task<IActionResult> getBaoCaoById(int id)
        {
            var baocao = await _repo.getBaoCaoByIdAsync(id);
            return baocao == null ? NotFound() : Ok(baocao);
        }

        [HttpPost("addBaoCao")]
        public async Task<IActionResult> addBaoCao(BaoCao baoCao)
        {
            try
            {
                var newBaocao = await _repo.addBaoCaoAsync(baoCao);
                var baocao = await _repo.getBaoCaoByIdAsync(newBaocao);
                return baocao == null ? NotFound() : Ok(baocao);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut("updateBaoCao/{id}")]
        public async Task<IActionResult> updateBaoCao(int id, BaoCao baoCao)
        {
            try
            {
                await _repo.updateBaoCaoAsync(id, baoCao);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpDelete("deleteBaoCao/{id}")]
        public async Task<IActionResult> deleteBaoCao(int id)
        {
            try
            {
                await _repo.deleteBaoCaoAsync(id);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
