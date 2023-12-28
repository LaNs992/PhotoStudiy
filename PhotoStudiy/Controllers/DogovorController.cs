using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PhotoStudiy.API.Exceptions;
using PhotoStudiy.API.Models;
using PhotoStudiy.API.Models.CreateRequest;
using PhotoStudiy.API.Models.Request;
using PhotoStudiy.API.Models.Response;
using PhotoStudiy.Services.Contracts.Interface;
using PhotoStudiy.Services.Contracts.ModelReqest;
using System.ComponentModel.DataAnnotations;

namespace PhotoStudiy.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [ApiExplorerSettings(GroupName = "Dogovor")]

    public class DogovorController : ControllerBase
    {
        private readonly IDogovorService dogovorService;
        private readonly IMapper mapper;

        public DogovorController(IDogovorService dogovorService, IMapper mapper)
        {
            this.dogovorService = dogovorService;
            this.mapper = mapper;
        }

        /// <summary>
        /// Получить список билетов
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(ICollection<DogovorResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await dogovorService.GetAllAsync(cancellationToken);
            var result2 = result.Select(x => mapper.Map<DogovorResponse>(x));
            return Ok(result2);
        }

        /// <summary>
        /// Получить билет по Id
        /// </summary>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(DogovorResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiExceptionsDetail), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById([Required] Guid id, CancellationToken cancellationToken)
        {
            var item = await dogovorService.GetByIdAsync(id, cancellationToken);
            return Ok(mapper.Map<DogovorResponse>(item));
        }

        /// <summary>
        /// Добавить билет
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(DogovorResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiValidationExceptionsDetail  ), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ApiExceptionsDetail), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Add(CreateDogovorRequest request, CancellationToken cancellationToken)
        {
            var model = mapper.Map<DogovorRequestModel>(request);
            var result = await dogovorService.AddAsync(model, cancellationToken);
            return Ok(mapper.Map<DogovorResponse>(result));
        }

        /// <summary>
        /// Изменить билет по Id
        /// </summary>
        [HttpPut]
        [ProducesResponseType(typeof(DogovorResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiExceptionsDetail), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiExceptionsDetail), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Edit(DogovorRequest request, CancellationToken cancellationToken)
        {
            var model = mapper.Map<DogovorRequestModel>(request);
            var result = await dogovorService.EditAsync(model, cancellationToken);
            return Ok(mapper.Map<DogovorResponse>(result));
        }

        /// <summary>
        /// Удалить билет по Id
        /// </summary>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiExceptionsDetail), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiExceptionsDetail), StatusCodes.Status417ExpectationFailed)]
        public async Task<IActionResult> Delete([Required] Guid id, CancellationToken cancellationToken)
        {
            await dogovorService.DeleteAsync(id, cancellationToken);
            return Ok();
        }
    }
}
