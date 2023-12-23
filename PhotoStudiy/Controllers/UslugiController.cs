using Microsoft.AspNetCore.Mvc;
using PhotoStudiy.API.Models;
using PhotoStudiy.API.Models.Response;
using PhotoStudiy.Services.Contracts.Interface;

namespace PhotoStudiy.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [ApiExplorerSettings(GroupName = "Uslugi")]

    public class UslugiController:ControllerBase
    {
        public readonly IUslugiService uslugiService;


        public UslugiController(IUslugiService uslugiService)
        {
            this.uslugiService = uslugiService;
        }
        [HttpGet]

        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await uslugiService.GetAllAsync(cancellationToken);
            return Ok(result.Select(x => new UslugiResponse
            {
                Id = x.Id,
               Name= x.Name,
              

            }));
        }
        [HttpGet("{id:guid}")]

        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var item = await uslugiService.GetByIdAsync(id, cancellationToken);
            if (item == null)
            {
                return NotFound($"Не удалось найти реквизита с индетификтором {id}");

            }

            return Ok(new UslugiResponse
            {
                Id = item.Id,
               Name= item.Name,
            });
        }
    }
}
