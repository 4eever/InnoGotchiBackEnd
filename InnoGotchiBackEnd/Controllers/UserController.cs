using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using BusinessAccessLayer.DTOs;
using BusinessAccessLayer.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using BusinessAccessLayer.Validators;

namespace Web_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IUserValidatorFactory _userValidatorFactory;

        public UserController(IUserService userService, IUserValidatorFactory userValidatorFactory)
        {
            _userService = userService;
            _userValidatorFactory = userValidatorFactory;
        }

        [HttpPost("sing-up")]
        public async Task<ActionResult<UserDTO>> SingUp(UserSignUpDTO userSignUpDTO)
        {
            var userValidator = _userValidatorFactory.GetValidator<UserSignUpDTO>();
            var validationResult = await userValidator.ValidateAsync(userSignUpDTO);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            return Ok(await _userService.SignUp(userSignUpDTO));
        }

        [HttpPost("log-in")]
        public async Task<ActionResult<UserDTO>> LogIn(UserLogInDTO userLogInDTO)
        {
            var userValidator = _userValidatorFactory.GetValidator<UserLogInDTO>();
            var validationResult = await userValidator.ValidateAsync(userLogInDTO);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            return Ok(await _userService.LogIn(userLogInDTO));
        }

        [HttpPut("account-detales")]
        public async Task<ActionResult<UserDTO>> UpdateUser(UserDTO userDTO)
        {
            var userValidator = _userValidatorFactory.GetValidator<UserDTO>();
            var validationResult = await userValidator.ValidateAsync(userDTO);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            return Ok(await _userService.UpdateUser(userDTO));
        }
    }
}
