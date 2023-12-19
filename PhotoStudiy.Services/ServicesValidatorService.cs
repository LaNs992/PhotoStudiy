using PhotoStudiy.General;
using PhotoStudiy.Repositories.Contracts.ReadRepositoriesContracts;
using PhotoStudiy.Services.Contracts.Exceptions;
using PhotoStudiy.Services.Contracts.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace PhotoStudiy.Services
{
    internal class ServicesValidatorService : IServiceValidatorService
    {
        private readonly Dictionary<Type, IValidator> validators = new Dictionary<Type, IValidator>();

        public ServicesValidatorService(IPhotographReadRepository photographReadRepository, IClientReadRepository clientReadRepository,
            IPhotoSetReadRepository photosetReadRepository, IUslugiReadRepository uslugiReadRepository,IRecvisitReadRepository recvisitReadRepository,IProductReadRepository productReadRepository)
        {
            validators.Add(typeof(PhotographModel), new CinemaModelValidator());
            validators.Add(typeof(ClientModel), new ClientModelValidator());
            validators.Add(typeof(PhotoSetModel), new FilmModelValidator());
            validators.Add(typeof(UslugiModel), new HallModelValidator());
            validators.Add(typeof(RecvisitModel), new StaffModelValidator());
            validators.Add(typeof(ProductModel), new StaffModelValidator());
            validators.Add(typeof(DogovorModel), new TicketRequestValidator(photographReadRepository,
                clientReadRepository, photosetReadRepository, uslugiReadRepository,recvisitReadRepository,productReadRepository));
        }

        public async Task ValidateAsync<TModel>(TModel model, CancellationToken cancellationToken)
            where TModel : class
        {
            var modelType = model.GetType();
            if (!validators.TryGetValue(modelType, out var validator))
            {
                throw new InvalidOperationException($"Не найден валидатор для модели {modelType}");
            }

            var context = new ValidationContext<TModel>(model);
            var result = await validator.ValidateAsync(context, cancellationToken);

            if (!result.IsValid)
            {
                throw new TimeTableValidationException(result.Errors.Select(x =>
                InvalidateItemModel.New(x.PropertyName, x.ErrorMessage)));
            }
        }
    }
}
