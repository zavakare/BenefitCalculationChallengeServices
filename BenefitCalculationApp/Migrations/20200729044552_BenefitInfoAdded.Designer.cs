﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace BenefitCalculationApp.Migrations
{
    [DbContext(typeof(EmployeeContext))]
    [Migration("20200729044552_BenefitInfoAdded")]
    partial class BenefitInfoAdded
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.2-rtm-10011")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BenefitCalculationApp.Models.Benefit", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<double>("CostPerDependent");

                    b.Property<double>("CostPerYear");

                    b.Property<int>("Discount");

                    b.HasKey("Id");

                    b.ToTable("Benefit");
                });

            modelBuilder.Entity("BenefitCalculationApp.Models.Dependent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("EmployeeId");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.HasKey("Id");

                    b.ToTable("Dependent");
                });

            modelBuilder.Entity("BenefitCalculationApp.Models.Employee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<int>("SalaryId");

                    b.HasKey("Id");

                    b.ToTable("Employee");
                });

            modelBuilder.Entity("BenefitCalculationApp.Models.Salary", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<double>("PaycheckAmount");

                    b.Property<int>("TotalPaychecks");

                    b.HasKey("Id");

                    b.ToTable("Salary");
                });
#pragma warning restore 612, 618
        }
    }
}