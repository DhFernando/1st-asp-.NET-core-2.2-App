using EmployeeManagementSystem.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.ViewModels
{
    public class EmployeeCreateViewModel
    {
        public int Id { get; set; }
        [Required]
        public String Name { get; set; }
        [Required]
        public String Email { get; set; }
        public Dept? Department { get; set; }
        public IFormFile PhotoPath { get; set; }
    }
}
