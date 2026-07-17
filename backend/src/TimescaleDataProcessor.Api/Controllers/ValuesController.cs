using Microsoft.AspNetCore.Mvc;
using TimescaleDataProcessor.Api.Services;

namespace TimescaleDataProcessor.Api.Controllers
{
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly ITimescaleDataImportService _importService;
        private readonly IValuesService _valuesService;

        public ValuesController(ITimescaleDataImportService importService, IValuesService valuesService)
        {
            _importService = importService;
            _valuesService = valuesService;
        }

        [HttpPost]
        [Route("api/values/import")]
        public async Task<IActionResult> ImportAsync(IFormFile file, CancellationToken ct)
        {
            try
            {
                await using var stream = file.OpenReadStream();
                await _importService.ProcessAsync(stream, file.FileName, ct);

                return Ok("Значения успешно импортированы");
            }
            catch (Exception ex)
            {
                return BadRequest($"Ошибка при импорте значений: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("api/values/latest")]
        public async Task<IActionResult> GetLatestAsync(CancellationToken ct)
        {
            return Ok(await _valuesService.GetLatestValuesAsync(ct));
        }
    }
}
