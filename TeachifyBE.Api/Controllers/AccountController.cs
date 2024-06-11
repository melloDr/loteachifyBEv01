using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeachifyBE_Business.Services;
using TeachifyBE_Data.Models.ResultModel;
using TeachifyBE_Data.Models.UserModel;

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
        public async Task<IActionResult> RegisterUser([FromBody] RegisterUserResquestModel model)
        {
            ResultModel result = await _service.RegisterUser(model.email, model.password, model.confirmPassword);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpPost("ChangePassword")]
        [Authorize]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordResquestModel model)
        {
            string token = Request.Headers["Authorization"].ToString().Split(" ")[1];
            ResultModel result = await _service.ChangePassword(model.oldPassword, model.newPassword, model.confirmPassword, token);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [Authorize]
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

        [HttpPost("PasswordRecovery")]
        public async Task<IActionResult> PasswordRecovery([FromBody] string email)
        {
            ResultModel result = await _service.PasswordRecovery(email);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
    }
}
