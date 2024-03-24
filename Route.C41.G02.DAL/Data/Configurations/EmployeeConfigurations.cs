using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Route.C41.G02.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.C41.G02.DAL.Data.Configurations
{
    internal class EmployeeConfigurations : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.Property(E=> E.Name).HasColumnType("varchar").HasMaxLength(50).IsRequired();
            builder.Property(E => E.Address).IsRequired();
            builder.Property(E => E.Salary).HasColumnType("decimal(12,2)");
            builder.Property(E => E.Gander).HasConversion(
                (Gander) => Gander.ToString(),                          // How Enum will store in DB
                (Gander)=> (Gander)Enum.Parse(typeof(Gander), Gander)  // How Enum will returned in app
                ) ;
            builder.Property(E => E.EmployeeType).HasConversion(
                (EmpType) => EmpType.ToString(),                             // How Enum will store in DB
                (EmpType) => (EmpType)Enum.Parse(typeof(EmpType), EmpType)  // How Enum will returned in app
                );
        }
    }
}
