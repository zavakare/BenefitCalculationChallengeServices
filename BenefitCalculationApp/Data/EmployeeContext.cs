
using Microsoft.EntityFrameworkCore;
using BenefitCalculationApp.Models;

    public class EmployeeContext : DbContext
    {
        public EmployeeContext (DbContextOptions<EmployeeContext> options)
            : base(options)
        {
        }

        public DbSet<Employee> Employee { get; set; }

    public DbSet<Salary> Salary { get; set; }

    public DbSet<Dependent> Dependent { get; set; }

    public DbSet<BenefitCalculationApp.Models.Benefit> Benefit { get; set; }

}
