using AutoMapper;
using FluentAssertions;
using PhotoStudiy.Common.Entity.InterfaceDB;
using PhotoStudiy.Context.Contracts.Models;
using PhotoStudiy.Context.Tests;
using PhotoStudiy.Repositories.Contracts.ReadRepositoriesContracts;
using PhotoStudiy.Repositories.Contracts.WriteRepositoriesContracts;
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
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PhotoStudiy.Services.Test.TestServices
{
    public class PhotographServiceTest : PhotoStudiyContextMemory
    {
        private readonly IPhotographService photographService;
        private readonly PhotographReadRepositories  photographRead;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="CinemaServiceTest"/>
        /// </summary>
        public PhotographServiceTest()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ServiceMapper());
            });
            photographRead = new PhotographReadRepositories(Reader);

            photographService = new PhotographService(new PhotographWriteRepository(WriterContext), photographRead,
                UnitOfWork, config.CreateMapper(), new ServicesValidatorService(photographRead, new ClientReadRepositories(Reader),
                 new PhotoSetReadRepositories(Reader), new UslugiReadRepositories(Reader), new RecvisitReadRepositories(Reader), new ProductsReadRepositories(Reader)));
        }
        /// <summary>
        /// Получение <see cref="Photogragh"/> по идентификатору возвращает null
        /// </summary>
        [Fact]
        public async Task GetByIdShouldReturnNull()
        {
            //Arrange
            var id = Guid.NewGuid();

            // Act
            Func<Task> result = () => photographService.GetByIdAsync(id, CancellationToken);

            // Assert
            await result.Should().ThrowAsync<PhotoStudiyEntityNotFoundException<Photogragh>>()
               .WithMessage($"*{id}*");
        }

        /// <summary>
        /// Получение <see cref="Photogragh"/> по идентификатору возвращает данные
        /// </summary>
        [Fact]
        public async Task GetByIdShouldReturnValue()
        {
            //Arrange
            var target = TestDataGenerator.Photogragh();
            await Context.Photograghs.AddAsync(target);
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            var result = await photographService.GetByIdAsync(target.Id, CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.BeEquivalentTo(new
                {
                    target.Name,
                    target.LastName,
                    target.Number,
                });
        }

        /// <summary>
        /// Получение <see cref="IEnumerable{Photogragh}"/> по идентификаторам возвращает пустйю коллекцию
        /// </summary>
        [Fact]
        public async Task GetAllShouldReturnEmpty()
        {
            // Act
            var result = await photographService.GetAllAsync(CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.BeEmpty();
        }

        /// <summary>
        /// Получение <see cref="IEnumerable{Photogragh}"/> по идентификаторам возвращает данные
        /// </summary>
        [Fact]
        public async Task GetAllShouldReturnValues()
        {
            //Arrange
            var target = TestDataGenerator.Photogragh();

            await Context.Photograghs.AddRangeAsync(target,
                TestDataGenerator.Photogragh(x => x.DeletedAt = DateTimeOffset.UtcNow));
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            var result = await photographService.GetAllAsync(CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.HaveCount(1)
                .And.ContainSingle(x => x.Id == target.Id);
        }

        /// <summary>
        /// Удаление несуществуюущего <see cref="Photogragh"/>
        /// </summary>
        [Fact]
        public async Task DeleteShouldNotFoundException()
        {
            //Arrange
            var id = Guid.NewGuid();

            // Act
            Func<Task> result = () => photographService.DeleteAsync(id, CancellationToken);

            // Assert
            await result.Should().ThrowAsync<PhotoStudiyEntityNotFoundException<Photogragh>>()
                .WithMessage($"*{id}*");
        }

        /// <summary>
        /// Удаление удаленного <see cref="Photogragh"/>
        /// </summary>
        [Fact]
        public async Task DeletingDeletedCReturnExсeption()
        {
            //Arrange
            var model = TestDataGenerator.Photogragh(x => x.DeletedAt = DateTime.UtcNow);
            await Context.Photograghs.AddAsync(model);
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            Func<Task> result = () => photographService.DeleteAsync(model.Id, CancellationToken);

            // Assert
            await result.Should().ThrowAsync<PhotoStudiyEntityNotFoundException<Photogragh>>()
                .WithMessage($"*{model.Id}*");
        }

        /// <summary>
        /// Удаление <see cref="Photogragh"/>
        /// </summary>
        [Fact]
        public async Task DeleteShouldWork()
        {
            //Arrange
            var model = TestDataGenerator.Photogragh();
            await Context.Photograghs.AddAsync(model);
            await UnitOfWork.SaveChangesAsync(CancellationToken);

            //Act
            Func<Task> act = () => photographService.DeleteAsync(model.Id, CancellationToken);

            // Assert
            await act.Should().NotThrowAsync();
            var entity = Context.Photograghs.Single(x => x.Id == model.Id);
            entity.Should().NotBeNull();
            entity.DeletedAt.Should().NotBeNull();
        }

        /// <summary>
        /// Добавление <see cref="Photogragh"/>
        /// </summary>
        [Fact]
        public async Task AddShouldWork()
        {
            //Arrange
            var model = TestDataGenerator.PhotograghModel();

            //Act
            Func<Task> act = () => photographService.AddAsync(model, CancellationToken);

            // Assert
            await act.Should().NotThrowAsync();
            var entity = Context.Photograghs.Single(x => x.Id == model.Id);
            entity.Should().NotBeNull();
            entity.DeletedAt.Should().BeNull();
        }

        /// <summary>
        /// Добавление не валидируемого <see cref="Photogragh"/>
        /// </summary>
        [Fact]
        public async Task AddShouldValidationException()
        {
            //Arrange
            var model = TestDataGenerator.PhotograghModel(x => x.Name = "T");

            //Act
            Func<Task> act = () => photographService.AddAsync(model, CancellationToken);

            // Assert
            await act.Should().ThrowAsync<PhotoStudiyValidationException>();
        }

        /// <summary>
        /// Изменение несуществующего <see cref="Photogragh"/>
        /// </summary>
        [Fact]
        public async Task EditShouldNotFoundException()
        {
            //Arrange
            var model = TestDataGenerator.PhotograghModel();

            //Act
            Func<Task> act = () => photographService.EditAsync(model, CancellationToken);

            // Assert
            await act.Should().ThrowAsync<PhotoStudiyEntityNotFoundException<Photogragh>>()
                .WithMessage($"*{model.Id}*");
        }

        /// <summary>
        /// Изменение невалидируемого <see cref="Photogragh"/>
        /// </summary>
        [Fact]
        public async Task EditShouldValidationException()
        {
            //Arrange
            var model = TestDataGenerator.PhotograghModel(x => x.Name = "T");

            //Act
            Func<Task> act = () => photographService.EditAsync(model, CancellationToken);

            // Assert
            await act.Should().ThrowAsync<PhotoStudiyValidationException>();
        }

        /// <summary>
        /// Изменение <see cref="Photogragh"/>
        /// </summary>
        [Fact]
        public async Task EditShouldWork()
        {
            //Arrange
            var model = TestDataGenerator.PhotograghModel();
            var cinema = TestDataGenerator.Photogragh(x => x.Id = model.Id);
            await Context.Photograghs.AddAsync(cinema);
            await UnitOfWork.SaveChangesAsync(CancellationToken);

            //Act
            Func<Task> act = () => photographService.EditAsync(model, CancellationToken);

            // Assert
            await act.Should().NotThrowAsync();
            var entity = Context.Photograghs.Single(x => x.Id == cinema.Id);
            entity.Should().NotBeNull()
                .And
                .BeEquivalentTo(new
                {
                    model.Id,
                    model.Name,
                    model.LastName,
                    model.Number
                });
        }
    }
}
