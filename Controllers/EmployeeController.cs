using Lab1.Models;
using Lab1.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lab1.Controllers
{
    class EmployeeController : IEmployeeController
    {
        private IEmployeeRepository _employeeRepository;

        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public void Add(Employee employee)
        {
            _employeeRepository.Add(employee);
        }

        public void Delete(Employee employee)
        {
            _employeeRepository.Delete(employee);
        }

        public void Edit(Employee oldParameters, Employee newParameters)
        {
            _employeeRepository.Edit(oldParameters, newParameters);
        }

        public List<Employee> Find(Employee employee)
        {
            return _employeeRepository.Find(employee);
        }

        public List<Employee> GetAll()
        {
            return _employeeRepository.GetAll();
        }
    }
}
