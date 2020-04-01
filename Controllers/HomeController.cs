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

        private string ProcessUploadedFile(EmployeeCreateViewModel model)
        {
            String uniquFileName = null;
            if (model.PhotoPath != null)
            {
                String uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "Images");
                uniquFileName = Guid.NewGuid().ToString() + "_" + model.PhotoPath.FileName;
                String filePath = Path.Combine(uploadsFolder, uniquFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.PhotoPath.CopyTo(fileStream);
                }

            }

            return uniquFileName;
        }


        [HttpGet]
        public ViewResult Edit(int id)
        {
            Employee employee = _employeeRepository.GetEmployee(id);
            EmployeeEditViewModel employeeEditViewModel = new EmployeeEditViewModel
            {
                Id = employee.Id,
                Name = employee.Name,
                Email = employee.Email,
                Department = employee.Department,
                ExistingPhotoPath = employee.PhotoPath      
               
            };
            return View(employeeEditViewModel);
        }

        [HttpPost]
        public IActionResult Edit(EmployeeEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                Employee employee = _employeeRepository.GetEmployee(model.Id);
                employee.Name = model.Name;
                employee.Email = model.Email;
                employee.Department = model.Department;
                if(model.PhotoPath != null)
                {
                    if (model.ExistingPhotoPath != null)
                    {
                        String filePath = Path.Combine(hostingEnvironment.WebRootPath, "images", model.ExistingPhotoPath);
                        System.IO.File.Delete(filePath);
                    }
                    employee.PhotoPath = ProcessUploadedFile(model);
                }
                
                _employeeRepository.Update(employee);

                return RedirectToAction("Index");
            }
            return View();

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
                string uniquFileName = ProcessUploadedFile(model);

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
