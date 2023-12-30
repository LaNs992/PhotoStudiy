using AutoMapper;
using FluentAssertions;
using PhotoStudiy.Common.Entity.InterfaceDB;
using PhotoStudiy.Context.Contracts.Models;
using PhotoStudiy.Context.Tests;
using PhotoStudiy.Repositories.Contracts.ReadRepositoriesContracts;
using PhotoStudiy.Repositories.ReadRepositories;
using PhotoStudiy.Repositories.Test;
using PhotoStudiy.Repositories.WriteRepositories;
using PhotoStudiy.Services.AutoMappers;
using PhotoStudiy.Services.Contracts.Exceptions;
using PhotoStudiy.Services.Contracts.Interface;
using PhotoStudiy.Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PhotoStudiy.Services.Test.TestServices
{
    public class DogovorServiceTest : PhotoStudiyContextMemory
    {
        private readonly IDogovorService dogovorService;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="TicketServiceTest"/>
        /// </summary>
        public DogovorServiceTest()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ServiceMapper());
            });
            dogovorService = new DogovorService(new DogovorWriteRepository(WriterContext), new DogovorReadRepositories(Reader), new PhotographReadRepositories(Reader),
            new ClientReadRepositories(Reader), new PhotoSetReadRepositories(Reader), new ProductsReadRepositories(Reader), new RecvisitReadRepositories(Reader) ,new UslugiReadRepositories(Reader), config.CreateMapper(), UnitOfWork,
            new ServicesValidatorService(new PhotographReadRepositories(Reader),
            new ClientReadRepositories(Reader), new PhotoSetReadRepositories(Reader), new UslugiReadRepositories(Reader), new RecvisitReadRepositories(Reader), new ProductsReadRepositories(Reader)));
        }

        /// <summary>
        /// Получение <see cref="Dogovor"/> по идентификатору возвращает null
        /// </summary>
        [Fact]
        public async Task GetByIdShouldReturnNull()
        {
            //Arrange
            var id = Guid.NewGuid();

            // Act
            Func<Task> result = () => dogovorService.GetByIdAsync(id, CancellationToken);

            // Assert
            await result.Should().ThrowAsync<PhotoStudiyEntityNotFoundException<Dogovor>>()
                .WithMessage($"*{id}*");
        }

        /// <summary>
        /// Получение <see cref="Dogovor"/> по идентификатору возвращает данные
        /// </summary>
        [Fact]
        public async Task GetByIdShouldReturnValue()
        {
            //Arrange
            var target = TestDataGenerator.Dogovor();
            await Context.Dogovors.AddAsync(target);
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            var result = await dogovorService.GetByIdAsync(target.Id, CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.BeEquivalentTo(new
                {
                    target.Id,
                    target.Date,
                    target.Price
                });
        }

        /// <summary>
        /// Получение <see cref="IEnumerable{Dogovor}"/> по идентификаторам возвращает пустйю коллекцию
        /// </summary>
        [Fact]
        public async Task GetAllShouldReturnEmpty()
        {
            // Act
            var result = await dogovorService.GetAllAsync(CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.BeEmpty();
        }

        /// <summary>
        /// Получение <see cref="IEnumerable{Dogovor}"/> по идентификаторам возвращает данные
        /// </summary>
        [Fact]
        public async Task GetAllShouldReturnValues()
        {
            //Arrange
            var target = TestDataGenerator.Dogovor();
            await Context.Dogovors.AddRangeAsync(target,
                TestDataGenerator.Dogovor(x => x.DeletedAt = DateTimeOffset.UtcNow));
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            var result = await dogovorService.GetAllAsync(CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.HaveCount(0);
        }

        /// <summary>
        /// Удаление не существуюущего <see cref="Dogovor"/>
        /// </summary>
        [Fact]
        public async Task DeletingNonExistentCinemaReturnExсeption()
        {
            //Arrange
            var id = Guid.NewGuid();

            // Act
            Func<Task> result = () => dogovorService.DeleteAsync(id, CancellationToken);

            // Assert
            await result.Should().ThrowAsync<PhotoStudiyEntityNotFoundException<Dogovor>>()
                .WithMessage($"*{id}*");
        }

        /// <summary>
        /// Удаление удаленного <see cref="Dogovor"/>
        /// </summary>
        [Fact]
        public async Task DeletingDeletedCinemaReturnExсeption()
        {
            //Arrange
            var model = TestDataGenerator.Dogovor(x => x.DeletedAt = DateTime.UtcNow);
            await Context.Dogovors.AddAsync(model);
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            Func<Task> result = () => dogovorService.DeleteAsync(model.Id, CancellationToken);

            // Assert
            await result.Should().ThrowAsync<PhotoStudiyEntityNotFoundException<Dogovor>>()
                .WithMessage($"*{model.Id}*");
        }

        /// <summary>
        /// Удаление <see cref="Dogovor"/>
        /// </summary>
        [Fact]
        public async Task DeleteShouldWork()
        {
            //Arrange
            var model = TestDataGenerator.Dogovor();
            await Context.Dogovors.AddAsync(model);
            await UnitOfWork.SaveChangesAsync(CancellationToken);

            //Act
            Func<Task> act = () => dogovorService.DeleteAsync(model.Id, CancellationToken);

            // Assert
            await act.Should().NotThrowAsync();
            var entity = Context.Dogovors.Single(x => x.Id == model.Id);
            entity.Should().NotBeNull();
            entity.DeletedAt.Should().NotBeNull();
        }

        /// <summary>
        /// Добавление <see cref="Dogovor"/>
        /// </summary>
        [Fact]
        public async Task AddShouldWork()
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

            //Act
            Func<Task> act = () => dogovorService.AddAsync(model, CancellationToken);

            // Assert
            await act.Should().NotThrowAsync();
            var entity = Context.Dogovors.Single(x => x.Id == model.Id);
            entity.Should().NotBeNull();
            entity.DeletedAt.Should().BeNull();
        }

        /// <summary>
        /// Добавление не валидируемого <see cref="Dogovor"/>
        /// </summary>
        [Fact]
        public async Task AddShouldValidationException()
        {
            //Arrange
            var model = TestDataGenerator.DogovorRequestModel();

            //Act
            Func<Task> act = () => dogovorService.AddAsync(model, CancellationToken);

            // Assert
            await act.Should().ThrowAsync<PhotoStudiyValidationException>();
        }

        /// <summary>
        /// Изменение несуществующего <see cref="Dogovor"/>
        /// </summary>
        [Fact]
        public async Task EditShouldNotFoundException()
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

            //Act
            Func<Task> act = () => dogovorService.EditAsync(model, CancellationToken);

            // Assert
            await act.Should().ThrowAsync<PhotoStudiyEntityNotFoundException<Dogovor>>()
                .WithMessage($"*{model.Id}*");
        }

        /// <summary>
        /// Изменение невалидируемого <see cref="Dogovor"/>
        /// </summary>
        [Fact]
        public async Task EditShouldValidationException()
        {
            //Arrange
            var model = TestDataGenerator.DogovorRequestModel();

            //Act
            Func<Task> act = () => dogovorService.EditAsync(model, CancellationToken);

            // Assert
            await act.Should().ThrowAsync<PhotoStudiyValidationException>();
        }

        /// <summary>
        /// Изменение <see cref="Dogovor"/>
        /// </summary>
        [Fact]
        public async Task EditShouldWork()
        {
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

            var dogovor = TestDataGenerator.Dogovor();
            dogovor.ClientId = client.Id;
            dogovor.PhotographId = photograph.Id;
            dogovor.PhotosetId = photoSet.Id;
            dogovor.ProductId = product.Id;
            dogovor.UslugiId = uslugi.Id;
            dogovor.RecvisitId = recvisit.Id;

            var model = TestDataGenerator.DogovorRequestModel();
            model.Id = dogovor.Id;
            model.ClientId = client.Id;
            model.PhotographId = photograph.Id;
            model.PhotosetId = photoSet.Id;
            model.ProductId = product.Id;
            model.UslugiId = uslugi.Id;
            model.RecvisitId = recvisit.Id;

            await Context.Dogovors.AddAsync(dogovor);
            await UnitOfWork.SaveChangesAsync(CancellationToken);

            //Act
            Func<Task> act = () => dogovorService.EditAsync(model, CancellationToken);

            // Assert
            await act.Should().NotThrowAsync();
            var entity = Context.Dogovors.Single(x => x.Id == dogovor.Id);
            entity.Should().NotBeNull()
                .And
                .BeEquivalentTo(new
                {
                    model.Id,
                    model.Price,
                    model.Date
                });
        }
    }
}
