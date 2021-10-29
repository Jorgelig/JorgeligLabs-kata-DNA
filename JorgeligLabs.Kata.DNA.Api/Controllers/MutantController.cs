using JorgeligLabs.Kata.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace JorgeligLabs.Kata.DNA.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MutantController : ControllerBase
    {

        private readonly ILogger<MutantController> _logger;
        private readonly IEvaluationService _evaluationService;

        public MutantController(ILogger<MutantController> logger, IEvaluationService evaluationService)
        {
            _logger = logger;
            _evaluationService = evaluationService;
        }

        [HttpPost(Name = "mutation")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public IActionResult Post([FromBody]MutationRequest? request)
        {
            if(request == null || request?.DNA?.Count() == 0 || request?.DNA.Count(i => string.IsNullOrWhiteSpace(i)) > 0) 
                return new StatusCodeResult(StatusCodes.Status403Forbidden);
            try
            {
                var isMutation = _evaluationService.HasMutation(request?.DNA);


                return isMutation
                    ? new StatusCodeResult(StatusCodes.Status403Forbidden)
                    : Ok(request);
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex?.InnerException?.Message ?? ex?.Message);
                return new StatusCodeResult(StatusCodes.Status403Forbidden);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex?.InnerException?.Message ?? ex?.Message);
                throw;
            }
        

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