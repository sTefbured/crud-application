using Lab1.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lab1.Repository
{
    interface IEmployeeRepository
    {
        public List<Employee> Find(Employee employee);

        public void Edit(Employee oldParameters, Employee newParameters);

        public void Delete(Employee employee);

        public bool Add(Employee employee);

        public List<Employee> GetAll();
    }
}
