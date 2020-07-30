using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BenefitCalculationApp.Models
{
    public class EmployeeInfo
    {
        public Person Employee { get; set; }
        public Person[] Dependents { get; set; }
        public Salary Salary { get; set; }
    }

    public class Person
    {
        public string FirstName;
        public string LastName;
    }
}
