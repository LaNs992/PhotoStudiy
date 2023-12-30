using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using PhotoStudiy.API.Tests.Infrastuctures;
using Microsoft.Extensions.DependencyInjection;
using PhotoStudiy.API.Controllers;
using FluentAssertions;

namespace PhotoStudiy.API.Tests.Tests
{
    public class DependeciensTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> factory;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="DependenciesTests"/>
        /// </summary>
        public DependeciensTests(WebApplicationFactory<Program> factory)
        {
            this.factory = factory.WithWebHostBuilder(builder => builder.ConfigureTestAppConfiguration());
        }

        /// <summary>
        /// Проверка резолва зависимостей
        /// </summary>
        //[Theory]
        //[MemberData(nameof(ApiControllerCore))]
        //public void ControllerCoreShouldBeResolved(Type controller)
        //{
        //    // Arrange
        //    using var scope = factory.Services.CreateScope();

        //    // Act
        //    var instance = scope.ServiceProvider.GetRequiredService(controller);

        //    // Assert
        //    instance.Should().NotBeNull();
        //}

        /// <summary>
        /// Коллекция контроллеров по администрированию
        /// </summary>
        public static IEnumerable<object[]>? ApiControllerCore =>
            Assembly.GetAssembly(typeof(ClientController))
                ?.DefinedTypes
                .Where(type => typeof(ControllerBase).IsAssignableFrom(type))
                .Where(type => !type.IsAbstract)
                .Select(type => new[] { type });
    }
}
