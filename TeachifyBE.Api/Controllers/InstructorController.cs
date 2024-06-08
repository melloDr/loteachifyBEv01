using Microsoft.AspNetCore.Mvc;
using TeachifyBE_Business.Services;
using TeachifyBE_Data.Models.InstructorModel;
using TeachifyBE_Data.Models.ResultModel;

namespace TeachifyBE.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InstructorController : Controller
    {
        private readonly IGeneralService _service;
        public InstructorController(IGeneralService service)
        {
            _service = service;
        }
        //[Authorize]
        [HttpPost("instructor")]
        public async Task<IActionResult> BecomeAnInstructor([FromBody] InstructorModel model)
        {
            ResultModel result = await _service.BecomeAnInstructor(model);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpGet("instructors")]
        public async Task<IActionResult> GetIntructor(Guid? id, string? subject, string? gender, string? city)
        {
            ResultModel result = await _service.GetIntructors(id, subject, gender, city);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
    }
}
