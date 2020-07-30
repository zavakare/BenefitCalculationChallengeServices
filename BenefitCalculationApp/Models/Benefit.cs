using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BenefitCalculationApp.Models
{
    public class Benefit
    {
        public int Id { get; set; }
        public double CostPerYear { get; set; }
        public double CostPerDependent { get; set; } //per year
        public int Discount { get; set; } //Names that begin with A
    }
}
