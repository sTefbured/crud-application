using Lab1.Models;
using System.Collections.Generic;

namespace Lab1.Repository
{
    public interface IEmployeeRepository
    {
        public List<Employee> Find(Employee employee);

        public void Edit(Employee oldParameters, Employee newParameters);

        public void Delete(Employee employee);

        public bool Add(Employee employee);

        public List<Employee> GetAll();
    }
}
