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

namespace PhotoStudiy.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [ApiExplorerSettings(GroupName = "Photograph")]

    public class PhotographController : ControllerBase
    {
        private readonly IPhotographService photographService;
        private readonly IMapper mapper;

        public PhotographController(IPhotographService photographService, IMapper mapper)
        {
            this.photographService = photographService;
            this.mapper = mapper;
        }

        /// <summary>
        /// Получить список Фотографов
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(ICollection<PhotographResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await photographService.GetAllAsync(cancellationToken);
            return Ok(result.Select(x => mapper.Map<PhotographResponse>(x)));
        }

        /// <summary>
        /// Получить Фотографа по Id
        /// </summary>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(PhotographResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiExceptionsDetail), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById([Required] Guid id, CancellationToken cancellationToken)
        {
            var item = await photographService.GetByIdAsync(id, cancellationToken);
            return Ok(mapper.Map<PhotographResponse>(item));
        }

        /// <summary>
        /// Добавить Фотографа
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(PhotographResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiValidationExceptionsDetail), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ApiExceptionsDetail), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Add(CreatePhotographRequest model, CancellationToken cancellationToken)
        {
            var photographModel = mapper.Map<PhotographModel>(model);
            var result = await photographService.AddAsync(photographModel, cancellationToken);
            return Ok(mapper.Map<PhotographResponse>(result));
        }

        /// <summary>
        /// Изменить Фотографа по Id
        /// </summary>
        [HttpPut]
        [ProducesResponseType(typeof(PhotographResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiExceptionsDetail), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiValidationExceptionsDetail), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ApiExceptionsDetail), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Edit(PhotographRequest request, CancellationToken cancellationToken)
        {
            var model = mapper.Map<PhotographModel>(request);
            var result = await photographService.EditAsync(model, cancellationToken);
            return Ok(mapper.Map<PhotographResponse>(result));
        }

        /// <summary>
        /// Удалить клиента по Id
        /// </summary>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiExceptionsDetail), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiExceptionsDetail), StatusCodes.Status417ExpectationFailed)]
        public async Task<IActionResult> Delete([Required] Guid id, CancellationToken cancellationToken)
        {
            await photographService.DeleteAsync(id, cancellationToken);
            return Ok();
        }
    }
   }
