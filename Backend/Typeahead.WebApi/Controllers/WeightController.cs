using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Typeahead.WebApi.Data;
using Typeahead.WebApi.Filters;
using Typeahead.WebApi.Model;

namespace Typeahead.WebApi.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("weight")]
    public class WeightController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly TermsRepository _repository;

        public WeightController(ILogger logger, TermsRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        [HttpPost(Name = "WeightIncrease")]
        [ProducesHttpStatusCode(HttpStatusCode.NoContent)]
        public async Task<IActionResult> Post([FromBody] WeightInput weightInput)
        {
            _logger.LogDebug("Weight increase for term with id = {termId}", weightInput.TermId);

            await _repository.WeightIncrease(weightInput.TermId, weightInput.Input);

            return NoContent();
        }
    }
}