using Microsoft.AspNetCore.Mvc;
using PhotoStudiy.API.Models;
using PhotoStudiy.Services.Contracts.Interface;

namespace PhotoStudiy.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController:ControllerBase
    {
        public readonly IProductService productService;


        public ProductController(IProductService productService)
        {
            this.productService = productService;
        }
        [HttpGet]

        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await productService.GetAllAsync(cancellationToken);
            return Ok(result.Select(x => new ProductResponse
            {
                Id = x.Id,
                Name= x.Name,
                Price= x.Price,
                Amount = x.Amount,


            }));
        }
        [HttpGet("{id:guid}")]

        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var item = await productService.GetByIdAsync(id, cancellationToken);
            if (item == null)
            {
                return NotFound($"Не удалось найти продукта с индетификтором {id}");

            }

            return Ok(new ProductResponse
            {
                Id = item.Id,
                Amount= item.Amount,
                Price= item.Price,
                Name= item.Name,
            });
        }
    }
}
