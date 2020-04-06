using EmployeeManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.ViewModels
{
    public class EditRoleViewModel
    {   
        public EditRoleViewModel()
        {
            Users = new List<ApplicationUser>();
        }
        public String Id { get; set; }
        [Required(ErrorMessage ="Role Name is Required")]
        public String RoleName { get; set; }

        public List<ApplicationUser> Users { get; set; }

    }
}
