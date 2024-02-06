using FakeItEasy;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using Tekton.Application.UseCases.Products.Queries.GetProductByIdCommand;
using Tekton.Domain.Dto;
using Tekton.Service.WebApi.Controllers.v1;

namespace Tekton.Application.TestUnit.Products
{
    public class GetProductByIdQueryHandlerTest
    {

        
        [Fact]
        public async Task GetProductById_IfIdisZero()
        {
            var dataStore = A.Fake<IMediator>();
            var fakeRecipe = A.Dummy<Transversal.Common.Response<ProductDto>>();
            A.CallTo(() => dataStore.Send(new GetProductByIdQuery() { Id = 0 }, default)).Returns(Task.FromResult(fakeRecipe));
            var controller = new ProductController(dataStore);

            var result = await controller.Get(0);
            var r = result?.GetType();
            var aa = Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}