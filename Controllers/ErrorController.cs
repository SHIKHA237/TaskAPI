using Microsoft.AspNetCore.Mvc;

namespace TaskAPI.Controllers
{
    public class ErrorController : ControllerBase
    {
        [Route("/error")]
        [HttpGet]
        public IActionResult Error()
        {
            return Problem();
        }
    }
}
