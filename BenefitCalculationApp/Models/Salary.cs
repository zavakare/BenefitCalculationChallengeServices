using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BenefitCalculationApp.Models
{
    public class Salary
    {
        public int Id { get; set; }
        public double PaycheckAmount { get; set; }
        public int TotalPaychecks { get; set; }
    }
}
