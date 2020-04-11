using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Models
{
    public class MockDepartmentRepository : IDepartmentRepository
    {
        private List<Department> _departmentList;

        public MockDepartmentRepository()
        {
            _departmentList = new List<Department>()
            {
                new Department() { Id = 1 , Name = "Dilshan"},
                new Department() { Id = 2 , Name = "Hasitha" },
                new Department() { Id = 3 , Name = "Fernando"}
            };
        }

        public IEnumerable<Department> GetAllDepartment()
        {
            return this._departmentList;
        }

    }
}
