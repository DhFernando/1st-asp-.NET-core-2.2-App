using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Models
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasData(
                  new Employee { Id = 3, Department = Dept.Accounting, Name = "Hasitha", Email = "hasitha@gmail.com" },
                  new Employee { Id = 4, Department = Dept.IT, Name = "Fernando", Email = "Fernando@gmail.com" }
              );
        }
    }
}
