using EmployeeManagementSystem.Models;
using EmployeeManagementSystem.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Controllers 
{
    public class HomeController : Controller
    {
        IEmployeeRepository _employeeRepository;
        public HomeController(IEmployeeRepository employeeRepository)
        {
            this._employeeRepository = employeeRepository;
        }
        public ViewResult Index()
        {
            return View(_employeeRepository.GetAllEmployees()) ;
        }
        public ViewResult EmployeeDetails()
        {
            HomeGetEmployeeViewModel homeGetEmployeeViewModel = new HomeGetEmployeeViewModel() {
                   Employee = _employeeRepository.GetEmployee(1),
                   Title = "Employee Details"
            };
            
            return View(homeGetEmployeeViewModel);
        }
    }
}
