using AutoMapper;
using FluentAssertions;
using PhotoStudiy.Common.Entity.InterfaceDB;
using PhotoStudiy.Context.Tests;
using PhotoStudiy.Repositories.Contracts.ReadRepositoriesContracts;
using PhotoStudiy.Repositories.ReadRepositories;
using PhotoStudiy.Services.AutoMappers;
using PhotoStudiy.Services.Contracts.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PhotoStudiy.Repositories.Test.Tests
{
    public class DogovorReadTest : PhotoStudiyContextMemory
    {
        private readonly IDogovorReadRepository dogovorReadRepository;

        public DogovorReadTest()
        {
            dogovorReadRepository = new DogovorReadRepositories(Reader);
        }

        /// <summary>
        /// Возвращает пустой список договоров
        /// </summary>
        [Fact]
        public async Task GetAllShouldReturnEmpty()
        {
            // Act
            var result = await dogovorReadRepository.GetAllAsync(CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.BeEmpty();
        }

        /// <summary>
        /// Возвращает список договоров
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
            var result = await dogovorReadRepository.GetAllAsync(CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.HaveCount(1)
                .And.ContainSingle(x => x.Id == target.Id);
        }

        /// <summary>
        /// Получение договора по идентификатору возвращает null
        /// </summary>
        [Fact]
        public async Task GetByIdShouldReturnNull()
        {
            //Arrange
            var id = Guid.NewGuid();

            // Act
            var result = await dogovorReadRepository.GetByIdAsync(id, CancellationToken);

            // Assert
            result.Should().BeNull();
        }

        /// <summary>
        /// Получение договора по идентификатору возвращает данные
        /// </summary>
        [Fact]
        public async Task GetByIdShouldReturnValue()
        {
            //Arrange
            var target = TestDataGenerator.Dogovor();
            await Context.Dogovors.AddAsync(target);
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            var result = await dogovorReadRepository.GetByIdAsync(target.Id, CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.BeEquivalentTo(target);
        }

        /// <summary>
        /// Поиск договора в коллекции по идентификатору (true)
        /// </summary>
        [Fact]
        public async Task IsNotNullEntityReturnTrue()
        {
            //Arrange
            var target1 = TestDataGenerator.Dogovor();
            await Context.Dogovors.AddAsync(target1);
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            var result = await dogovorReadRepository.IsNotNullAsync(target1.Id, CancellationToken);

            // Assert
            result.Should().BeTrue();
        }

        /// <summary>
        /// Поиск договора в коллекции по идентификатору (false)
        /// </summary>
        [Fact]
        public async Task IsNotNullEntityReturnFalse()
        {
            //Arrange
            var target1 = Guid.NewGuid();

            // Act
            var result = await dogovorReadRepository.IsNotNullAsync(target1, CancellationToken);

            // Assert
            result.Should().BeFalse();
        }

        /// <summary>
        /// Поиск удаленного договора в коллекции по идентификатору
        /// </summary>
        [Fact]
        public async Task IsNotNullDeletedEntityReturnFalse()
        {
            //Arrange
            var target1 = TestDataGenerator.Dogovor(x => x.DeletedAt = DateTimeOffset.UtcNow);
            await Context.Dogovors.AddAsync(target1);
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            var result = await dogovorReadRepository.IsNotNullAsync(target1.Id, CancellationToken);

            // Assert
            result.Should().BeFalse();
        }
    }
}
