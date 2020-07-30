using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BenefitCalculationApp.Models
{
    public class Employees
    {
        public EmployeeInfo[] EmployeeInfo { get; set; }
        public BenefitInfo Benefit { get; set; }
    }

    public class BenefitInfo
    {
        public int CostPerYear { get; set; }
        public int DependentCost { get; set; } //per year
        public int Discount { get; set; } //Names that start with A
    }
}
