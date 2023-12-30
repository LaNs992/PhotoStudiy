using AutoMapper;
using FluentAssertions;
using PhotoStudiy.Context.Contracts.Models;
using PhotoStudiy.Context.Tests;
using PhotoStudiy.Repositories.ReadRepositories;
using PhotoStudiy.Repositories.Test;
using PhotoStudiy.Repositories.WriteRepositories;
using PhotoStudiy.Services.AutoMappers;
using PhotoStudiy.Services.Contracts.Exceptions;
using PhotoStudiy.Services.Contracts.Interface;
using PhotoStudiy.Services.Services;
using Xunit;

namespace PhotoStudiy.Services.Test.TestServices
{
    public class ClientServiceTest : PhotoStudiyContextMemory
    {
        private readonly IClientService clientService;
        private readonly ClientReadRepositories clientReadRepository;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="ClientServiceTest"/>
        /// </summary>
        public ClientServiceTest()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ServiceMapper());
            });
            clientReadRepository = new ClientReadRepositories(Reader);

            clientService = new ClientService(new ClientWriteRepository(WriterContext), clientReadRepository,
                UnitOfWork, config.CreateMapper(), new ServicesValidatorService(new PhotographReadRepositories(Reader),
                clientReadRepository, new PhotoSetReadRepositories(Reader),new UslugiReadRepositories(Reader),new RecvisitReadRepositories(Reader), new ProductsReadRepositories(Reader)));
        }

        /// <summary>
        /// Получение <see cref="Client"/> по идентификатору возвращает null
        /// </summary>
        [Fact]
        public async Task GetByIdShouldReturnNull()
        {
            //Arrange
            var id = Guid.NewGuid();

            // Act
            Func<Task> result = () => clientService.GetByIdAsync(id, CancellationToken);

            // Assert
            await result.Should().ThrowAsync<PhotoStudiyEntityNotFoundException<Client>>()
               .WithMessage($"*{id}*");
        }

        /// <summary>
        /// Получение <see cref="Client"/> по идентификатору возвращает данные
        /// </summary>
        [Fact]
        public async Task GetByIdShouldReturnValue()
        {
            //Arrange
            var target = TestDataGenerator.Client();
            await Context.Clients.AddAsync(target);
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            var result = await clientService.GetByIdAsync(target.Id, CancellationToken);

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
        /// Получение <see cref="IEnumerable{Client}"/> по идентификаторам возвращает пустую коллекцию
        /// </summary>
        [Fact]
        public async Task GetAllShouldReturnEmpty()
        {
            // Act
            var result = await clientService.GetAllAsync(CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.BeEmpty();
        }

        /// <summary>
        /// Получение <see cref="IEnumerable{Client}"/> по идентификаторам возвращает данные
        /// </summary>
        [Fact]
        public async Task GetAllShouldReturnValues()
        {
            //Arrange
            var target = TestDataGenerator.Client();

            await Context.Clients.AddRangeAsync(target,
                TestDataGenerator.Client(x => x.DeletedAt = DateTimeOffset.UtcNow));
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            var result = await clientService.GetAllAsync(CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.HaveCount(1)
                .And.ContainSingle(x => x.Id == target.Id);
        }

        /// <summary>
        /// Удаление несуществуюущего <see cref="Client"/>
        /// </summary>
        [Fact]
        public async Task DeletingNonExistentClientReturnExсeption()
        {
            //Arrange
            var id = Guid.NewGuid();

            // Act
            Func<Task> result = () => clientService.DeleteAsync(id, CancellationToken);

            // Assert
            await result.Should().ThrowAsync<PhotoStudiyEntityNotFoundException<Client>>()
               .WithMessage($"*{id}*");
        }

        /// <summary>
        /// Удаление удаленного <see cref="Client"/>
        /// </summary>
        [Fact]
        public async Task DeletingDeletedClientReturnExсeption()
        {
            //Arrange
            var model = TestDataGenerator.Client(x => x.DeletedAt = DateTime.UtcNow);
            await Context.Clients.AddAsync(model);
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            Func<Task> result = () => clientService.DeleteAsync(model.Id, CancellationToken);

            // Assert
            await result.Should().ThrowAsync<PhotoStudiyEntityNotFoundException<Client>>()
                .WithMessage($"*{model.Id}*");
        }

        /// <summary>
        /// Удаление <see cref="Client"/>
        /// </summary>
        [Fact]
        public async Task DeleteShouldWork()
        {
            //Arrange
            var model = TestDataGenerator.Client();
            await Context.Clients.AddAsync(model);
            await UnitOfWork.SaveChangesAsync(CancellationToken);

            //Act
            Func<Task> act = () => clientService.DeleteAsync(model.Id, CancellationToken);

            // Assert
            await act.Should().NotThrowAsync();
            var entity = Context.Clients.Single(x => x.Id == model.Id);
            entity.Should().NotBeNull();
            entity.DeletedAt.Should().NotBeNull();
        }

        /// <summary>
        /// Добавление <see cref="Client"/>
        /// </summary>
        [Fact]
        public async Task AddShouldWork()
        {
            //Arrange
            var model = TestDataGenerator.ClientModel();

            //Act
            Func<Task> act = () => clientService.AddAsync(model, CancellationToken);

            // Assert
            await act.Should().NotThrowAsync();
            var entity = Context.Clients.Single(x => x.Id == model.Id);
            entity.Should().NotBeNull();
            entity.DeletedAt.Should().BeNull();
        }

        /// <summary>
        /// Добавление невалидируемого <see cref="Client"/>
        /// </summary>
        [Fact]
        public async Task AddShouldValidationException()
        {
            //Arrange
            var model = TestDataGenerator.ClientModel(x => x.Name = "T");

            //Act
            Func<Task> act = () => clientService.AddAsync(model, CancellationToken);

            // Assert
            await act.Should().ThrowAsync<PhotoStudiyValidationException>();
        }

        /// <summary>
        /// Изменение несуществующего <see cref="Client"/>
        /// </summary>
        [Fact]
        public async Task EditShouldNotFoundException()
        {
            //Arrange
            var model = TestDataGenerator.ClientModel();

            //Act
            Func<Task> act = () => clientService.EditAsync(model, CancellationToken);

            // Assert
            await act.Should().ThrowAsync<PhotoStudiyEntityNotFoundException<Client>>()
                .WithMessage($"*{model.Id}*");
        }

        /// <summary>
        /// Изменение невалидируемого <see cref="Client"/>
        /// </summary>
        [Fact]
        public async Task EditShouldValidationException()
        {
            //Arrange
            var model = TestDataGenerator.ClientModel(x => x.Name = "T");

            //Act
            Func<Task> act = () => clientService.EditAsync(model, CancellationToken);

            // Assert
            await act.Should().ThrowAsync<PhotoStudiyValidationException>();
        }

        /// <summary>
        /// Изменение <see cref="Client"/>
        /// </summary>
        [Fact]
        public async Task EditShouldWork()
        {
            //Arrange
            var model = TestDataGenerator.ClientModel();
            var client = TestDataGenerator.Client(x => x.Id = model.Id);
            await Context.Clients.AddAsync(client);
            await UnitOfWork.SaveChangesAsync(CancellationToken);

            //Act
            Func<Task> act = () => clientService.EditAsync(model, CancellationToken);

            // Assert
            await act.Should().NotThrowAsync();
            var entity = Context.Clients.Single(x => x.Id == client.Id);
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
