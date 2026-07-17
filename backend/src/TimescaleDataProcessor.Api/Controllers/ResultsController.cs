using Microsoft.AspNetCore.Mvc;
using TimescaleDataProcessor.Api.Dtos;
using TimescaleDataProcessor.Api.Services;

namespace TimescaleDataProcessor.Api.Controllers
{
    [ApiController]
    public class ResultsController : ControllerBase
    {
        private readonly IResultsService _service;

        public ResultsController(IResultsService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("api/results")]
        public async Task<IActionResult> GetAsync([FromQuery] ResultsFilterDto filter)
        {
            return Ok(await _service.GetFilteredResultsAsync(filter));
        }
    }
}
