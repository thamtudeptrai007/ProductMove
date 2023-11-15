using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductMove_API.Service.HoaDonService;
using ProductMove_Model;

namespace ProductMove_API.Controllers
{
    public class HoaDonsController : ConBase
    {
        private readonly IHoaDonService _repo;

        public HoaDonsController(IHoaDonService repo)
        {
            _repo = repo;
        }

        [HttpGet("getAllHoaDon")]
        public async Task<IActionResult> getAllHoaDon()
        {
            try
            {
                return Ok(await _repo.getAllHoaDon());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("getidhoadonnew")]
        public async Task<IActionResult> getIdHoaDonNew()
        {
            try
            {
                return Ok(await _repo.getidhoadonnew());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("getHoaDonByid/{id}")]
        public async Task<IActionResult> getHoaDonByid(int id)
        {
            var kho = await _repo.getHoaDonByid(id);
            return kho == null ? NotFound() : Ok(kho);
        }

        [HttpPost("addNewHoadon")]
        public async Task<IActionResult> addNewHoaDongAsync(HoaDon hoadon)
        {
            try
            {
                var req = await _repo.addNewHoadon(hoadon);
                var res = await _repo.getHoaDonByid(req);
                return res == null ? NotFound() : Ok(res);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut("updateHoadon/{id}")]
        public async Task<IActionResult> updateHoadon(int id, HoaDon hoadon)
        {
            try
            {
                await _repo.updateHoadon(id, hoadon);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
