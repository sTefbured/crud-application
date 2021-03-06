using Lab1.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lab1.Controllers
{
    public interface IEmployeeController
    {
        public void Add(Employee employee);
        public void Delete(Employee employee);
        public List<Employee> Find(Employee employee);
        public void Edit(Employee oldParameters, Employee newParameters);
        public List<Employee> GetAll();
    }
}
