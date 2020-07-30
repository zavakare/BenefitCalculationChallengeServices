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
    [Route("api/Salary")]
    public class SalaryController : Controller
    {
        private readonly EmployeeContext _context;

        public SalaryController(EmployeeContext context)
        {
            _context = context;
        }

        // GET: api/Salary
        [HttpGet]
        public IEnumerable<Salary> GetSalary()
        {
            return _context.Salary;
        }

        // GET: api/Salary/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSalary([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var salary = await _context.Salary.SingleOrDefaultAsync(m => m.Id == id);

            if (salary == null)
            {
                return NotFound();
            }

            return Ok(salary);
        }

        // PUT: api/Salary/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSalary([FromRoute] int id, [FromBody] Salary salary)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != salary.Id)
            {
                return BadRequest();
            }

            _context.Entry(salary).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SalaryExists(id))
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

        // POST: api/Salary
        [HttpPost]
        public async Task<IActionResult> PostSalary([FromBody] Salary salary)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Salary.Add(salary);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSalary", new { id = salary.Id }, salary);
        }

        // DELETE: api/Salary/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSalary([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var salary = await _context.Salary.SingleOrDefaultAsync(m => m.Id == id);
            if (salary == null)
            {
                return NotFound();
            }

            _context.Salary.Remove(salary);
            await _context.SaveChangesAsync();

            return Ok(salary);
        }

        private bool SalaryExists(int id)
        {
            return _context.Salary.Any(e => e.Id == id);
        }
    }
}