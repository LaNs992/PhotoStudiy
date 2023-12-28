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
    public class ClientService : IClientService, IServiceAnhor
    {
        private readonly IClientWriteRepository clientWriteRepository;
        private readonly IClientReadRepository clientReadRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IServiceValidatorService validatorService;

        public ClientService(IClientWriteRepository clientWriteRepository, IClientReadRepository clientReadRepository,
            IUnitOfWork unitOfWork, IMapper mapper, IServiceValidatorService validatorService)
        {
            this.clientReadRepository = clientReadRepository;
            this.clientWriteRepository = clientWriteRepository;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this.validatorService = validatorService;
        }

        async Task<ClientModel> IClientService.AddAsync(ClientModel model, CancellationToken cancellationToken)
        {
            model.Id = Guid.NewGuid();
            await validatorService.ValidateAsync(model, cancellationToken);

            var item = mapper.Map<Client>(model);

            clientWriteRepository.Add(item);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return mapper.Map<ClientModel>(item);
        }

        async Task IClientService.DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var targetClient = await clientReadRepository.GetByIdAsync(id, cancellationToken);
            if (targetClient == null)
            {
                throw new PhotoStudiyEntityNotFoundException<Client>(id);
            }

            if (targetClient.DeletedAt.HasValue)
            {
                throw new PhotoStudiyInvalidOperationException($"Клиент с идентификатором {id} уже удален");
            }

            clientWriteRepository.Delete(targetClient);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }

        async Task<ClientModel> IClientService.EditAsync(ClientModel source, CancellationToken cancellationToken)
        {
            await validatorService.ValidateAsync(source, cancellationToken);

            var targetClient = await clientReadRepository.GetByIdAsync(source.Id, cancellationToken);
            if (targetClient == null)
            {
                throw new PhotoStudiyEntityNotFoundException<Client>(source.Id);
            }

            targetClient = mapper.Map<Client>(source);

            clientWriteRepository.Update(targetClient);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return mapper.Map<ClientModel>(targetClient);
        }

        async Task<IEnumerable<ClientModel>> IClientService.GetAllAsync(CancellationToken cancellationToken)
        {
            var result = await clientReadRepository.GetAllAsync(cancellationToken);
            return result.Select(x => mapper.Map<ClientModel>(x));
        }

        async Task<ClientModel?> IClientService.GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var item = await clientReadRepository.GetByIdAsync(id, cancellationToken);

            if (item == null)
            {
                throw new PhotoStudiyEntityNotFoundException<Client>(id);
            }

            return mapper.Map<ClientModel>(item);
        }
    }
}
