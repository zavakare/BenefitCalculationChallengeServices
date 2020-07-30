using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BenefitCalculationApp.Models;

namespace BenefitCalculationApp.Controllers
{
    [Produces("application/json")]
    [Route("api/Dependent")]
    public class DependentController : Controller
    {
        private readonly EmployeeContext _context;

        public DependentController(EmployeeContext context)
        {
            _context = context;
        }

        // GET: api/Dependent
        [HttpGet]
        public IEnumerable<Dependent> GetDependent()
        {
            return _context.Dependent;
        }

        // GET: api/Dependent/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDependent([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var dependent = await _context.Dependent.SingleOrDefaultAsync(m => m.Id == id);

            if (dependent == null)
            {
                return NotFound();
            }

            return Ok(dependent);
        }

        // PUT: api/Dependent/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDependent([FromRoute] int id, [FromBody] Dependent dependent)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != dependent.Id)
            {
                return BadRequest();
            }

            _context.Entry(dependent).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DependentExists(id))
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

        // POST: api/Dependent
        [HttpPost]
        public async Task<IActionResult> PostDependent([FromBody] Dependent dependent)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Dependent.Add(dependent);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDependent", new { id = dependent.Id }, dependent);
        }

        // DELETE: api/Dependent/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDependent([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var dependent = await _context.Dependent.SingleOrDefaultAsync(m => m.Id == id);
            if (dependent == null)
            {
                return NotFound();
            }

            _context.Dependent.Remove(dependent);
            await _context.SaveChangesAsync();

            return Ok(dependent);
        }

        private bool DependentExists(int id)
        {
            return _context.Dependent.Any(e => e.Id == id);
        }
    }
}