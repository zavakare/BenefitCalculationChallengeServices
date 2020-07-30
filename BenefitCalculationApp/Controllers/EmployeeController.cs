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
    [Route("api/Employee")]
    public class EmployeeController : Controller
    {
        private readonly EmployeeContext _employeeContext;

        public EmployeeController(EmployeeContext context)
        {
            _employeeContext = context;
        }

        // GET: api/Employee
        [HttpGet]
        public List<TestClass> GetEmployee()
        {
            var dependents = (from emp in _employeeContext.Employee
                              join dep in _employeeContext.Dependent on emp.Id equals dep.EmployeeId
                              select new Dependent
                              {
                                  EmployeeId = dep.EmployeeId,
                                  FirstName = dep.FirstName,
                                  LastName = dep.LastName
                              }).ToList();
            var testdata = new List<TestClass>();
            //make repo call -- move this to repo file
            var result =
                from emp in _employeeContext.Employee
                join sal in _employeeContext.Salary on emp.SalaryId equals sal.Id
                select new TestClass
                {
                    Id = emp.Id,
                    FirstName = emp.FirstName,
                    LastName = emp.LastName,
                    PayCheckAmount = sal.PaycheckAmount,
                    TotalPayChecks = sal.TotalPaychecks,
                    Dependents = GetDependents(emp.Id, dependents)
                };

            testdata = result.ToList();
            return testdata;
        }

        // GET: api/Employee/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployee([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var employee = await _employeeContext.Employee.SingleOrDefaultAsync(m => m.Id == id);

            if (employee == null)
            {
                return NotFound();
            }

            return Ok(employee);
        }

        // PUT: api/Employee/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployee([FromRoute] int id, [FromBody] Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != employee.Id)
            {
                return BadRequest();
            }

            _employeeContext.Entry(employee).State = EntityState.Modified;

            try
            {
                await _employeeContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(id))
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

        // POST: api/Employee
        [HttpPost]
        public async Task<IActionResult> PostEmployee([FromBody] TestClass employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var salaryToEnter = new Salary
            {
                PaycheckAmount = employee.PayCheckAmount,
                TotalPaychecks = employee.TotalPayChecks,

            };
            _employeeContext.Salary.Add(salaryToEnter);
            await _employeeContext.SaveChangesAsync();

            var employeeToEnter = new Employee
            {
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                SalaryId = salaryToEnter.Id
            };

            _employeeContext.Employee.Add(employeeToEnter);
            await _employeeContext.SaveChangesAsync();

            var dependentsToEnter = new List<Dependent>();
            var dependent = new Dependent();
            employee.Dependents.ForEach(x =>
            {
                dependent = new Dependent();
                dependent.FirstName = x.FirstName;
                dependent.LastName = x.LastName;
                dependent.EmployeeId = employeeToEnter.Id;

                dependentsToEnter.Add(dependent);
            });

            _employeeContext.Dependent.AddRange(dependentsToEnter);
            await _employeeContext.SaveChangesAsync();

            return CreatedAtAction("GetEmployee", new { id = employeeToEnter.Id }, employee);
        }

        // DELETE: api/Employee/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var employee = await _employeeContext.Employee.SingleOrDefaultAsync(m => m.Id == id);
            if (employee == null)
            {
                return NotFound();
            }

            _employeeContext.Employee.Remove(employee);
            await _employeeContext.SaveChangesAsync();

            return Ok(employee);
        }

        private bool EmployeeExists(int id)
        {
            return _employeeContext.Employee.Any(e => e.Id == id);
        }

        private List<Person> GetDependents(int employeeId, List<Dependent> dependents)
        {
            var dependentNames = new List<Person>();
            foreach (var dep in dependents)
            {
                if (dep.EmployeeId == employeeId)
                {
                    dependentNames.Add(new Person
                    {
                        FirstName = dep.FirstName,
                        LastName = dep.LastName
                    });
                }
            }
            return dependentNames;

        }
    }
}