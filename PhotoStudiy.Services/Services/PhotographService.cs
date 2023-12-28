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
    public class PhotographService : IPhotographService, IServiceAnhor
    {
        private readonly IPhotographWriteRepository photographWriteRepository;
        private readonly IPhotographReadRepository photographReadRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IServiceValidatorService validatorService;

        public PhotographService(IPhotographWriteRepository photographWriteRepository, IPhotographReadRepository photographReadRepository,
            IUnitOfWork unitOfWork, IMapper mapper, IServiceValidatorService validatorService)
        {
            this.photographReadRepository = photographReadRepository;
            this.photographWriteRepository = photographWriteRepository;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this.validatorService = validatorService;
        }

        async Task<PhotographModel> IPhotographService.AddAsync(PhotographModel model, CancellationToken cancellationToken)
        {
            model.Id = Guid.NewGuid();
            await validatorService.ValidateAsync(model, cancellationToken);

            var item = mapper.Map<Photogragh>(model);

            photographWriteRepository.Add(item);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return mapper.Map<PhotographModel>(item);
        }

        async Task IPhotographService.DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var targetPhotogragh = await photographReadRepository.GetByIdAsync(id, cancellationToken);
            if (targetPhotogragh == null)
            {
                throw new PhotoStudiyEntityNotFoundException<Photogragh>(id);
            }

            if (targetPhotogragh.DeletedAt.HasValue)
            {
                throw new PhotoStudiyInvalidOperationException($"Фотограф с идентификатором {id} уже удален");
            }

            photographWriteRepository.Delete(targetPhotogragh);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }

        async Task<PhotographModel> IPhotographService.EditAsync(PhotographModel source, CancellationToken cancellationToken)
        {
            await validatorService.ValidateAsync(source, cancellationToken);

            var targetPhotogragh = await photographReadRepository.GetByIdAsync(source.Id, cancellationToken);
            if (targetPhotogragh == null)
            {
                throw new PhotoStudiyEntityNotFoundException<Photogragh>(source.Id);
            }

            targetPhotogragh = mapper.Map<Photogragh>(source);

            photographWriteRepository.Update(targetPhotogragh);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return mapper.Map<PhotographModel>(targetPhotogragh);
        }

        async Task<IEnumerable<PhotographModel>> IPhotographService.GetAllAsync(CancellationToken cancellationToken)
        {
            var result = await photographReadRepository.GetAllAsync(cancellationToken);
            return result.Select(x => mapper.Map<PhotographModel>(x));
        }

        async Task<PhotographModel?> IPhotographService.GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var item = await photographReadRepository.GetByIdAsync(id, cancellationToken);

            if (item == null)
            {
                throw new PhotoStudiyEntityNotFoundException<Photogragh>(id);
            }

            return mapper.Map<PhotographModel>(item);
        }
    }
}
