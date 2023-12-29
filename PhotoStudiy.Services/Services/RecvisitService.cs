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
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoStudiy.Services.Services
{
    public class RecvisitService : IRecvisitService, IServiceAnhor
    {
        private readonly IRecvisitReadRepository recvisitReadRepository;
        private readonly IRecvisitWriteRepository recvisitWriteRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IServiceValidatorService validatorService;

        public RecvisitService(IRecvisitReadRepository recvisitReadRepository, IMapper mapper,
            IRecvisitWriteRepository recvisitWriteRepository, IUnitOfWork unitOfWork, IServiceValidatorService validatorService)
        {
            this.recvisitReadRepository = recvisitReadRepository;
            this.mapper = mapper;
            this.recvisitWriteRepository = recvisitWriteRepository;
            this.unitOfWork = unitOfWork;
            this.validatorService = validatorService;
        }

        async Task<RecvisitModel> IRecvisitService.AddAsync(RecvisitModel model, CancellationToken cancellationToken)
        {
            model.Id = Guid.NewGuid();

            await validatorService.ValidateAsync(model, cancellationToken);

            var item = mapper.Map<Recvisit>(model);
            recvisitWriteRepository.Add(item);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return mapper.Map<RecvisitModel>(item);
        }

        async Task IRecvisitService.DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var targetRecvisit = await recvisitReadRepository.GetByIdAsync(id, cancellationToken);

            if (targetRecvisit == null)
            {
                throw new PhotoStudiyEntityNotFoundException<Recvisit>(id);
            }

           

            recvisitWriteRepository.Delete(targetRecvisit);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }

        async Task<RecvisitModel> IRecvisitService.EditAsync(RecvisitModel source, CancellationToken cancellationToken)
        {
            await validatorService.ValidateAsync(source, cancellationToken);

            var targetRecvisit = await recvisitReadRepository.GetByIdAsync(source.Id, cancellationToken);

            if (targetRecvisit == null)
            {
                throw new PhotoStudiyEntityNotFoundException<Recvisit>(source.Id);
            }

            targetRecvisit = mapper.Map<Recvisit>(source);
            recvisitWriteRepository.Update(targetRecvisit);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return mapper.Map<RecvisitModel>(targetRecvisit);
        }

        async Task<IEnumerable<RecvisitModel>> IRecvisitService.GetAllAsync(CancellationToken cancellationToken)
        {
            var result = await recvisitReadRepository.GetAllAsync(cancellationToken);
            return result.Select(x => mapper.Map<RecvisitModel>(x));
        }

        async Task<RecvisitModel?> IRecvisitService.GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var item = await recvisitReadRepository.GetByIdAsync(id, cancellationToken);

            if (item == null)
            {
                throw new PhotoStudiyEntityNotFoundException<Recvisit>(id);
            }

            return mapper.Map<RecvisitModel>(item);
        }
    }
}
