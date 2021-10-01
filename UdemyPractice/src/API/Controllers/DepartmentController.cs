
using BLL.Services;
using System.Threading.Tasks;
using BLL.Request;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class DepartmentController : MainApiController
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _departmentService.GetAllAsync());
        }

        [HttpGet("{code}")]
        public async Task<IActionResult> GetA(string code)
        {
            return Ok(await _departmentService.GetAAsync(code));
        }

        [HttpPost]
        public async Task<IActionResult> Insert(InsertDepartmentRequestModel reqModel)
        {
            return Ok(await _departmentService.InsertAsync(reqModel));
        }

        [HttpPut("{code}")]
        public async Task<IActionResult> Update(string code, UpdateDepartmentRequestModel reqModel)
        {
            return Ok(await _departmentService.Update(code, reqModel));
        }

        [HttpDelete("{code}")]
        public async Task<IActionResult> Delete(string code)
        {
            return Ok(await _departmentService.Delete(code));
        }
    }
}