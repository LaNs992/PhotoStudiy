using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using PhotoStudiy.API.Models;
using PhotoStudiy.Services.Contracts.Interface;

namespace PhotoStudiy.API.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class ClientController: ControllerBase
    {
        public readonly IClientService clientService;


        public ClientController(IClientService clientService)
        {
            this.clientService = clientService;
        }
        [HttpGet]

        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await clientService.GetAllAsync(cancellationToken);
            return Ok(result.Select(x => new ClientResponse
            {
                Id = x.Id,
                LastName = x.LastName,
                Name = x.Name,
                Number = x.Number,

            }));
        }
        [HttpGet("{id:guid}")]
       
        public async Task<IActionResult> GetById(Guid id , CancellationToken cancellationToken)
        {
            var result = await clientService.GetByIdAsync(id, cancellationToken);
            if (result == null)
            {
                return NotFound($"Не удалось найти дисциплину с индетификтором {id}");

            }

            return Ok(new ClientResponse
            {
                Id = result.Id,
                LastName = result.LastName,
                Name = result.Name,
                Number = result.Number,
            });
        }
    }
}
