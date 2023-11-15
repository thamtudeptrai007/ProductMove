using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductMove_API.Service.NhapKhoSerice;
using ProductMove_API.Service.SeriService;
using ProductMove_Model;

namespace ProductMove_API.Controllers
{
    public class SerisController : ConBase
    {
        private readonly ISeriService _repo;

        public SerisController(ISeriService repo)
        {
            _repo = repo;
        }

        [HttpGet("getAllSeri")]
        public async Task<IActionResult> getAllSeriAsync()
        {
            try
            {
                return Ok(await _repo.getAllSeriAsync());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("getSeriById/{id}")]
        public async Task<IActionResult> getSeriAsync(int id)
        {
            var data = await _repo.getSeriAsync(id);
            return data == null ? NotFound() : Ok(data);
        }

        [HttpGet("getSeriByName/{name}")]
        public async Task<IActionResult> getSeriByNameAsync(string name)
        {
            var kho = await _repo.getSeriByNameAsync(name);
            return kho == null ? NotFound() : Ok(kho);
        }

        [HttpGet("getAllspByTTSP/{name}")]
        public async Task<IActionResult> getAllspByTTSPAsync(string name)
        {
            var data = await _repo.getSpByTTSPAsync(name);
            return data == null ? NotFound() : Ok(data);
        }

        [HttpGet("getTopSpBySeri/{top}/{idsp}/{idkho}")]
        public async Task<IActionResult> getTopSpAsync(int top, int idsp, int idkho)
        {
            try
            {
                var data = await _repo.getTopSeriAsync(top, idsp, idkho);
                return data == null ? NotFound() : Ok(data);
            }
            catch
            {
                return BadRequest();
            }

        }

        [HttpGet("getList_IDkho_TTSP/{idkho}/{ttsp}")]
        public async Task<IActionResult> getList_IDkho_TTSP(int idkho, string ttsp)
        {
            try
            {
                var data = await _repo.getList_IDkho_TTSP(idkho, ttsp);
                return data == null ? NotFound() : Ok(data);
            }
            catch
            {
                return BadRequest();
            }

        }

        [HttpPost("addSeri")]
        public async Task<IActionResult> addSeriAsync(Seri nhapKho)
        {
            try
            {
                var newsp = await _repo.addSeriAsync(nhapKho);
                var nhapkho = await _repo.getSeriAsync(newsp);
                return nhapkho == null ? NotFound() : Ok(nhapKho);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut("updateSeri/{id}")]
        public async Task<IActionResult> updateSeriAsync(int id, Seri nhapKho)
        {
            try
            {
                await _repo.updateSeriAsync(id, nhapKho);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpDelete("deleteSeri/{id}")]
        public async Task<IActionResult> deleteSeriAsync(int id)
        {
            try
            {
                await _repo.deleteSeriAsync(id);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
