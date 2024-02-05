using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Tekton.Application.UseCases.Products.Commands.CreateProductCommand;
using Tekton.Application.UseCases.Products.Commands.UpdateProductCommand;
using Tekton.Application.UseCases.Products.Queries.GetProductByIdCommand;
using Swashbuckle.AspNetCore.Annotations;
using Tekton.Transversal.Common;
using Tekton.Domain.Entities;
using Tekton.Domain.Dto;

namespace Tekton.Service.WebApi.Controllers.v1
{
    [EnableRateLimiting("fixedWindow")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [SwaggerTag("Solicitudes al maestro de Productos")]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET api/<ProductController>/5
        [HttpGet("{id}")]
        [SwaggerOperation(
            Summary = "Obtener el producto por su Id",
            Description = "Este endpoint retorna el producto registrado por el Id",
            OperationId = "Get",
            Tags = new string[] { "Get" })]
        [SwaggerResponse(200, "Datos del producto", typeof(Response<ProductDto>))]
        [SwaggerResponse(404, "Producto no encontrado")]
        [SwaggerResponse(500, "Error del servidor")]
        [ProducesResponseType(typeof(Response<ProductDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get([FromRoute] int id)
        {            
            var response = await _mediator.Send(new GetProductByIdQuery() { Id = id});
            if (response.IsSuccess)
                return Ok(response);

            return BadRequest(response);
        }

        // POST api/<ProductController>
        [HttpPost]
        [SwaggerOperation(
            Summary = "Crear nuevo producto",
            Description = "Este endpoint crea un Producto",
            OperationId = "Post",
            Tags = new string[] { "Post" })]
        [SwaggerResponse(201, "Crear el producto", typeof(Response<bool>))]
        [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status201Created)]
        public async Task<IActionResult> Post([FromBody] CreateProductCommand command)
        {
            if (command == null)
                return BadRequest();
            var response = await _mediator.Send(command);
            if (response.IsSuccess)
                return CreatedAtAction(nameof(Post), new { IsSuccess = response.IsSuccess }, response);

            return BadRequest(response);
        }

        // PUT api/<ProductController>
        [HttpPut()]
        [SwaggerOperation(
            Summary = "Actualizar el producto",
            Description = "Este endpoint actualiza los datos del Producto por su Id",
            OperationId = "Put",
            Tags = new string[] { "Put" })]
        [SwaggerResponse(200, "Actualizar el producto", typeof(Response<bool>))]
        [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Put([FromBody] UpdateProductCommand command)
        {
            if (command == null)
                return BadRequest();

            var productDtoExists = await _mediator.Send(new GetProductByIdQuery() { Id = command.Id });
            if (productDtoExists.Data == null)
                return NotFound(productDtoExists);

            var response = await _mediator.Send(command);
            if (response.IsSuccess)
                return Ok(response);

            return BadRequest(response);
        }
    }
}
