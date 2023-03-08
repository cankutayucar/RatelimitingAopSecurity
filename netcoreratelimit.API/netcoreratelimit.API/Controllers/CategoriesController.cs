using Microsoft.AspNetCore.Mvc;

namespace netcoreratelimit.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetCategory()
        {
            return Ok(new { Id = 1, Category = "Kırtasiye" });
        }
    }
}
