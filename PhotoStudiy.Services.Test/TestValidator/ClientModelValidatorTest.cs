using FluentValidation.TestHelper;
using PhotoStudiy.Repositories.Test;
using PhotoStudiy.Services.Validator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PhotoStudiy.Services.Test.TestValidator
{
    public class ClientModelValidatorTest
    {
        private readonly ClientModelValidator validator;

        public ClientModelValidatorTest()
        {
            validator = new ClientModelValidator();
        }

        /// <summary>
        /// Тест на наличие ошибок
        /// </summary>
        [Fact]
        public void ValidatorShouldError()
        {
            //Arrange
            var model = TestDataGenerator.ClientModel(x => { x.Name = "1"; x.LastName = "1";  x.Number = "1"; });

            // Act
            var result = validator.TestValidate(model);

            // Assert
            result.ShouldHaveAnyValidationError();
        }

        /// <summary>
        /// Тест на отсутствие ошибок
        /// </summary>
        [Fact]
        public void ValidatorShouldSuccess()
        {
            //Arrange
            var model = TestDataGenerator.ClientModel();

            // Act
            var result = validator.TestValidate(model);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
