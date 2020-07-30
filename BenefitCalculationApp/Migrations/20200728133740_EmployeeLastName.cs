using Microsoft.EntityFrameworkCore.Migrations;

namespace BenefitCalculationApp.Migrations
{
    public partial class EmployeeLastName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Employee",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Employee");
        }
    }
}
