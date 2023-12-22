using BusinessAccessLayer.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InnogotchiBodyPartController : ControllerBase
    {
        private readonly IInnogotchiBodyPartService _innogotchiBodyPartService;

        public InnogotchiBodyPartController(IInnogotchiBodyPartService innogotchiBodyPartService)
        {
            _innogotchiBodyPartService = innogotchiBodyPartService;
        }

        [HttpGet("innogotchi-body-image")]
        public async Task<ActionResult<string>> GetInnnpgotchiBodyImage(int innogotchiBodyPartNumber)
        {
            return Ok(await _innogotchiBodyPartService.GetBodyPartImage(1, innogotchiBodyPartNumber)); // 1 - id body 
        }

        [HttpGet("innogotchi-eyes-image")]
        public async Task<ActionResult<string>> GetInnnpgotchiEyesImage(int innogotchiBodyPartNumber)
        {
            return Ok(await _innogotchiBodyPartService.GetBodyPartImage(2, innogotchiBodyPartNumber)); // 2 - id eyes
        }

        [HttpGet("innogotchi-mouth-image")]
        public async Task<ActionResult<string>> GetInnnpgotchiMouthImage(int innogotchiBodyPartNumber)
        {
            return Ok(await _innogotchiBodyPartService.GetBodyPartImage(3, innogotchiBodyPartNumber)); // 3 - id mouth
        }

        [HttpGet("innogotchi-nose-image")]
        public async Task<ActionResult<string>> GetInnnpgotchiNoseImage(int innogotchiBodyPartNumber)
        {
            return Ok(await _innogotchiBodyPartService.GetBodyPartImage(4, innogotchiBodyPartNumber)); // 4 - id nose
        }
    }
}
