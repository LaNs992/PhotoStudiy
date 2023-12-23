using Microsoft.AspNetCore.Mvc;
using PhotoStudiy.API.Models;
using PhotoStudiy.API.Models.Response;
using PhotoStudiy.Services.Contracts.Interface;

namespace PhotoStudiy.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [ApiExplorerSettings(GroupName = "Photograph")]

    public class PhotographController: ControllerBase
    {
        public readonly IPhotographService photographService;


        public PhotographController(IPhotographService photographService)
        {
            this.photographService = photographService;
        }
        [HttpGet]

        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await photographService.GetAllAsync(cancellationToken);
            return Ok(result.Select(x => new PhotographResponse
            {
                Id = x.Id,
                LastName= x.LastName,
                Name= x.Name,
                Number = x.Number,

            }));
        }
        [HttpGet("{id:guid}")]

        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var item = await photographService.GetByIdAsync(id, cancellationToken);
            if (item == null)
            {
                return NotFound($"Не удалось найти фотографа с индетификтором {id}");

            }

            return Ok(new PhotographResponse
            {
                Id = item.Id,
                Number= item.Number,
                LastName= item.LastName,
                Name= item.Name,
            });
        }
    }
}
