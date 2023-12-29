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
    public class UslugiModelValidatorTest
    {
        private readonly UslugiModelValidator validator;

        public UslugiModelValidatorTest()
        {
            validator = new UslugiModelValidator();
        }

        /// <summary>
        /// Тест на наличие ошибок
        /// </summary>
        [Fact]
        public void ValidatorShouldError()
        {
            //Arrange
            var model = TestDataGenerator.UslugiModel(x => { x.Name = "1"; x.Price = "1"; });

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
            var model = TestDataGenerator.UslugiModel();

            // Act
            var result = validator.TestValidate(model);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
