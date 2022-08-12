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
    [Route("api/Carts")]
    [Produces("application/json")]
    public class CartController : ControllerBase
    {
        private readonly ICrud<Cart> _repository;

        public CartController(ICrud<Cart> repository)
        {
            _repository = repository;
        }
        
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Cart cart)
        {
            await _repository.Create(cart);

            return Ok();
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult> Get([FromRoute] int id)
        {
            var cart = await _repository.Read(id);
            if (cart == null)
            {
                return NotFound();
            }

            return Ok(cart);
        }
        
        [HttpPut]
        public async Task<ActionResult> Put([FromBody] Cart cart)
        {
            try
            {
                await _repository.Update(cart);
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