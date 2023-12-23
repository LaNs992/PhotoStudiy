using Microsoft.AspNetCore.Mvc;
using PhotoStudiy.API.Models;
using PhotoStudiy.API.Models.Response;
using PhotoStudiy.Services.Contracts.Interface;

namespace PhotoStudiy.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [ApiExplorerSettings(GroupName = "Dogovor")]

    public class DogovorController: ControllerBase
    {
        public readonly IDogovorService dogovorService;


        public DogovorController(IDogovorService dogovorService)
        {
            this.dogovorService = dogovorService;
        }
        [HttpGet]

        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await dogovorService.GetAllAsync(cancellationToken);
            return Ok(result.Select(x => new DogovorResponse
            {
              //Id= x.Id,
              //ClientId= x.ClientId,
              //Date= x.Date,
              //PhotographId= x.PhotographId,
              //PhotosetId= x.PhotosetId,
              //Price = x.Price,
              //ProductId=x.ProductId,
              //RecvisitId =x.RecvisitId,
              //UslugiId=x.UslugiId,
              
            }));
        }
        [HttpGet("{id:guid}")]

        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var item = await dogovorService.GetByIdAsync(id, cancellationToken);
            if (item == null)
            {
                return NotFound($"Не удалось найти дисциплину с индетификтором {id}");

            }

            return Ok(new DogovorResponse
            {
                //Id= item.Id, 
                //UslugiId= item.UslugiId,
                //RecvisitId= item.RecvisitId,
                //ProductId = item.ProductId,
                //Price = item.Price,
                //PhotosetId= item.PhotosetId,
                //PhotographId= item.PhotographId,
                //Date= item.Date,
                //ClientId = item.ClientId,
            });
        }
    }
}
