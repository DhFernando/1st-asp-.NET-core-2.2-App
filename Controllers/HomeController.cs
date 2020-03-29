using EmployeeManagementSystem.Models;
using EmployeeManagementSystem.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Controllers 
{
    
    public class HomeController : Controller
    {
        IEmployeeRepository _employeeRepository;
        private readonly IHostingEnvironment hostingEnvironment;
       
        public HomeController(IEmployeeRepository employeeRepository ,IHostingEnvironment hostingEnvironment)
        {
            this._employeeRepository = employeeRepository;
            this.hostingEnvironment = hostingEnvironment;
            
        }

        
        public ViewResult Index()
        {
            return View(_employeeRepository.GetAllEmployees()) ;
        }

        public ViewResult EmployeeDetails(int? id)
        {
            HomeGetEmployeeViewModel homeGetEmployeeViewModel = new HomeGetEmployeeViewModel() {
                   Employee = _employeeRepository.GetEmployee(id ?? 1),
                   Title = "Employee Details"
            };
            
            return View(homeGetEmployeeViewModel);
        }
        [HttpGet]
       
        public ViewResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(EmployeeCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                String uniquFileName = null;
                if (model.PhotoPath != null)
                {
                    String uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "Images");
                    uniquFileName = Guid.NewGuid().ToString()+ "_" + model.PhotoPath.FileName;
                    String filePath = Path.Combine(uploadsFolder, uniquFileName);
                    model.PhotoPath.CopyTo(new FileStream(filePath, FileMode.Create));
                }

                Employee newEmployee = new Employee
                {
                    Name = model.Name,
                    Email = model.Email,
                    Department = model.Department,
                    PhotoPath = uniquFileName
                };

                _employeeRepository.Add(newEmployee);
                
                return RedirectToAction("EmployeeDetails", new { id = newEmployee.Id });
            }
            return View();
            
        }
    }
}
