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
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PhotoStudiy.Services.Test.TestServices
{
    public class UslugiServiceTest : PhotoStudiyContextMemory
    {

        private readonly IUslugiService uslugiService;
        private readonly UslugiReadRepositories uslugiReadRepositories;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="StaffServiceTest"/>
        /// </summary>
        public UslugiServiceTest()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ServiceMapper());
            });
            uslugiReadRepositories = new UslugiReadRepositories(Reader);
            uslugiService = new UslugiService(
            new UslugiWriteRepository(WriterContext), uslugiReadRepositories, UnitOfWork, config.CreateMapper(), 
            new ServicesValidatorService(new PhotographReadRepositories(Reader), new ClientReadRepositories(Reader),
            new PhotoSetReadRepositories(Reader), uslugiReadRepositories, new RecvisitReadRepositories(Reader), new ProductsReadRepositories(Reader)));
        }

        /// <summary>
        /// Получение <see cref="Uslugi"/> по идентификатору возвращает null
        /// </summary>
        [Fact]
        public async Task GetByIdShouldReturnNull()
        {
            //Arrange
            var id = Guid.NewGuid();

            // Act
            Func<Task> result = () => uslugiService.GetByIdAsync(id, CancellationToken);

            // Assert
            await result.Should().ThrowAsync<PhotoStudiyEntityNotFoundException<Uslugi>>()
                .WithMessage($"*{id}*");
        }

        /// <summary>
        /// Получение <see cref="Uslugi"/> по идентификатору возвращает данные
        /// </summary>
        [Fact]
        public async Task GetByIdShouldReturnValue()
        {
            //Arrange
            var target = TestDataGenerator.Uslugi();
            await Context.Uslugs.AddAsync(target);
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            var result = await uslugiService.GetByIdAsync(target.Id, CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.BeEquivalentTo(new
                {
                    target.Id,
                    target.Name,
                    target.Price,
                });
        }

        /// <summary>
        /// Получение <see cref="IEnumerable{Uslugi}"/> по идентификаторам возвращает пустую коллекцию
        /// </summary>
        [Fact]
        public async Task GetAllShouldReturnEmpty()
        {
            // Act
            var result = await uslugiService.GetAllAsync(CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.BeEmpty();
        }

        /// <summary>
        /// Получение <see cref="Uslugi"/> по идентификаторам возвращает данные
        /// </summary>
        [Fact]
        public async Task GetAllShouldReturnValues()
        {
            //Arrange
            var target = TestDataGenerator.Uslugi();

            await Context.Uslugs.AddRangeAsync(target,
                TestDataGenerator.Uslugi(x => x.DeletedAt = DateTimeOffset.UtcNow));
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            var result = await uslugiService.GetAllAsync(CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.HaveCount(1)
                .And.ContainSingle(x => x.Id == target.Id);
        }

        /// <summary>
        /// Удаление несуществуюущего <see cref="Uslugi"/>
        /// </summary>
        [Fact]
        public async Task DeletingNonExistentCinemaReturnExсeption()
        {
            //Arrange
            var id = Guid.NewGuid();

            // Act
            Func<Task> result = () => uslugiService.DeleteAsync(id, CancellationToken);

            // Assert
            await result.Should().ThrowAsync<PhotoStudiyEntityNotFoundException<Uslugi>>()
                .WithMessage($"*{id}*");
        }

        /// <summary>
        /// Удаление удаленного <see cref="Uslugi"/>
        /// </summary>
        [Fact]
        public async Task DeletingDeletedCinemaReturnExсeption()
        {
            //Arrange
            var model = TestDataGenerator.Uslugi(x => x.DeletedAt = DateTime.UtcNow);
            await Context.Uslugs.AddAsync(model);
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            Func<Task> result = () => uslugiService.DeleteAsync(model.Id, CancellationToken);

            // Assert
            await result.Should().ThrowAsync<PhotoStudiyEntityNotFoundException<Uslugi>>()
                .WithMessage($"*{model.Id}*");
        }

        /// <summary>
        /// Удаление <see cref="Uslugi"/>
        /// </summary>
        [Fact]
        public async Task DeleteShouldWork()
        {
            //Arrange
            var model = TestDataGenerator.Uslugi();
            await Context.Uslugs.AddAsync(model);
            await UnitOfWork.SaveChangesAsync(CancellationToken);

            //Act
            Func<Task> act = () => uslugiService.DeleteAsync(model.Id, CancellationToken);

            // Assert
            await act.Should().NotThrowAsync();
            var entity = Context.Uslugs.Single(x => x.Id == model.Id);
            entity.Should().NotBeNull();
            entity.DeletedAt.Should().NotBeNull();
        }

        /// <summary>
        /// Добавление <see cref="Uslugi"/>
        /// </summary>
        [Fact]
        public async Task AddShouldWork()
        {
            //Arrange
            var model = TestDataGenerator.UslugiModel();

            //Act
            Func<Task> act = () => uslugiService.AddAsync(model, CancellationToken);

            // Assert
            await act.Should().NotThrowAsync();
            var entity = Context.Uslugs.Single(x => x.Id == model.Id);
            entity.Should().NotBeNull();
            entity.DeletedAt.Should().BeNull();
        }

        /// <summary>
        /// Добавление не валидируемого <see cref="Uslugi"/>
        /// </summary>
        [Fact]
        public async Task AddShouldValidationException()
        {
            //Arrange
            var model = TestDataGenerator.UslugiModel(x => x.Name = "q");

            //Act
            Func<Task> act = () => uslugiService.AddAsync(model, CancellationToken);

            // Assert
            await act.Should().ThrowAsync<PhotoStudiyValidationException>();
        }

        /// <summary>
        /// Изменение несуществующего <see cref="Uslugi"/>
        /// </summary>
        [Fact]
        public async Task EditShouldNotFoundException()
        {
            //Arrange
            var model = TestDataGenerator.UslugiModel();

            //Act
            Func<Task> act = () => uslugiService.EditAsync(model, CancellationToken);

            // Assert
            await act.Should().ThrowAsync<PhotoStudiyEntityNotFoundException<Uslugi>>()
                .WithMessage($"*{model.Id}*");
        }

        /// <summary>
        /// Изменение невалидируемого <see cref="Uslugi"/>
        /// </summary>
        [Fact]
        public async Task EditShouldValidationException()
        {
            //Arrange
            var model = TestDataGenerator.UslugiModel(x => x.Name = "q");

            //Act
            Func<Task> act = () => uslugiService.EditAsync(model, CancellationToken);

            // Assert
            await act.Should().ThrowAsync<PhotoStudiyValidationException>();
        }

        /// <summary>
        /// Изменение <see cref="Uslugi"/>
        /// </summary>
        [Fact]
        public async Task EditShouldWork()
        {
            //Arrange
            var model = TestDataGenerator.UslugiModel();
            var staff = TestDataGenerator.Uslugi(x => x.Id = model.Id);
            await Context.Uslugs.AddAsync(staff);
            await UnitOfWork.SaveChangesAsync(CancellationToken);

            //Act
            Func<Task> act = () => uslugiService.EditAsync(model, CancellationToken);

            // Assert
            await act.Should().NotThrowAsync();
            var entity = Context.Uslugs.Single(x => x.Id == staff.Id);
            entity.Should().NotBeNull()
                .And
                .BeEquivalentTo(new
                {
                    model.Id,
                    model.Name,
                    model.Price,
                    
                });
        }
    }
}
