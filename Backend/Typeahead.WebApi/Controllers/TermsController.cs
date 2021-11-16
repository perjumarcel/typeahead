using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Typeahead.WebApi.Data;
using Typeahead.WebApi.Filters;

namespace Typeahead.WebApi.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("terms")]
    public class TermsController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly TermsRepository _repository;

        public TermsController(ILogger<TermsController> logger, TermsRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        [HttpGet("{filter:minlength(3):maxlength(50)}", Name = "GetFilteredTerms")]
        [ProducesHttpStatusCode(HttpStatusCode.OK)]
        [ProducesHttpStatusCode(HttpStatusCode.NotFound)]
        public async Task<IActionResult> Get(string filter)
        {
            _logger.LogDebug("Get filtered terms by {filter}", filter);

            var results = await _repository.GetFilteredTerms(filter);
            if (!results.Any())
            {
                return NotFound($"No terms found for input {filter}");
            }

            return Ok(results);
        }
    }
}