using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BenefitCalculationApp.Models
{
    public class TestClass
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public double PayCheckAmount { get; set; }
        public int TotalPayChecks { get; set; }
        public List<Person> Dependents { get; set; }
    }
}
