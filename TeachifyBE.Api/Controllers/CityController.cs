using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeachifyBE_Business.Services;
using TeachifyBE_Data.Models.ResultModel;

namespace TeachifyBE.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CityController : Controller
    {
        private readonly IGeneralService _service;
        public CityController(IGeneralService service)
        {
            _service = service;
        }


        //[Authorize]
        [HttpGet("cities")]
        public async Task<IActionResult> GetAllCities()
        {
            ResultModel result = await _service.Cities();
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
    }
}
