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
    public class UslugiReadTest : PhotoStudiyContextMemory
    {
        private readonly IUslugiReadRepository uslugiReadRepository;

        public UslugiReadTest()
        {
            uslugiReadRepository = new UslugiReadRepositories(Reader);
        }

        /// <summary>
        /// Возвращает пустой список Услуг
        /// </summary>
        [Fact]
        public async Task GetAllShouldReturnEmpty()
        {
            // Act
            var result = await uslugiReadRepository.GetAllAsync(CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.BeEmpty();
        }

        /// <summary>
        /// Возвращает список Услуг
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
            var result = await uslugiReadRepository.GetAllAsync(CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.HaveCount(1)
                .And.ContainSingle(x => x.Id == target.Id);
        }

        /// <summary>
        /// Получение Услуг по идентификатору возвращает null
        /// </summary>
        [Fact]
        public async Task GetByIdShouldReturnNull()
        {
            //Arrange
            var id = Guid.NewGuid();

            // Act
            var result = await uslugiReadRepository.GetByIdAsync(id, CancellationToken);

            // Assert
            result.Should().BeNull();
        }

        /// <summary>
        /// Получение Услуг по идентификатору возвращает данные
        /// </summary>
        [Fact]
        public async Task GetByIdShouldReturnValue()
        {
            //Arrange
            var target = TestDataGenerator.Uslugi();
            await Context.Uslugs.AddAsync(target);
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            var result = await uslugiReadRepository.GetByIdAsync(target.Id, CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.BeEquivalentTo(target);
        }

        /// <summary>
        /// Получение списка Услуг по идентификаторам возвращает пустую коллекцию
        /// </summary>
        [Fact]
        public async Task GetByIdsShouldReturnEmpty()
        {
            //Arrange
            var id1 = Guid.NewGuid();
            var id2 = Guid.NewGuid();
            var id3 = Guid.NewGuid();

            // Act
            var result = await uslugiReadRepository.GetByIdsAsync(new[] { id1, id2, id3 }, CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.BeEmpty();
        }

        /// <summary>
        /// Получение списка Услуг по идентификаторам возвращает данные
        /// </summary>
        [Fact]
        public async Task GetByIdsShouldReturnValue()
        {
            //Arrange
            var target1 = TestDataGenerator.Uslugi();
            var target2 = TestDataGenerator.Uslugi(x => x.DeletedAt = DateTimeOffset.UtcNow);
            var target3 = TestDataGenerator.Uslugi();
            var target4 = TestDataGenerator.Uslugi();
            await Context.Uslugs.AddRangeAsync(target1, target2, target3, target4);
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            var result = await uslugiReadRepository.GetByIdsAsync(new[] { target1.Id, target2.Id, target4.Id }, CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.HaveCount(2)
                .And.ContainKey(target1.Id)
                .And.ContainKey(target4.Id);
        }

        /// <summary>
        /// Поиск Услуги в коллекции по идентификатору (true)
        /// </summary>
        [Fact]
        public async Task IsNotNullEntityReturnTrue()
        {
            //Arrange
            var target1 = TestDataGenerator.Uslugi();
            await Context.Uslugs.AddAsync(target1);
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            var result = await uslugiReadRepository.IsNotNullAsync(target1.Id, CancellationToken);

            // Assert
            result.Should().BeTrue();
        }

        /// <summary>
        /// Поиск Услуги в коллекции по идентификатору (false)
        /// </summary>
        [Fact]
        public async Task IsNotNullEntityReturnFalse()
        {
            //Arrange
            var target1 = Guid.NewGuid();

            // Act
            var result = await uslugiReadRepository.IsNotNullAsync(target1, CancellationToken);

            // Assert
            result.Should().BeFalse();
        }

        /// <summary>
        /// Поиск удаленного Услуги в коллекции по идентификатору
        /// </summary>
        [Fact]
        public async Task IsNotNullDeletedEntityReturnFalse()
        {
            //Arrange
            var target1 = TestDataGenerator.Uslugi(x => x.DeletedAt = DateTimeOffset.UtcNow);
            await Context.Uslugs.AddAsync(target1);
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            var result = await uslugiReadRepository.IsNotNullAsync(target1.Id, CancellationToken);

            // Assert
            result.Should().BeFalse();
        }
    }
}
