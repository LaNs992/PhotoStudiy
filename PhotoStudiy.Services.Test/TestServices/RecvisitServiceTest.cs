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
    public class RecvisitServiceTest : PhotoStudiyContextMemory
    {
        private readonly IRecvisitService recvisitService;
        private readonly RecvisitReadRepositories recvisitReadRepositories;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="StaffServiceTest"/>
        /// </summary>
        public RecvisitServiceTest()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ServiceMapper());
            });
            recvisitReadRepositories = new RecvisitReadRepositories(Reader);
            recvisitService = new RecvisitService(recvisitReadRepositories, config.CreateMapper(),
            new RecvisitWriteRepository(WriterContext), UnitOfWork,
            new ServicesValidatorService(new PhotographReadRepositories(Reader), new ClientReadRepositories(Reader),
             new PhotoSetReadRepositories(Reader), new UslugiReadRepositories(Reader), recvisitReadRepositories, new ProductsReadRepositories(Reader)));
        }

        /// <summary>
        /// Получение <see cref="Recvisit"/> по идентификатору возвращает null
        /// </summary>
        [Fact]
        public async Task GetByIdShouldReturnNull()
        {
            //Arrange
            var id = Guid.NewGuid();

            // Act
            Func<Task> result = () => recvisitService.GetByIdAsync(id, CancellationToken);

            // Assert
            await result.Should().ThrowAsync<PhotoStudiyEntityNotFoundException<Recvisit>>()
                .WithMessage($"*{id}*");
        }

        /// <summary>
        /// Получение <see cref="Recvisit"/> по идентификатору возвращает данные
        /// </summary>
        [Fact]
        public async Task GetByIdShouldReturnValue()
        {
            //Arrange
            var target = TestDataGenerator.Recvisit();
            await Context.Recvisits.AddAsync(target);
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            var result = await recvisitService.GetByIdAsync(target.Id, CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.BeEquivalentTo(new
                {
                    target.Id,
                    target.Name,
                    target.Description,
                    target.Amount
                });
        }

        /// <summary>
        /// Получение <see cref="IEnumerable{Recvisit}"/> по идентификаторам возвращает пустую коллекцию
        /// </summary>
        [Fact]
        public async Task GetAllShouldReturnEmpty()
        {
            // Act
            var result = await recvisitService.GetAllAsync(CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.BeEmpty();
        }

        /// <summary>
        /// Получение <see cref="Recvisit"/> по идентификаторам возвращает данные
        /// </summary>
        [Fact]
        public async Task GetAllShouldReturnValues()
        {
            //Arrange
            var target = TestDataGenerator.Recvisit();

            await Context.Recvisits.AddRangeAsync(target,
                TestDataGenerator.Recvisit(x => x.DeletedAt = DateTimeOffset.UtcNow));
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            var result = await recvisitService.GetAllAsync(CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.HaveCount(1)
                .And.ContainSingle(x => x.Id == target.Id);
        }

        /// <summary>
        /// Удаление несуществуюущего <see cref="Recvisit"/>
        /// </summary>
        [Fact]
        public async Task DeletingNonExistentCinemaReturnExсeption()
        {
            //Arrange
            var id = Guid.NewGuid();

            // Act
            Func<Task> result = () => recvisitService.DeleteAsync(id, CancellationToken);

            // Assert
            await result.Should().ThrowAsync<PhotoStudiyEntityNotFoundException<Recvisit>>()
                .WithMessage($"*{id}*");
        }

        /// <summary>
        /// Удаление удаленного <see cref="Recvisit"/>
        /// </summary>
        [Fact]
        public async Task DeletingDeletedCinemaReturnExсeption()
        {
            //Arrange
            var model = TestDataGenerator.Recvisit(x => x.DeletedAt = DateTime.UtcNow);
            await Context.Recvisits.AddAsync(model);
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            Func<Task> result = () => recvisitService.DeleteAsync(model.Id, CancellationToken);

            // Assert
            await result.Should().ThrowAsync<PhotoStudiyEntityNotFoundException<Recvisit>>()
                .WithMessage($"*{model.Id}*");
        }

        /// <summary>
        /// Удаление <see cref="Recvisit"/>
        /// </summary>
        [Fact]
        public async Task DeleteShouldWork()
        {
            //Arrange
            var model = TestDataGenerator.Recvisit();
            await Context.Recvisits.AddAsync(model);
            await UnitOfWork.SaveChangesAsync(CancellationToken);

            //Act
            Func<Task> act = () => recvisitService.DeleteAsync(model.Id, CancellationToken);

            // Assert
            await act.Should().NotThrowAsync();
            var entity = Context.Recvisits.Single(x => x.Id == model.Id);
            entity.Should().NotBeNull();
            entity.DeletedAt.Should().NotBeNull();
        }

        /// <summary>
        /// Добавление <see cref="Recvisit"/>
        /// </summary>
        [Fact]
        public async Task AddShouldWork()
        {
            //Arrange
            var model = TestDataGenerator.RecvisitModel();

            //Act
            Func<Task> act = () => recvisitService.AddAsync(model, CancellationToken);

            // Assert
            await act.Should().NotThrowAsync();
            var entity = Context.Recvisits.Single(x => x.Id == model.Id);
            entity.Should().NotBeNull();
            entity.DeletedAt.Should().BeNull();
        }

        /// <summary>
        /// Добавление не валидируемого <see cref="Recvisit"/>
        /// </summary>
        [Fact]
        public async Task AddShouldValidationException()
        {
            //Arrange
            var model = TestDataGenerator.RecvisitModel(x => x.Name = "q");

            //Act
            Func<Task> act = () => recvisitService.AddAsync(model, CancellationToken);

            // Assert
            await act.Should().ThrowAsync<PhotoStudiyValidationException>();
        }

        /// <summary>
        /// Изменение несуществующего <see cref="Recvisit"/>
        /// </summary>
        [Fact]
        public async Task EditShouldNotFoundException()
        {
            //Arrange
            var model = TestDataGenerator.RecvisitModel();

            //Act
            Func<Task> act = () => recvisitService.EditAsync(model, CancellationToken);

            // Assert
            await act.Should().ThrowAsync<PhotoStudiyEntityNotFoundException<Recvisit>>()
                .WithMessage($"*{model.Id}*");
        }

        /// <summary>
        /// Изменение невалидируемого <see cref="Recvisit"/>
        /// </summary>
        [Fact]
        public async Task EditShouldValidationException()
        {
            //Arrange
            var model = TestDataGenerator.RecvisitModel(x => x.Name = "q");

            //Act
            Func<Task> act = () => recvisitService.EditAsync(model, CancellationToken);

            // Assert
            await act.Should().ThrowAsync<PhotoStudiyValidationException>();
        }

        /// <summary>
        /// Изменение <see cref="Recvisit"/>
        /// </summary>
        [Fact]
        public async Task EditShouldWork()
        {
            //Arrange
            var model = TestDataGenerator.RecvisitModel();
            var staff = TestDataGenerator.Recvisit(x => x.Id = model.Id);
            await Context.Recvisits.AddAsync(staff);
            await UnitOfWork.SaveChangesAsync(CancellationToken);

            //Act
            Func<Task> act = () => recvisitService.EditAsync(model, CancellationToken);

            // Assert
            await act.Should().NotThrowAsync();
            var entity = Context.Recvisits.Single(x => x.Id == staff.Id);
            entity.Should().NotBeNull()
                .And
                .BeEquivalentTo(new
                {
                    model.Id,
                    model.Name,
                    model.Description,
                    model.Amount
                });
        }
    }
}
