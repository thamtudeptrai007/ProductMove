using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ProductMove_API.Controllers
{
    public class WelcomeController : ConBase
    {
        [HttpGet]
        public ActionResult<object> Get()
        {
            return new
            {
                greeting = "Welcome to ProductMove API Server",
                version = "1.0"
            };
        }

    }
}
