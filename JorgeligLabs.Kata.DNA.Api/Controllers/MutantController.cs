using Microsoft.AspNetCore.Mvc;

namespace JorgeligLabs.Kata.DNA.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MutantController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<MutantController> _logger;

        public MutantController(ILogger<MutantController> logger)
        {
            _logger = logger;
        }

        [HttpPost(Name = "mutation")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public IActionResult Post(MutationRequest? request)
        {
            if(request == null || request?.DNA?.Count() == 0 || request?.DNA.Count(i => string.IsNullOrWhiteSpace(i)) > 0) return new StatusCodeResult(StatusCodes.Status403Forbidden);
            return Ok(request);

        }

        [HttpGet(Name = "stats")]
        public MutantStatsResponse Get()
        {
            return new MutantStatsResponse
            {
                CountMutations = 40,
                CountNoMutation = 100
            };
        }
    }
}