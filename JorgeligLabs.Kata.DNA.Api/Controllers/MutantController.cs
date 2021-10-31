using JorgeligLabs.Kata.Core.Interfaces;
using JorgeligLabs.Kata.DNA.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JorgeligLabs.Kata.DNA.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MutantController : ControllerBase
    {

        private readonly ILogger<MutantController> _logger;
        private readonly IEvaluationService _evaluationService;
        private readonly IStorageService _storageService;

        public MutantController(ILogger<MutantController> logger, 
            IEvaluationService evaluationService,
            IStorageService storage)
        {
            _logger = logger;
            _evaluationService = evaluationService;
            _storageService = storage;
        }

        [Authorize]
        [HttpPost("/mutation")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public IActionResult Post([FromBody]MutationRequest? request)
        {
            if(request == null || request?.DNA?.Count() == 0 || request?.DNA.Count(i => string.IsNullOrWhiteSpace(i)) > 0) 
                return new StatusCodeResult(StatusCodes.Status403Forbidden);
            try
            {
                var isMutation = _evaluationService.HasMutation(request?.DNA);
                var item = _storageService.InsertOrUpdate(request.DNA, isMutation);

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

        [Authorize]
        [HttpGet("/stats")]
        public MutantStatsResponse Get()
        {
            var mutans = _storageService.GetMutants();
            var humans = _storageService.GetHumans();

            return new MutantStatsResponse
            {
                CountMutations = mutans.Length,
                CountNoMutation = humans.Length,
            };
        }
    }
}