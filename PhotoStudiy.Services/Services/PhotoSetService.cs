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
    public class PhotoSetService : IPhotoSetService, IServiceAnhor
    {
        private readonly IPhotoSetReadRepository photoSetReadRRepository;
        private readonly IPhotoSetWriteRepository photoSetWriteRRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IServiceValidatorService validatorService;

        public PhotoSetService(IPhotoSetReadRepository photoSetReadRRepository, IMapper mapper,
            IPhotoSetWriteRepository photoSetWriteRRepository, IUnitOfWork unitOfWork, IServiceValidatorService validatorService)
        {
            this.photoSetReadRRepository = photoSetReadRRepository;
            this.mapper = mapper;
            this.photoSetWriteRRepository = photoSetWriteRRepository;
            this.unitOfWork = unitOfWork;
            this.validatorService = validatorService;
        }

        async Task<PhotoSetModel> IPhotoSetService.AddAsync(PhotoSetModel model, CancellationToken cancellationToken)
        {
            model.Id = Guid.NewGuid();

            await validatorService.ValidateAsync(model, cancellationToken);

            var item = mapper.Map<PhotoSet>(model);
            photoSetWriteRRepository.Add(item);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return mapper.Map<PhotoSetModel>(item);
        }

        async Task IPhotoSetService.DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var targetPhotoSet = await photoSetReadRRepository.GetByIdAsync(id, cancellationToken);

            if (targetPhotoSet == null)
            {
                throw new TimeTableEntityNotFoundException<PhotoSet>(id);
            }

            if (targetPhotoSet.DeletedAt.HasValue)
            {
                throw new TimeTableInvalidOperationException($"Фотосет с идентификатором {id} уже удален");
            }

            photoSetWriteRRepository.Delete(targetPhotoSet);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }

        async Task<PhotoSetModel> IPhotoSetService.EditAsync(PhotoSetModel source, CancellationToken cancellationToken)
        {
            await validatorService.ValidateAsync(source, cancellationToken);

            var targetPhotoSet = await photoSetReadRRepository.GetByIdAsync(source.Id, cancellationToken);

            if (targetPhotoSet == null)
            {
                throw new TimeTableEntityNotFoundException<PhotoSet>(source.Id);
            }

            targetPhotoSet = mapper.Map<PhotoSet>(source);
            photoSetWriteRRepository.Update(targetPhotoSet);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return mapper.Map<PhotoSetModel>(targetPhotoSet);
        }

        async Task<IEnumerable<PhotoSetModel>> IPhotoSetService.GetAllAsync(CancellationToken cancellationToken)
        {
            var result = await photoSetReadRRepository.GetAllAsync(cancellationToken);
            return result.Select(x => mapper.Map<PhotoSetModel>(x));
        }

        async Task<PhotoSetModel?> IPhotoSetService.GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var item = await photoSetReadRRepository.GetByIdAsync(id, cancellationToken);

            if (item == null)
            {
                throw new TimeTableEntityNotFoundException<PhotoSet>(id);
            }

            return mapper.Map<PhotoSetModel>(item);
        }
    }
}
