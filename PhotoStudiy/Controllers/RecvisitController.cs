using Microsoft.AspNetCore.Mvc;
using PhotoStudiy.API.Models;
using PhotoStudiy.API.Models.Response;
using PhotoStudiy.Services.Contracts.Interface;

namespace PhotoStudiy.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [ApiExplorerSettings(GroupName = "Recvisit")]

    public class RecvisitController:ControllerBase
    {
        public readonly IRecvisitService recvisitService;


        public RecvisitController(IRecvisitService recvisitService)
        {
            this.recvisitService = recvisitService;
        }
        [HttpGet]

        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await recvisitService.GetAllAsync(cancellationToken);
            return Ok(result.Select(x => new RecvisitResponse
            {
                Id = x.Id,
               Name= x.Name,
               Amount= x.Amount,
               Description = x.Description,


            }));
        }
        [HttpGet("{id:guid}")]

        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var item = await recvisitService.GetByIdAsync(id, cancellationToken);
            if (item == null)
            {
                return NotFound($"Не удалось найти реквизита с индетификтором {id}");

            }

            return Ok(new RecvisitResponse
            {
                Id = item.Id,
               Description= item.Description,
              Amount= item.Amount,
              Name= item.Name,
            });
        }
    }
}
