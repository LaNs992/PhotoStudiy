using AutoMapper;
using PhotoStudiy.Common.Entity.InterfaceDB;
using PhotoStudiy.Context.Contracts.Models;
using PhotoStudiy.Repositories.Contracts.ReadRepositoriesContracts;
using PhotoStudiy.Repositories.Contracts.WriteRepositoriesContracts;
using PhotoStudiy.Services.Anchors;
using PhotoStudiy.Services.Contracts.Exceptions;
using PhotoStudiy.Services.Contracts.Interface;
using PhotoStudiy.Services.Contracts.ModelReqest;
using PhotoStudiy.Services.Contracts.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace PhotoStudiy.Services.Services
{
    internal class DogovorService : IDogovorService, IServiceAnhor
    {
        private readonly IDogovorWriteRepository dogovorWriteRepository;
        private readonly IDogovorReadRepository dogovorReadRepository;
        private readonly IPhotographReadRepository photographReadRepository;
        private readonly IClientReadRepository clientReadRepository;
        private readonly IPhotoSetReadRepository photoSetReadRepository;
        private readonly IProductReadRepository productReadRepository;
        private readonly IRecvisitReadRepository recvisitReadRepository;
        private readonly IUslugiReadRepository uslugiReadRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IServiceValidatorService validatorService;

        public DogovorService(IDogovorWriteRepository dogovorWriteRepository, IDogovorReadRepository dogovorReadRepository, IPhotographReadRepository photographReadRepository,
            IClientReadRepository clientReadRepository, IPhotoSetReadRepository photoSetReadRepository,
           IProductReadRepository productReadRepository, IRecvisitReadRepository recvisitReadRepository, IUslugiReadRepository uslugiReadRepository,
            IMapper mapper, IUnitOfWork unitOfWork, IServiceValidatorService validatorService)
        {
            this.dogovorWriteRepository = dogovorWriteRepository;
            this.dogovorReadRepository = dogovorReadRepository;
            this.clientReadRepository = clientReadRepository;
            this.photographReadRepository = photographReadRepository;
            this.photoSetReadRepository = photoSetReadRepository;
            this.productReadRepository = productReadRepository;
            this.recvisitReadRepository = recvisitReadRepository;
            this.uslugiReadRepository = uslugiReadRepository;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this.validatorService = validatorService;
        }

        async Task<DogovorModel> IDogovorService.AddAsync(DogovorRequestModel model, CancellationToken cancellationToken)
        {
            model.Id = Guid.NewGuid();
            await validatorService.ValidateAsync(model, cancellationToken);

            var dogovor = mapper.Map<Dogovor>(model);
            dogovorWriteRepository.Add(dogovor);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return await GetTicketModelOnMapping(dogovor, cancellationToken);
        }

        async Task IDogovorService.DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var targetTicket = await dogovorReadRepository.GetByIdAsync(id, cancellationToken);

            if (targetTicket == null)
            {
                throw new PhotoStudiyEntityNotFoundException<Dogovor>(id);
            }

            if (targetTicket.DeletedAt.HasValue)
            {
                throw new PhotoStudiyInvalidOperationException($"Билет с идентификатором {id} уже удален");
            }

            dogovorWriteRepository.Delete(targetTicket);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }

        async Task<DogovorModel> IDogovorService.EditAsync(DogovorRequestModel model, CancellationToken cancellationToken)
        {
            await validatorService.ValidateAsync(model, cancellationToken);

            var dogovor = await dogovorReadRepository.GetByIdAsync(model.Id, cancellationToken);

            if (dogovor == null)
            {
                throw new PhotoStudiyEntityNotFoundException<Dogovor>(model.Id);
            }

            dogovor = mapper.Map<Dogovor>(model);
            dogovorWriteRepository.Update(dogovor);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return await GetTicketModelOnMapping(dogovor, cancellationToken);
        }

        async Task<IEnumerable<DogovorModel>> IDogovorService.GetAllAsync(CancellationToken cancellationToken)
        {
            var dogovors = await dogovorReadRepository.GetAllAsync(cancellationToken);
            var photographs = await photographReadRepository
                .GetByIdsAsync(dogovors.Select(x => x.PhotographId).Distinct(), cancellationToken);

            var clients = await clientReadRepository
                .GetByIdsAsync(dogovors.Select(x => x.ClientId).Distinct(), cancellationToken);

            var phtosets = await photoSetReadRepository
                .GetByIdsAsync(dogovors.Select(x => x.PhotosetId).Distinct(), cancellationToken);

            var products = await productReadRepository
                .GetByIdsAsync(dogovors.Select(x => x.ProductId).Distinct(), cancellationToken);

            var recvisits = await recvisitReadRepository
                .GetByIdsAsync(dogovors.Select(x => x.RecvisitId).Distinct(), cancellationToken);

            var uslugs = await uslugiReadRepository
                .GetByIdsAsync(dogovors.Select(x => x.UslugiId).Distinct(), cancellationToken);


            var result = new List<DogovorModel>();

            foreach (var dogovor in dogovors)
            {
                if (!photographs.TryGetValue(dogovor.PhotographId, out var photogragh) ||
                !clients.TryGetValue(dogovor.ClientId, out var client) ||
                !phtosets.TryGetValue(dogovor.PhotosetId, out var photoSet) ||
                !products.TryGetValue(dogovor.ProductId, out var product) ||
                !recvisits.TryGetValue(dogovor.RecvisitId, out var recvisit) ||
                !uslugs.TryGetValue(dogovor.UslugiId, out var uslugi))

                {
                    continue;
                }
                else
                {
                    var DogovorModel = mapper.Map<DogovorModel>(dogovor);

                    DogovorModel.Photograph = mapper.Map<PhotographModel>(photogragh);
                    DogovorModel.Photoset = mapper.Map<PhotoSetModel>(photoSet);
                    DogovorModel.Product = mapper.Map<ProductModel>(product);
                    DogovorModel.Recvisit = mapper.Map<RecvisitModel>(recvisit);
                    DogovorModel.Uslugi = mapper.Map<UslugiModel>(uslugi);
                    DogovorModel.Client = mapper.Map<ClientModel>(client);

                    result.Add(DogovorModel);
                }
            }
            return result;
        }

        async Task<DogovorModel?> IDogovorService.GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var item = await dogovorReadRepository.GetByIdAsync(id, cancellationToken);

            if (item == null)
            {
                throw new PhotoStudiyEntityNotFoundException<Dogovor>(id);
            }

            return await GetTicketModelOnMapping(item, cancellationToken);
        }

        async private Task<DogovorModel> GetTicketModelOnMapping(Dogovor dogovor, CancellationToken cancellationToken)
        {
            var DogovorModel = mapper.Map<DogovorModel>(dogovor);
            DogovorModel.Photograph = mapper.Map<PhotographModel>(await photographReadRepository.GetByIdAsync(dogovor.PhotographId, cancellationToken));
            DogovorModel.Photoset = mapper.Map<PhotoSetModel>(await photoSetReadRepository.GetByIdAsync(dogovor.PhotosetId, cancellationToken));
            DogovorModel.Product = mapper.Map<ProductModel>(await productReadRepository.GetByIdAsync(dogovor.ProductId, cancellationToken));
            DogovorModel.Recvisit = mapper.Map<RecvisitModel>(await recvisitReadRepository.GetByIdAsync(dogovor.RecvisitId, cancellationToken));
            DogovorModel.Uslugi = mapper.Map<UslugiModel>(await uslugiReadRepository.GetByIdAsync(dogovor.UslugiId, cancellationToken));
            DogovorModel.Client = mapper.Map<ClientModel>(await clientReadRepository.GetByIdAsync(dogovor.ClientId, cancellationToken));

            return DogovorModel;
        }
    }
}
