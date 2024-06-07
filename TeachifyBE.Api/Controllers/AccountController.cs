using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeachifyBE_Business.Services;
using TeachifyBE_Data.Models.ResultModel;

namespace TeachifyBE.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly IGeneralService _service;
        public AccountController(IGeneralService service)
        {
            _service = service;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterUser(string email, string password, string confirmPassword)
        {
            ResultModel result = await _service.RegisterUser(email, password, confirmPassword);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpGet("GetAllUser")]
        public async Task<IActionResult> GetAllUser()
        {
            ResultModel result = await _service.GetListUserAll();
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpGet("Token")]
        public async Task<IActionResult> Token(string email, string password)
        {
            ResultModel result = await _service.Token(email, password);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
    }
}
