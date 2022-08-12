using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EGeladinho.Src.Models;
using EGeladinho.Src.Repository;
using Microsoft.AspNetCore.Mvc;

namespace EGeladinho.Src.Controllers
{
    [ApiController]
    [Route("api/Products")]
    [Produces("application/json")]
    public class ProductController : ControllerBase
    {

        private readonly ICrud<Product> _repository;

        public ProductController(ICrud<Product> repository)
        {
            _repository = repository;
        }
        
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Product product)
        {
            await _repository.Create(product);

            return Ok();
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult> Get([FromRoute] int id)
        {
            var product = await _repository.Read(id);
            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }
        
        [HttpPut]
        public async Task<ActionResult> Put([FromBody] Product product)
        {
            try
            {
                await _repository.Update(product);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            try
            {
                await _repository.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
    }
}