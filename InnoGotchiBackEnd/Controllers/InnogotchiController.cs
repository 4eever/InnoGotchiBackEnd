using BusinessAccessLayer.DTOs;
using BusinessAccessLayer.Services;
using BusinessAccessLayer.Validators;
using DataAccessLayer.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Web_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InnogotchiController : ControllerBase
    {
        private readonly IInnogotchiService _innogotchiService;
        private readonly IInnogotchiValidatorFactory _innogotchiValidatorFactory;

        public InnogotchiController(IInnogotchiService innogotchiService, IInnogotchiValidatorFactory innogotchiValidatorFactory)
        {
            _innogotchiService = innogotchiService;
            _innogotchiValidatorFactory = innogotchiValidatorFactory;
        }

        [HttpPost("create-innogotchi"), Authorize(Roles = "Admin", Policy = "FarmId")] //////////
        public async Task<ActionResult<InnogotchiCreateDTO>> CreateInnogotchi(int farmId, InnogotchiCreateDTO innogotchiCreateDTO)
        {
            var innogotchiValidator = _innogotchiValidatorFactory.GetValidator<InnogotchiCreateDTO>();
            var validationResult = await innogotchiValidator.ValidateAsync(innogotchiCreateDTO);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            return Ok(await _innogotchiService.CreateInnogotchi(innogotchiCreateDTO));
        }

        [HttpGet("farm-innogotchies")]
        public async Task<ActionResult<List<InnogotchiInformationDTO>>> GetFarmInnogotchies(int farmId)
        {
            return Ok(await _innogotchiService.GetFarmInnogotchies(farmId));
        }

        [HttpGet("all-innogotchies")]
        public async Task<ActionResult<List<InnogotchiInformationDTO>>> GetAllInnogotchies(int userId)
        {
            return Ok(await _innogotchiService.GetAllInnogotchies(userId));
        }

        [HttpPut("feed")]
        public async Task<ActionResult> Feed(int innogotchiId) //если показатель сытости full, то нельзя кормить
        {
            await _innogotchiService.Feed(innogotchiId);

            return Ok();
        }

        [HttpPut("drink")]
        public async Task<ActionResult> Drink(int innogotchiId) //если показатель жажды full, то нельзя поить
        {
            await _innogotchiService.Drink(innogotchiId);

            return Ok();
        }

        [HttpDelete("dead")]
        public async Task<ActionResult> Dead(int innogotchiId)
        {
            await _innogotchiService.Dead(innogotchiId);

            return Ok();
        }
    }
}
