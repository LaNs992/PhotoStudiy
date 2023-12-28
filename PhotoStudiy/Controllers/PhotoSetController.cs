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
    [ApiExplorerSettings(GroupName = "PhotoSet")]

    public class PhotoSetController : ControllerBase
    {
        private readonly IPhotoSetService photoSetService;
        private readonly IMapper mapper;

        public PhotoSetController(IPhotoSetService photoSetService, IMapper mapper)
        {
            this.photoSetService = photoSetService;
            this.mapper = mapper;
        }

        /// <summary>
        /// Получить список Фотосетов
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(ICollection<PhotoSetResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await photoSetService.GetAllAsync(cancellationToken);
            return Ok(result.Select(x => mapper.Map<PhotoSetResponse>(x)));
        }

        /// <summary>
        /// Получить Фотосет по Id
        /// </summary>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(PhotoSetResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiExceptionsDetail), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiExceptionsDetail), StatusCodes.Status417ExpectationFailed)]
        public async Task<IActionResult> GetById([Required] Guid id, CancellationToken cancellationToken)
        {
            var item = await photoSetService.GetByIdAsync(id, cancellationToken);
            return Ok(mapper.Map<PhotoSetResponse>(item));
        }

        /// <summary>
        /// Добавить Фотосет
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(PhotoSetResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiValidationExceptionsDetail), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ApiExceptionsDetail), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Add(CreatePhotoSetRequest model, CancellationToken cancellationToken)
        {
            var filmModel = mapper.Map<PhotoSetModel>(model);
            var result = await photoSetService.AddAsync(filmModel, cancellationToken);
            return Ok(mapper.Map<PhotoSetResponse>(result));
        }

        /// <summary>
        /// Изменить Фотосет по Id
        /// </summary>
        [HttpPut]
        [ProducesResponseType(typeof(PhotoSetResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiExceptionsDetail), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiValidationExceptionsDetail), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ApiExceptionsDetail), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Edit(PhotoSetRequest request, CancellationToken cancellationToken)
        {
            var model = mapper.Map<PhotoSetModel>(request);
            var result = await photoSetService.EditAsync(model, cancellationToken);
            return Ok(mapper.Map<PhotoSetResponse>(result));
        }

        /// <summary>
        /// Удалить Фотосет по Id
        /// </summary>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiExceptionsDetail), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiExceptionsDetail), StatusCodes.Status417ExpectationFailed)]
        public async Task<IActionResult> Delete([Required] Guid id, CancellationToken cancellationToken)
        {
            await photoSetService.DeleteAsync(id, cancellationToken);
            return Ok();
        }
    }
}
