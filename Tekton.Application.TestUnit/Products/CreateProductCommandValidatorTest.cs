using FluentAssertions;
using FluentValidation.TestHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekton.Application.UseCases.Products.Commands.CreateProductCommand;

namespace Tekton.Application.TestUnit.Products
{
    public class CreateProductCommandValidatorTest
    {
        [Fact]
        public void CreateProductCommandValidator_IfNameIsNullOrEmpty_ShouldThrowValidationException()
        {
            //arrange
            var validator = new CreateProductCommandValidator();

            var createStoryCommand = new CreateProductCommand()
            {
                Name = "",
                Description = "prueba",
                Price = 1,
                Stock = 1
            };

            //act
            var result = validator.TestValidate(createStoryCommand);

            //assert
            result.IsValid.Should().BeFalse();
        }
        [Fact]
        public void CreateProductCommandValidator_IfDescriptionIsNullOrEmpty_ShouldThrowValidationException()
        {
            //arrange
            var validator = new CreateProductCommandValidator();

            var createStoryCommand = new CreateProductCommand()
            {
                Name = "prueba",
                Description = "",
                Price = 1,
                Stock = 1
            };

            //act
            var result = validator.TestValidate(createStoryCommand);

            //assert
            result.IsValid.Should().BeFalse();
        }
        [Fact]
        public void CreateProductCommandValidator_IfPriceIsZeroOrMinor_ShouldThrowValidationException()
        {
            //arrange
            var validator = new CreateProductCommandValidator();

            var createStoryCommand = new CreateProductCommand()
            {
                Name = "prueba",
                Description = "Description",
                Price = 0,
                Stock = 1
            };

            //act
            var result = validator.TestValidate(createStoryCommand);

            //assert
            result.IsValid.Should().BeFalse();
        }
        [Fact]
        public void CreateProductCommandValidator_IfStockIsZeroOrMinor_ShouldThrowValidationException()
        {
            //arrange
            var validator = new CreateProductCommandValidator();

            var createStoryCommand = new CreateProductCommand()
            {
                Name = "prueba",
                Description = "Description",
                Price = 1,
                Stock = -1
            };

            //act
            var result = validator.TestValidate(createStoryCommand);

            //assert
            result.IsValid.Should().BeFalse();
        }
    }
}
