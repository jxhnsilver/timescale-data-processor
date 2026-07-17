using Microsoft.AspNetCore.Mvc;
using TimescaleDataProcessor.Api.Services;

namespace TimescaleDataProcessor.Api.Controllers
{
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly ITimescaleDataImportService _importService;

        public ValuesController(ITimescaleDataImportService importService)
        {
            _importService = importService;
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
    }
}
