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
using PhotoStudiy.Services.Validator;

namespace PhotoStudiy.Services
{
    internal class ServicesValidatorService : IServiceValidatorService
    {
        private readonly Dictionary<Type, IValidator> validators = new Dictionary<Type, IValidator>();

        public ServicesValidatorService(IPhotographReadRepository photographReadRepository, IClientReadRepository clientReadRepository,
            IPhotoSetReadRepository photosetReadRepository, IUslugiReadRepository uslugiReadRepository,IRecvisitReadRepository recvisitReadRepository,IProductReadRepository productReadRepository)
        {
            validators.Add(typeof(PhotographModel), new PhotographModelValidator());
            validators.Add(typeof(ClientModel), new ClientModelValidator());
            validators.Add(typeof(PhotoSetModel), new PhotoSetModelValidator());
            validators.Add(typeof(UslugiModel), new UslugiModelValidator());
            validators.Add(typeof(RecvisitModel), new RecvisitModelValidator());
            validators.Add(typeof(ProductModel), new ProductModelValidator());
            validators.Add(typeof(DogovorModel), new DogovorModelValidator(photographReadRepository,
                clientReadRepository, photosetReadRepository, productReadRepository, recvisitReadRepository, uslugiReadRepository));
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
