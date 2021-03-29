using Lab1.Models;
using Lab1.Repository;
using System.Collections.Generic;
using Lab1.Attribute;

namespace Lab1.Controllers
{
    public class EmployeeController : IEmployeeController
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        [Security(UserRole.ADMIN, UserRole.USER)]
        public void Add(Employee employee)
        {
            _employeeRepository.Add(employee);
        }

        [Security(UserRole.ADMIN)]
        public void Delete(Employee employee)
        {
            _employeeRepository.Delete(employee);
        }

        [Security(UserRole.ADMIN, UserRole.USER)]
        public void Edit(Employee oldParameters, Employee newParameters)
        {
            _employeeRepository.Edit(oldParameters, newParameters);
        }

        [Security(UserRole.GUEST, UserRole.ADMIN, UserRole.USER)]
        public List<Employee> Find(Employee employee)
        {
            return _employeeRepository.Find(employee);
        }

        [Security(UserRole.GUEST, UserRole.ADMIN, UserRole.USER)]
        public List<Employee> GetAll()
        {
            return _employeeRepository.GetAll();
        }
    }
}
