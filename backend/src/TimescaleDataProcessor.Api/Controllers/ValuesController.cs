using Microsoft.AspNetCore.Mvc;
using TimescaleDataProcessor.Api.Dtos;
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

        /// <summary>
        /// Импортирует файл со значениями в базу данных
        /// </summary>
        /// <remarks>
        /// Поддерживаемые форматы: CSV
        /// </remarks>
        /// <param name="file">Файл с данными для импорта</param>
        /// <param name="ct">Токен отмены операции</param>
        /// <returns>Сообщение об успешном импорте или ошибке</returns>
        [HttpPost]
        [Route("api/values/import")]
        [ProducesResponseType(StatusCodes.Status200OK, Description = "Данные из файла успешно импортированы")]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest, Description = "Ошибка формата или структуры файла")]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity, Description = "Нарушение бизнес-правил при обработке данных")]
        public async Task<IActionResult> ImportAsync(IFormFile file, CancellationToken ct)
        {
            await using var stream = file.OpenReadStream();
            await _importService.ProcessAsync(stream, file.FileName, ct);

            return Ok("Значения успешно импортированы");
        }

        /// <summary>
        /// Возвращает последние 10 значений, отсортированных по дате начала операции и имени файла
        /// </summary>
        /// <param name="ct">Токен отмены операции</param>
        /// <returns>Список последних 10 значений</returns>
        [HttpGet]
        [Route("api/values/latest")]
        [ProducesResponseType(typeof(IReadOnlyList<ValueRecordDto>), StatusCodes.Status200OK, Description = "Список последних 10 значений успешно получен")]
        public async Task<IActionResult> GetLatestAsync(CancellationToken ct)
        {
            return Ok(await _valuesService.GetLatestValuesAsync(ct));
        }
    }
}
