using BusinessAccessLayer.DTOs;
using BusinessAccessLayer.Services;
using BusinessAccessLayer.Validators;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Web_Api;

namespace Web_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FarmController : ControllerBase
    {
        private readonly IFarmService _farmService;
        private readonly IUserFarmService _userFarmService;
        private readonly IFarmValidatorFactory _farmValidatorFactory;
        private readonly IConfiguration _configuration;

        public FarmController(IFarmService farmService, IFarmValidatorFactory farmValidatorFactory, IUserFarmService userFarmService, IConfiguration configuration)
        {
            _farmService = farmService;
            _userFarmService = userFarmService;
            _farmValidatorFactory = farmValidatorFactory;
            _configuration = configuration;
        }

        [HttpPost("log-in-farm")]
        public async Task<ActionResult> GetToken(UserFarmDTO userFarmDTO)
        {
            string token = await CreateToken(userFarmDTO);

            return Ok(token);
        }

        [HttpPost("create-farm")]
        public async Task<ActionResult<FarmDTO>> CreateFarm(FarmCreateDTO farmCreateDTO)
        {
            var farmvalidator = _farmValidatorFactory.GetValidator<FarmCreateDTO>();
            var validationresult = await farmvalidator.ValidateAsync(farmCreateDTO);

            if (!validationresult.IsValid)
            {
                return BadRequest(validationresult.Errors);
            }

            FarmDTO farmDTO = await _farmService.CreateFarm(farmCreateDTO);

            //добавление роли admin для User
            UserFarmDTO userFarmDTO = new UserFarmDTO(farmDTO.UserId, farmDTO.FarmId, 1);
            await _userFarmService.CreateUserFarm(userFarmDTO);

            return Ok(farmDTO);
        }

        [HttpGet("all-user-farms")]
        public async Task<ActionResult<List<FarmUserAllDTO>>> GetAllUserFarms(int userId)
        {
            return Ok(await _farmService.GetAllUserFarms(userId));
        }

        [HttpGet("collaborators"), Authorize(Roles = "Admin", Policy = "FarmId")] //////////////////////////
        public async Task<ActionResult<List<string>>> GetCollaborators(int farmId)
        {
            return Ok(await _farmService.GetCollaborators(farmId));
        }

        [HttpPost("add-collaborator"), Authorize(Roles = "Admin", Policy = "FarmId")] //////////////////////////
        public async Task<ActionResult<UserFarmDTO>> AddCollaborator(int farmId, UserFarmDTO userFarmDTO)
        {
            UserFarmDTO userFarmDTOCollaborator = new UserFarmDTO(userFarmDTO.UserId, userFarmDTO.FarmId, 2);
            return Ok(await _userFarmService.CreateUserFarm(userFarmDTOCollaborator));
        }

        //[CustomAuthorize(Roles = "Admin", FarmId = nameof(GetFarmId))]
        [HttpGet("farm-statistics"), Authorize(Roles = "Admin", Policy = "FarmId")] //////////////////////////////
        public async Task<ActionResult<FarmStatisticDTO>> GetFarmStatistic(int farmId)
        {
            return Ok(await _farmService.GetFarmStatistic(farmId));
        }

        private async Task<string> CreateToken(UserFarmDTO userFarmDTO) 
        {
            UserFarmDTO userFarmDTOWithRole = await _userFarmService.GetUserFarm(userFarmDTO);
            string role = userFarmDTOWithRole.RoleId == 1 ? "Admin" : "User";

            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userFarmDTO.UserId.ToString()),
                new Claim("FarmId", userFarmDTOWithRole.FarmId.ToString()),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, role)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.Now.AddDays(1),
                    signingCredentials: creds
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        //public string GetFarmId()
        //{
        //    return _httpContextAccessor.HttpContext.Request.Query["FarmId"];
        //}
    }
}
