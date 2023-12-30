using AutoMapper;
using FluentAssertions;
using FluentAssertions.Extensions;
using PhotoStudiy.API.AutoMappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PhotoStudiy.API.Tests.Tests
{
    public class MapperTest
    {
        [Fact]
        public void TestMapper()
        {
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile<APIMappers>());
            configuration.AssertConfigurationIsValid();
        }

        [Fact]
        public void TestValidate()
        {
            var item = 1.March(2022).At(20, 30).AsLocal();
            var item2 = 2.March(2022).At(20, 30).AsLocal();
            item.Should().NotBe(item2);
        }
    }
}
