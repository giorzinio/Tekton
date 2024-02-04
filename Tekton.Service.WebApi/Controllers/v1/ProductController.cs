using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Tekon.Application.DTO;
using Tekton.Application.Interface.UseCases;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Tekton.Service.WebApi.Controllers.v1
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class ProductController : ControllerBase
    {
        private readonly IProductApplication _productApplication;

        public ProductController(IProductApplication productApplication)
        {
            _productApplication = productApplication;
        }        

        // GET api/<ProductController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var response = await _productApplication.GetById(id);
            if (response.IsSuccess)
                return Ok(response);

            return BadRequest(response);
        }

        // POST api/<ProductController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ProductDto productDto)
        {
            if (productDto == null)
                return BadRequest();
            var response = await _productApplication.Create(productDto);
            if (response.IsSuccess)
                return Ok(response);

            return BadRequest(response);
        }

        // PUT api/<ProductController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] ProductDto productDto)
        {
            var productDtoExists = await _productApplication.GetById(id);
            if (productDtoExists.Data == null)
                return NotFound(productDtoExists);

            if (productDto == null)
                return BadRequest();
            var response = await _productApplication.Update(productDto);
            if (response.IsSuccess)
                return Ok(response);

            return BadRequest(response);
        }
    }
}
