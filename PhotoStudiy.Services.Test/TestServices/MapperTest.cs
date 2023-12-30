using AutoMapper;
using PhotoStudiy.Services.AutoMappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PhotoStudiy.Services.Test.TestServices
{
    public class MapperTest
    {
        [Fact]
        public void TestMapper()
        {
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile<ServiceMapper>());
            configuration.AssertConfigurationIsValid();
        }
    }
}
