using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PhotoStudiy.API.Exceptions;
using PhotoStudiy.API.Models;
using PhotoStudiy.API.Models.CreateRequest;
using PhotoStudiy.API.Models.Request;
using PhotoStudiy.API.Models.Response;
using PhotoStudiy.Services.Contracts.Interface;
using PhotoStudiy.Services.Contracts.Models;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace PhotoStudiy.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [ApiExplorerSettings(GroupName = "Recvisit")]

    public class RecvisitController : ControllerBase
    {
        private readonly IRecvisitService recvisitService;
        private readonly IMapper mapper;

        public RecvisitController(IRecvisitService recvisitService, IMapper mapper)
        {
            this.recvisitService = recvisitService;
            this.mapper = mapper;
        }

        /// <summary>
        /// Получить список Реквизитов
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(ICollection<RecvisitResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await recvisitService.GetAllAsync(cancellationToken);
            return Ok(result.Select(x => mapper.Map<RecvisitResponse>(x)));
        }

        /// <summary>
        /// Получить Реквизит по Id
        /// </summary>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(RecvisitResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiExceptionsDetail), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiExceptionsDetail), StatusCodes.Status417ExpectationFailed)]
        public async Task<IActionResult> GetById([Required] Guid id, CancellationToken cancellationToken)
        {
            var item = await recvisitService.GetByIdAsync(id, cancellationToken);
            return Ok(mapper.Map<RecvisitResponse>(item));
        }

        /// <summary>
        /// Добавить Реквизит
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(RecvisitResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiValidationExceptionsDetail  ), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ApiExceptionsDetail), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Add(CreateRecvisitRequest model, CancellationToken cancellationToken)
        {
            var filmModel = mapper.Map<RecvisitModel>(model);
            var result = await recvisitService.AddAsync(filmModel, cancellationToken);
            return Ok(mapper.Map<RecvisitResponse>(result));
        }

        /// <summary>
        /// Изменить Реквизит по Id
        /// </summary>
        [HttpPut]
        [ProducesResponseType(typeof(RecvisitResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiExceptionsDetail), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiValidationExceptionsDetail), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ApiExceptionsDetail), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Edit(RecvisitRequest request, CancellationToken cancellationToken)
        {
            var model = mapper.Map<RecvisitModel>(request);
            var result = await recvisitService.EditAsync(model, cancellationToken);
            return Ok(mapper.Map<RecvisitResponse>(result));
        }

        /// <summary>
        /// Удалить Реквизит по Id
        /// </summary>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiExceptionsDetail), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiExceptionsDetail), StatusCodes.Status417ExpectationFailed)]
        public async Task<IActionResult> Delete([Required] Guid id, CancellationToken cancellationToken)
        {
            await recvisitService.DeleteAsync(id, cancellationToken);
            return Ok();
        }
    }
}
