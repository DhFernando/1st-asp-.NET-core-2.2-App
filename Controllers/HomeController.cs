using EmployeeManagementSystem.Models;
using EmployeeManagementSystem.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Controllers 
{
    [Route("[controller]")]
    public class HomeController : Controller
    {
        IEmployeeRepository _employeeRepository;
        public HomeController(IEmployeeRepository employeeRepository)
        {
            this._employeeRepository = employeeRepository;
        }

        [Route("")]
        [Route("[action]")]
        [Route("~/")]
        public ViewResult Index()
        {
            return View(_employeeRepository.GetAllEmployees()) ;
        }

        [Route("[action]/{id?}")]
        public ViewResult EmployeeDetails(int? id)
        {
            HomeGetEmployeeViewModel homeGetEmployeeViewModel = new HomeGetEmployeeViewModel() {
                   Employee = _employeeRepository.GetEmployee(id??1),
                   Title = "Employee Details"
            };
            
            return View(homeGetEmployeeViewModel);
        }
    }
}
