using Microsoft.AspNetCore.Mvc;

namespace GrowControl.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        public HomeController()
        {
        }

        [HttpGet("Ping")]
        public async Task<IActionResult> Ping()
        {
            return Ok("pong");
        }
    }
}
