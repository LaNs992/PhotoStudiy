using FluentValidation.TestHelper;
using PhotoStudiy.Common.Entity.InterfaceDB;
using PhotoStudiy.Context.Contracts.Models;
using PhotoStudiy.Context.Tests;
using PhotoStudiy.Repositories.Contracts.ReadRepositoriesContracts;
using PhotoStudiy.Repositories.ReadRepositories;
using PhotoStudiy.Repositories.Test;
using PhotoStudiy.Services.Validator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PhotoStudiy.Services.Test.TestValidator
{
    public class DogovorModelValidatorTest : PhotoStudiyContextMemory
    {
        private readonly DogovorModelValidator validator;
        public DogovorModelValidatorTest()
        {
            validator = new DogovorModelValidator(new PhotographReadRepositories(Reader),
            new ClientReadRepositories(Reader), new PhotoSetReadRepositories(Reader), new ProductsReadRepositories(Reader), new RecvisitReadRepositories(Reader), new UslugiReadRepositories(Reader));
        }

        /// <summary>
        /// Тест на наличие ошибок
        /// </summary>
        [Fact]
        public async void ValidatorShouldError()
        {
            //Arrange
            var model = TestDataGenerator.DogovorRequestModel(x => {  x.Date = DateTimeOffset.Now; x.Price = 0;  });
            model.ClientId = Guid.NewGuid(); ;
            model.PhotographId = Guid.NewGuid(); ;
            model.PhotosetId = Guid.NewGuid(); ;
            model.ProductId = Guid.NewGuid(); ;
            model.UslugiId = Guid.NewGuid(); ;
            model.RecvisitId = Guid.NewGuid(); ;
            

            // Act
            var result = await validator.TestValidateAsync(model);

            // Assert
            result.ShouldHaveAnyValidationError();
        }

        /// <summary>
        /// Тест на отсутствие ошибок
        /// </summary>
        [Fact]
        async public void ValidatorShouldSuccess()
        {
            //Arrange
            var photograph = TestDataGenerator.Photogragh();
            var photoSet = TestDataGenerator.PhotoSet();
            var product = TestDataGenerator.Product();
            var recvisit = TestDataGenerator.Recvisit();
            var uslugi = TestDataGenerator.Uslugi();
            var client = TestDataGenerator.Client();
            await Context.Photograghs.AddAsync(photograph);
            await Context.PhotoSets.AddAsync(photoSet);
            await Context.Uslugs.AddAsync(uslugi);
            await Context.Recvisits.AddAsync(recvisit);
            await Context.Products.AddAsync(product);
            await Context.Clients.AddAsync(client);
            await UnitOfWork.SaveChangesAsync(CancellationToken);

            var model = TestDataGenerator.DogovorRequestModel();
            model.ClientId = client.Id;
            model.PhotographId = photograph.Id;
            model.PhotosetId = photoSet.Id;
            model.ProductId = product.Id;
            model.UslugiId = uslugi.Id;
            model.RecvisitId = recvisit.Id;

            // Act
            var result = await validator.TestValidateAsync(model);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
