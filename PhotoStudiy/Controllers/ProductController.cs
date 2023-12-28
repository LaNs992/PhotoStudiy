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
    [ApiExplorerSettings(GroupName = "Product")]

    public class ProductController : ControllerBase
    {
        private readonly IProductService productService;
        private readonly IMapper mapper;

        public ProductController(IProductService productService, IMapper mapper)
        {
            this.productService = productService;
            this.mapper = mapper;
        }

        /// <summary>
        /// Получить список Продуктов
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(ICollection<ProductResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await productService.GetAllAsync(cancellationToken);
            return Ok(result.Select(x => mapper.Map<ProductResponse>(x)));
        }

        /// <summary>
        /// Получить Продукт по Id
        /// </summary>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(ProductResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiExceptionsDetail), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiExceptionsDetail), StatusCodes.Status417ExpectationFailed)]
        public async Task<IActionResult> GetById([Required] Guid id, CancellationToken cancellationToken)
        {
            var item = await productService.GetByIdAsync(id, cancellationToken);
            return Ok(mapper.Map<ProductResponse>(item));
        }

        /// <summary>
        /// Добавить Продукт
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(ProductResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiValidationExceptionsDetail), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ApiExceptionsDetail), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Add(CreateProductRequest model, CancellationToken cancellationToken)
        {
            var filmModel = mapper.Map<ProductModel>(model);
            var result = await productService.AddAsync(filmModel, cancellationToken);
            return Ok(mapper.Map<ProductResponse>(result));
        }

        /// <summary>
        /// Изменить Продукт по Id
        /// </summary>
        [HttpPut]
        [ProducesResponseType(typeof(ProductResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiExceptionsDetail), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiValidationExceptionsDetail), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ApiExceptionsDetail), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Edit(ProductRequest request, CancellationToken cancellationToken)
        {
            var model = mapper.Map<ProductModel>(request);
            var result = await productService.EditAsync(model, cancellationToken);
            return Ok(mapper.Map<ProductResponse>(result));
        }

        /// <summary>
        /// Удалить Продукт по Id
        /// </summary>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiExceptionsDetail), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiExceptionsDetail), StatusCodes.Status417ExpectationFailed)]
        public async Task<IActionResult> Delete([Required] Guid id, CancellationToken cancellationToken)
        {
            await productService.DeleteAsync(id, cancellationToken);
            return Ok();
        }
    }
}
