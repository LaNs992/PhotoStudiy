using AutoMapper;
using PhotoStudiy.Common.Entity.InterfaceDB;
using PhotoStudiy.Context.Contracts.Models;
using PhotoStudiy.Repositories.Contracts.ReadRepositoriesContracts;
using PhotoStudiy.Repositories.Contracts.WriteRepositoriesContracts;
using PhotoStudiy.Services.Anchors;
using PhotoStudiy.Services.Contracts.Exceptions;
using PhotoStudiy.Services.Contracts.Interface;
using PhotoStudiy.Services.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoStudiy.Services.Services
{
    public class ProductService : IProductService, IServiceAnhor
    {
        private readonly IProductReadRepository productReadRepositiry;
        private readonly IProductWriteRepository productWriteRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IServiceValidatorService validatorService;

        public ProductService(IProductReadRepository productReadRepositiry, IMapper mapper,
            IProductWriteRepository productWriteRepository, IUnitOfWork unitOfWork, IServiceValidatorService validatorService)
        {
            this.productReadRepositiry = productReadRepositiry;
            this.mapper = mapper;
            this.productWriteRepository = productWriteRepository;
            this.unitOfWork = unitOfWork;
            this.validatorService = validatorService;
        }

        async Task<ProductModel> IProductService.AddAsync(ProductModel model, CancellationToken cancellationToken)
        {
            model.Id = Guid.NewGuid();

            await validatorService.ValidateAsync(model, cancellationToken);

            var item = mapper.Map<Product>(model);
            productWriteRepository.Add(item);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return mapper.Map<ProductModel>(item);
        }

        async Task IProductService.DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var targetProduct = await productReadRepositiry.GetByIdAsync(id, cancellationToken);

            if (targetProduct == null)
            {
                throw new PhotoStudiyEntityNotFoundException<Product>(id);
            }

           

            productWriteRepository.Delete(targetProduct);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }

        async Task<ProductModel> IProductService.EditAsync(ProductModel source, CancellationToken cancellationToken)
        {
            await validatorService.ValidateAsync(source, cancellationToken);

            var targetCinema = await productReadRepositiry.GetByIdAsync(source.Id, cancellationToken);

            if (targetCinema == null)
            {
                throw new PhotoStudiyEntityNotFoundException<Product>(source.Id);
            }

            targetCinema = mapper.Map<Product>(source);
            productWriteRepository.Update(targetCinema);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return mapper.Map<ProductModel>(targetCinema);
        }

        async Task<IEnumerable<ProductModel>> IProductService.GetAllAsync(CancellationToken cancellationToken)
        {
            var result = await productReadRepositiry.GetAllAsync(cancellationToken);
            return result.Select(x => mapper.Map<ProductModel>(x));
        }

        async Task<ProductModel?> IProductService.GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var item = await productReadRepositiry.GetByIdAsync(id, cancellationToken);

            if (item == null)
            {
                throw new PhotoStudiyEntityNotFoundException<Product>(id);
            }

            return mapper.Map<ProductModel>(item);
        }
    }
}
