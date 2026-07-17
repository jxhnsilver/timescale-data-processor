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

        /// <summary>
        /// Получает список результатов с применением фильтра
        /// </summary>
        /// <param name="filter">Параметры фильтрации</param>
        /// <param name="ct">Токен отмены операции</param>
        /// <returns>Возвращает список отфильтрованных результатов</returns>
        [HttpGet]
        [Route("api/results")]
        [ProducesResponseType(StatusCodes.Status200OK, Description = "Список отфильтрованных результатов успешно получен")]
        public async Task<IActionResult> GetAsync([FromQuery] ResultsFilterDto filter, CancellationToken ct)
        {
            return Ok(await _service.GetFilteredResultsAsync(filter, ct));
        }
    }
}
