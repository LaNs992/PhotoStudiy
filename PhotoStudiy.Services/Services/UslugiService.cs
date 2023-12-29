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
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoStudiy.Services.Services
{
    public class UslugiService : IUslugiService, IServiceAnhor
    {
        private readonly IUslugiWriteRepository uslugiWriteRepository;
        private readonly IUslugiReadRepository uslugiReadRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IServiceValidatorService validatorService;

        public UslugiService(IUslugiWriteRepository uslugiWriteRepository, IUslugiReadRepository uslugiReadRepository,
            IUnitOfWork unitOfWork, IMapper mapper, IServiceValidatorService validatorService)
        {
            this.uslugiWriteRepository = uslugiWriteRepository;
            this.uslugiReadRepository = uslugiReadRepository;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.validatorService = validatorService;
        }

        async Task<UslugiModel> IUslugiService.AddAsync(UslugiModel model, CancellationToken cancellationToken)
        {
            model.Id = Guid.NewGuid();
            await validatorService.ValidateAsync(model, cancellationToken);

            var item = mapper.Map<Uslugi>(model);

            uslugiWriteRepository.Add(item);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return mapper.Map<UslugiModel>(item);
        }

        async Task IUslugiService.DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var targetUslugi = await uslugiReadRepository.GetByIdAsync(id, cancellationToken);

            if (targetUslugi == null)
            {
                throw new PhotoStudiyEntityNotFoundException<Uslugi>(id);
            }

            if (targetUslugi.DeletedAt.HasValue)
            {
                throw new PhotoStudiyInvalidOperationException($"Услуга с идентификатором {id} уже удален");
            }

            uslugiWriteRepository.Delete(targetUslugi);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }

        async Task<UslugiModel> IUslugiService.EditAsync(UslugiModel source, CancellationToken cancellationToken)
        {
            await validatorService.ValidateAsync(source, cancellationToken);

            var targetUslugi = await uslugiReadRepository.GetByIdAsync(source.Id, cancellationToken);

            if (targetUslugi == null)
            {
                throw new PhotoStudiyEntityNotFoundException<Uslugi>(source.Id);
            }

            targetUslugi = mapper.Map<Uslugi>(source);

            uslugiWriteRepository.Update(targetUslugi);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return mapper.Map<UslugiModel>(targetUslugi);
        }

        async Task<IEnumerable<UslugiModel>> IUslugiService.GetAllAsync(CancellationToken cancellationToken)
        {
            var result = await uslugiReadRepository.GetAllAsync(cancellationToken);
            return result.Select(x => mapper.Map<UslugiModel>(x));
        }

        async Task<UslugiModel?> IUslugiService.GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var item = await uslugiReadRepository.GetByIdAsync(id, cancellationToken);

            if (item == null)
            {
                throw new PhotoStudiyEntityNotFoundException<Uslugi>(id);
            }

            return mapper.Map<UslugiModel>(item);
        }
    }
}
