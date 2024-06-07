using Microsoft.AspNetCore.Mvc;
using TeachifyBE_Business.Services;
using TeachifyBE_Data.Models.ResultModel;

namespace TeachifyBE.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CourseController : Controller
    {
        private readonly IGeneralService _service;
        public CourseController(IGeneralService service)
        {
            _service = service;
        }

        //[Authorize]
        [HttpGet("courses")]
        public async Task<IActionResult> GetAllCourses()
        {
            ResultModel result = await _service.Courses();
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
    }
}
