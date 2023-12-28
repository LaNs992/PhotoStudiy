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
    [ApiExplorerSettings(GroupName = "Uslugi")]

    public class UslugiController : ControllerBase
    {
        private readonly IUslugiService uslugiService;
        private readonly IMapper mapper;

        public UslugiController(IUslugiService uslugiService, IMapper mapper)
        {
            this.uslugiService = uslugiService;
            this.mapper = mapper;
        }

        /// <summary>
        /// Получить список Услуг
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(ICollection<UslugiResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await uslugiService.GetAllAsync(cancellationToken);
            return Ok(result.Select(x => mapper.Map<UslugiResponse>(x)));
        }

        /// <summary>
        /// Получить Услугу по Id
        /// </summary>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(UslugiResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiExceptionsDetail), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiExceptionsDetail), StatusCodes.Status417ExpectationFailed)]
        public async Task<IActionResult> GetById([Required] Guid id, CancellationToken cancellationToken)
        {
            var item = await uslugiService.GetByIdAsync(id, cancellationToken);
            return Ok(mapper.Map<UslugiResponse>(item));
        }

        /// <summary>
        /// Добавить Услугу
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(UslugiResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiValidationExceptionsDetail  ), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ApiExceptionsDetail), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Add(CreateUslugiRequest model, CancellationToken cancellationToken)
        {
            var uslugiModel = mapper.Map<UslugiModel>(model);
            var result = await uslugiService.AddAsync(uslugiModel, cancellationToken);
            return Ok(mapper.Map<UslugiResponse>(result));
        }

        /// <summary>
        /// Изменить Услугу по Id
        /// </summary>
        [HttpPut]
        [ProducesResponseType(typeof(UslugiResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiExceptionsDetail), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiValidationExceptionsDetail  ), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ApiExceptionsDetail), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Edit(UslugiRequest request, CancellationToken cancellationToken)
        {
            var model = mapper.Map<UslugiModel>(request);
            var result = await uslugiService.EditAsync(model, cancellationToken);
            return Ok(mapper.Map<UslugiResponse>(result));
        }

        /// <summary>
        /// Удалить Услугу по Id
        /// </summary>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiExceptionsDetail), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiExceptionsDetail), StatusCodes.Status417ExpectationFailed)]
        public async Task<IActionResult> Delete([Required] Guid id, CancellationToken cancellationToken)
        {
            await uslugiService.DeleteAsync(id, cancellationToken);
            return Ok();
        }
    }
}
