using Microsoft.AspNetCore.Mvc;
using PhotoStudiy.API.Models;
using PhotoStudiy.Services.Contracts.Interface;

namespace PhotoStudiy.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PhotoSetController: ControllerBase
    {
        public readonly IPhotoSetService photoSetService;


        public PhotoSetController(IPhotoSetService photoSetService)
        {
            this.photoSetService = photoSetService;
        }
        [HttpGet]

        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await photoSetService.GetAllAsync(cancellationToken);
            return Ok(result.Select(x => new PhotoSetResponse
            {
                Id = x.Id,
                Name= x.Name,
                Description= x.Description,
                Price = x.Price,
                

            }));
        }
        [HttpGet("{id:guid}")]

        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var item = await photoSetService.GetByIdAsync(id, cancellationToken);
            if (item == null)
            {
                return NotFound($"Не удалось найти фотографа с индетификтором {id}");

            }

            return Ok(new PhotoSetResponse
            {
                Id = item.Id,
              Price= item.Price,
              Description= item.Description,
              Name= item.Name,
            });
        }
    }
}
