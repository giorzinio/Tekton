using FluentAssertions;
using FluentValidation.TestHelper;
using Tekton.Application.UseCases.Products.Commands.UpdateProductCommand;

namespace Tekton.Application.TestUnit.Products
{
    public class UpdateProductCommandValidatorTest
    {
        [Fact]
        public void UpdateProductCommandValidator_IfNameIsNullOrEmpty_ShouldThrowValidationException()
        {
            //arrange
            var validator = new UpdateProductCommandValidator();

            var UpdateStoryCommand = new UpdateProductCommand()
            {
                Name = "",
                Description = "prueba",
                Price = 1,
                Stock = 1
            };

            //act
            var result = validator.TestValidate(UpdateStoryCommand);

            //assert
            result.IsValid.Should().BeFalse();
        }
        [Fact]
        public void UpdateProductCommandValidator_IfDescriptionIsNullOrEmpty_ShouldThrowValidationException()
        {
            //arrange
            var validator = new UpdateProductCommandValidator();

            var UpdateStoryCommand = new UpdateProductCommand()
            {
                Name = "prueba",
                Description = "",
                Price = 1,
                Stock = 1
            };

            //act
            var result = validator.TestValidate(UpdateStoryCommand);

            //assert
            result.IsValid.Should().BeFalse();
        }
        [Fact]
        public void UpdateProductCommandValidator_IfPriceIsZeroOrMinor_ShouldThrowValidationException()
        {
            //arrange
            var validator = new UpdateProductCommandValidator();

            var UpdateStoryCommand = new UpdateProductCommand()
            {
                Name = "prueba",
                Description = "Description",
                Price = 0,
                Stock = 1
            };

            //act
            var result = validator.TestValidate(UpdateStoryCommand);

            //assert
            result.IsValid.Should().BeFalse();
        }
        [Fact]
        public void UpdateProductCommandValidator_IfStockIsZeroOrMinor_ShouldThrowValidationException()
        {
            //arrange
            var validator = new UpdateProductCommandValidator();

            var UpdateStoryCommand = new UpdateProductCommand()
            {
                Name = "prueba",
                Description = "Description",
                Price = 1,
                Stock = -1
            };

            //act
            var result = validator.TestValidate(UpdateStoryCommand);

            //assert
            result.IsValid.Should().BeFalse();
        }
    }
}
