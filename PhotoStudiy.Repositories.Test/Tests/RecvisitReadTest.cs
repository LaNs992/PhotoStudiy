using FluentAssertions;
using PhotoStudiy.Context.Tests;
using PhotoStudiy.Repositories.Contracts.ReadRepositoriesContracts;
using PhotoStudiy.Repositories.ReadRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PhotoStudiy.Repositories.Test.Tests
{
    public class RecvisitReadTest : PhotoStudiyContextMemory
    {
        private readonly IRecvisitReadRepository recvisitReadRepository;

        public RecvisitReadTest()
        {
            recvisitReadRepository = new RecvisitReadRepositories(Reader);
        }

        /// <summary>
        /// Возвращает пустой список Реквизитов
        /// </summary>
        [Fact]
        public async Task GetAllShouldReturnEmpty()
        {
            // Act
            var result = await recvisitReadRepository.GetAllAsync(CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.BeEmpty();
        }

        /// <summary>
        /// Возвращает список Реквизитов
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
            var result = await recvisitReadRepository.GetAllAsync(CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.HaveCount(1)
                .And.ContainSingle(x => x.Id == target.Id);
        }

        /// <summary>
        /// Получение Реквизитов по идентификатору возвращает null
        /// </summary>
        [Fact]
        public async Task GetByIdShouldReturnNull()
        {
            //Arrange
            var id = Guid.NewGuid();

            // Act
            var result = await recvisitReadRepository.GetByIdAsync(id, CancellationToken);

            // Assert
            result.Should().BeNull();
        }

        /// <summary>
        /// Получение Реквизитов по идентификатору возвращает данные
        /// </summary>
        [Fact]
        public async Task GetByIdShouldReturnValue()
        {
            //Arrange
            var target = TestDataGenerator.Recvisit();
            await Context.Recvisits.AddAsync(target);
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            var result = await recvisitReadRepository.GetByIdAsync(target.Id, CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.BeEquivalentTo(target);
        }

        /// <summary>
        /// Получение списка Реквизитов по идентификаторам возвращает пустую коллекцию
        /// </summary>
        [Fact]
        public async Task GetByIdsShouldReturnEmpty()
        {
            //Arrange
            var id1 = Guid.NewGuid();
            var id2 = Guid.NewGuid();
            var id3 = Guid.NewGuid();

            // Act
            var result = await recvisitReadRepository.GetByIdsAsync(new[] { id1, id2, id3 }, CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.BeEmpty();
        }

        /// <summary>
        /// Получение списка Реквизитов по идентификаторам возвращает данные
        /// </summary>
        [Fact]
        public async Task GetByIdsShouldReturnValue()
        {
            //Arrange
            var target1 = TestDataGenerator.Recvisit();
            var target2 = TestDataGenerator.Recvisit(x => x.DeletedAt = DateTimeOffset.UtcNow);
            var target3 = TestDataGenerator.Recvisit();
            var target4 = TestDataGenerator.Recvisit();
            await Context.Recvisits.AddRangeAsync(target1, target2, target3, target4);
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            var result = await recvisitReadRepository.GetByIdsAsync(new[] { target1.Id, target2.Id, target4.Id }, CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.HaveCount(2)
                .And.ContainKey(target1.Id)
                .And.ContainKey(target4.Id);
        }

        /// <summary>
        /// Поиск Реквизита в коллекции по идентификатору (true)
        /// </summary>
        [Fact]
        public async Task IsNotNullEntityReturnTrue()
        {
            //Arrange
            var target1 = TestDataGenerator.Recvisit();
            await Context.Recvisits.AddAsync(target1);
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            var result = await recvisitReadRepository.IsNotNullAsync(target1.Id, CancellationToken);

            // Assert
            result.Should().BeTrue();
        }

        /// <summary>
        /// Поиск Реквизита в коллекции по идентификатору (false)
        /// </summary>
        [Fact]
        public async Task IsNotNullEntityReturnFalse()
        {
            //Arrange
            var target1 = Guid.NewGuid();

            // Act
            var result = await recvisitReadRepository.IsNotNullAsync(target1, CancellationToken);

            // Assert
            result.Should().BeFalse();
        }

        /// <summary>
        /// Поиск удаленного Реквизита в коллекции по идентификатору
        /// </summary>
        [Fact]
        public async Task IsNotNullDeletedEntityReturnFalse()
        {
            //Arrange
            var target1 = TestDataGenerator.Recvisit(x => x.DeletedAt = DateTimeOffset.UtcNow);
            await Context.Recvisits.AddAsync(target1);
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            var result = await recvisitReadRepository.IsNotNullAsync(target1.Id, CancellationToken);

            // Assert
            result.Should().BeFalse();
        }
    }
}
