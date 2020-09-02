using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPIdb1st.Models;

namespace WebAPIdb1st.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketItemsController : ControllerBase
    {
        private readonly ApiContext _context;

        public BasketItemsController(ApiContext context)
        {
            _context = context;
        }

        // GET: api/BasketItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BasketItems>>> GetBasketItems()
        {
            return await _context.BasketItems.ToListAsync();
        }

        // GET: api/BasketItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BasketItems>> GetBasketItems(string id)
        {
            var basketItems = await _context.BasketItems.FindAsync(id);

            if (basketItems == null)
            {
                return NotFound();
            }

            return basketItems;
        }

        // PUT: api/BasketItems/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBasketItems(string id, BasketItems basketItems)
        {
            if (id != basketItems.Id)
            {
                return BadRequest();
            }

            _context.Entry(basketItems).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BasketItemsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/BasketItems
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<BasketItems>> PostBasketItems(BasketItems basketItems)
        {
            _context.BasketItems.Add(basketItems);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (BasketItemsExists(basketItems.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetBasketItems", new { id = basketItems.Id }, basketItems);
        }

        // DELETE: api/BasketItems/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<BasketItems>> DeleteBasketItems(string id)
        {
            var basketItems = await _context.BasketItems.FindAsync(id);
            if (basketItems == null)
            {
                return NotFound();
            }

            _context.BasketItems.Remove(basketItems);
            await _context.SaveChangesAsync();

            return basketItems;
        }

        private bool BasketItemsExists(string id)
        {
            return _context.BasketItems.Any(e => e.Id == id);
        }
    }
}
