using FluentValidation;
using PhotoStudiy.Repositories.Contracts.ReadRepositoriesContracts;
using PhotoStudiy.Services.Contracts.ModelReqest;
using PhotoStudiy.Services.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoStudiy.Services.Validator
{
    public class DogovorModelValidator : AbstractValidator<DogovorRequestModel>
    {
        private readonly IPhotographReadRepository photographReadRepository;
        private readonly IClientReadRepository clientReadRepository;
        private readonly IPhotoSetReadRepository photoSetReadRepository;
        private readonly IProductReadRepository productReadRepository;
        private readonly IRecvisitReadRepository recvisitReadRepository;
        private readonly IUslugiReadRepository uslugiReadRepository;

        public DogovorModelValidator(IPhotographReadRepository photographReadRepository, IClientReadRepository clientReadRepository,
           IPhotoSetReadRepository photoSetReadRepository, IProductReadRepository productReadRepository, IRecvisitReadRepository recvisitReadRepository, IUslugiReadRepository uslugiReadRepository)
        {
            this.photographReadRepository = photographReadRepository;
            this.clientReadRepository = clientReadRepository;
            this.productReadRepository = productReadRepository;
            this.photoSetReadRepository = photoSetReadRepository;
            this.recvisitReadRepository = recvisitReadRepository;
            this.uslugiReadRepository = uslugiReadRepository;

            RuleFor(x => x.ProductId)
                 .NotEmpty().WithMessage(MessageForValidation.DefaultMessage)
                .MustAsync(async (x, cancellationToken) => await this.productReadRepository.IsNotNullAsync(x, cancellationToken))
                .WithMessage(MessageForValidation.NotFoundGuidMessage);

            RuleFor(x => x.PhotosetId)
                .NotEmpty().WithMessage(MessageForValidation.DefaultMessage)
                .MustAsync(async (x, cancellationToken) => await this.photoSetReadRepository.IsNotNullAsync(x, cancellationToken))
                .WithMessage(MessageForValidation.NotFoundGuidMessage);

            RuleFor(x => x.PhotographId)
                .NotEmpty().WithMessage(MessageForValidation.DefaultMessage)
                .MustAsync(async (x, cancellationToken) => await this.photographReadRepository.IsNotNullAsync(x, cancellationToken))
                .WithMessage(MessageForValidation.NotFoundGuidMessage);

            RuleFor(x => x.ClientId)
                .NotEmpty().WithMessage(MessageForValidation.DefaultMessage)
                .MustAsync(async (x, cancellationToken) => await this.clientReadRepository.IsNotNullAsync(x, cancellationToken))
                .WithMessage(MessageForValidation.NotFoundGuidMessage);

            RuleFor(x => x.RecvisitId)
                .NotEmpty().WithMessage(MessageForValidation.DefaultMessage)
                .MustAsync(async (x, cancellationToken) => await this.recvisitReadRepository.IsNotNullAsync(x, cancellationToken))
                .WithMessage(MessageForValidation.NotFoundGuidMessage);

            RuleFor(x => x.UslugiId)
                .NotEmpty().WithMessage(MessageForValidation.DefaultMessage)
                .MustAsync(async (x, cancellationToken) => await this.uslugiReadRepository.IsNotNullAsync(x, cancellationToken))
                .WithMessage(MessageForValidation.NotFoundGuidMessage);


            RuleFor(x => x.Date)
                .NotEmpty().WithMessage(MessageForValidation.DefaultMessage)
                .GreaterThan(DateTimeOffset.Now.AddMinutes(1)).WithMessage(MessageForValidation.InclusiveBetweenMessage);

            RuleFor(x => x.Price)
                .InclusiveBetween(1, 100000).WithMessage(MessageForValidation.InclusiveBetweenMessage);
        }
    }
    }
