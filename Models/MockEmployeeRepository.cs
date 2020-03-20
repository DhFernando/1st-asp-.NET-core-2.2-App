using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Models
{
    public class MockEmployeeRepository : IEmployeeRepository
    {
        private List<Employee> _employeeList;

        public MockEmployeeRepository()
        {
            _employeeList = new List<Employee>()
            {
                new Employee() { Id = 1 , Name = "Dilshan" , Department = "HR" , Email = "dilshan@gmail.com"},
                new Employee() { Id = 2 , Name = "Hasitha" , Department = "CS" , Email = "hasitha@gmail.com"},
                new Employee() { Id = 3 , Name = "Fernando" , Department = "CS" , Email = "fernando@gmail.com"}
            };
        }
        public Employee GetEmployee(int Id)
        {
            return _employeeList.FirstOrDefault(e=>e.Id==Id);
        }
    }
}
