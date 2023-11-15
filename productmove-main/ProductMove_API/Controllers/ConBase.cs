using Microsoft.AspNetCore.Mvc;

namespace ProductMove_API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public abstract class ConBase : ControllerBase
    {
    }
}
