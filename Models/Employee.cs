using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Models
{
    public class Employee
    {
        public int Id { get; set; }
        [Required]
        public String Name { get; set; }
        [Required]
        public String Email { get; set; }
        public Dept? Department { get; set; }
    }
}
