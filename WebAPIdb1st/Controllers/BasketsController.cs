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
    public class BasketsController : ControllerBase
    {
        private readonly ApiContext _context;

        public BasketsController(ApiContext context)
        {
            _context = context;
        }

        // GET: api/Baskets
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Baskets>>> GetBaskets()
        {
            return await _context.Baskets.ToListAsync();
        }

        // GET: api/Baskets/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Baskets>> GetBaskets(string id)
        {
            var baskets = await _context.Baskets.FindAsync(id);

            if (baskets == null)
            {
                return NotFound();
            }

            return baskets;
        }

        // PUT: api/Baskets/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBaskets(string id, Baskets baskets)
        {
            if (id != baskets.Id)
            {
                return BadRequest();
            }

            _context.Entry(baskets).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BasketsExists(id))
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

        // POST: api/Baskets
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Baskets>> PostBaskets(Baskets baskets)
        {
            _context.Baskets.Add(baskets);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (BasketsExists(baskets.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetBaskets", new { id = baskets.Id }, baskets);
        }

        // DELETE: api/Baskets/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Baskets>> DeleteBaskets(string id)
        {
            var baskets = await _context.Baskets.FindAsync(id);
            if (baskets == null)
            {
                return NotFound();
            }

            _context.Baskets.Remove(baskets);
            await _context.SaveChangesAsync();

            return baskets;
        }

        private bool BasketsExists(string id)
        {
            return _context.Baskets.Any(e => e.Id == id);
        }
    }
}
