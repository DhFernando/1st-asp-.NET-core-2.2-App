using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.ViewModels
{
    public class EditUserViewModel
    {

        public EditUserViewModel()
        {
            Claims = new List<String>();
            Roles = new List<String>();
        }

        public String Id { get; set; }
        [Required]
        public String UserName { get; set; }
        public String City { get; set; }
        [Required][EmailAddress]
        public String Email { get; set; }
        public List<String> Claims { get; set; }
        public IList<String> Roles { get; set; }


    }
}
