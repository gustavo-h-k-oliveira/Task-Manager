using Microsoft.AspNetCore.Mvc;

namespace TaskManager.Controllers
{
    [ApiController]
    [Route("/")]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Bem-vindo à API TaskManager!");
        }
    }
}