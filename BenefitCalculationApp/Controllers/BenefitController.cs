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
    [Route("api/Benefit")]
    public class BenefitController : Controller
    {
        private readonly EmployeeContext _context;

        public BenefitController(EmployeeContext context)
        {
            _context = context;
        }

        // GET: api/Benefit
        [HttpGet]
        public IEnumerable<Benefit> GetBenefit()
        {
            return _context.Benefit;
        }

        // GET: api/Benefit/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBenefit([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var benefit = await _context.Benefit.SingleOrDefaultAsync(m => m.Id == id);

            if (benefit == null)
            {
                return NotFound();
            }

            return Ok(benefit);
        }

        // PUT: api/Benefit/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBenefit([FromRoute] int id, [FromBody] Benefit benefit)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != benefit.Id)
            {
                return BadRequest();
            }

            _context.Entry(benefit).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BenefitExists(id))
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

        // POST: api/Benefit
        [HttpPost]
        public async Task<IActionResult> PostBenefit([FromBody] Benefit benefit)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Benefit.Add(benefit);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBenefit", new { id = benefit.Id }, benefit);
        }


        private bool BenefitExists(int id)
        {
            return _context.Benefit.Any(e => e.Id == id);
        }
    }
}