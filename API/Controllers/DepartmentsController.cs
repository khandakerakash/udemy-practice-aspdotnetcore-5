using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepartmentsController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok("get all departments.");
        }

        [HttpGet("{code}")]
        public IActionResult GetA(string code)
        {
            return Ok("get this " + code + " department data.");
        }

        [HttpPost]
        public IActionResult Insert()
        {
            return Ok("Insert a new department.");
        }

        [HttpPut("{code}")]
        public IActionResult Update(string code)
        {
            return Ok("Update this " + code + " department data.");
        }

        [HttpDelete("{code}")]
        public IActionResult Delete(string code)
        {
            return Ok("Delete this " + code + " department data.");
        }
    }
}