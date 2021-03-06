using Lab1.Models;
using System;
using System.Collections.Generic;
using System.IO;

namespace Lab1.Repository
{
    class EmployeeRepository : IEmployeeRepository
    {
        private const string databasePath = "employees.csv";

        public bool Add(Employee employee)
        {
            List<Employee> employees = GetAll();
            StreamWriter streamWriter = File.AppendText(databasePath);
            bool isAdded = Add(employee, employees, streamWriter);
            streamWriter.Close();
            return isAdded;
        }

        private bool Add(Employee employee, List<Employee> existingEmployees, StreamWriter streamWriter)
        {
            if (!employee.IsValid() || existingEmployees.Find((e) => e.IsEqualTo(employee)) != null)
            {
                return false;
            }
            streamWriter.WriteLine(employee.ToString());
            return true;
        }

        private void AddAll(List<Employee> employees)
        {
            List<Employee> allEmployees = GetAll();
            StreamWriter streamWriter = File.AppendText(databasePath);
            foreach (var employee in employees)
            {
                Add(employee, allEmployees, streamWriter);
            }
            streamWriter.Close();
        }

        public void Delete(Employee employee)
        {
            List<Employee> employeesToDelete = Find(employee);
            List<Employee> employees = GetAll();
            employees.RemoveAll((e) => employeesToDelete
                                            .Exists((emp) => emp.IsEqualTo(e)));
            File.Delete(databasePath);
            AddAll(employees);
        }

        public List<Employee> Find(Employee employee)
        {
            List<Employee> allEmployees = GetAll();
            return allEmployees.FindAll((e) => e.IsCompatibleWith(employee));
        }

        public List<Employee> GetAll()
        {
            List<Employee> employees = new List<Employee>();
            List<string> lines = GetAllLines();
            foreach (var line in lines)
            {
                employees.Add(new Employee(line));
            }
            return employees;
        }

        public void Edit(Employee oldParameters, Employee newParameters)
        {
            List<Employee> employeesFound = Find(oldParameters);
            List<Employee> allEmployees = GetAll();
            if (employeesFound.Count == 1)
            {
                Employee employee = allEmployees.Find((e) => e.IsEqualTo(employeesFound[0]));
                employee.Id = newParameters.Id;
                employee.FirstName = newParameters.FirstName;
                employee.SecondName = newParameters.SecondName;
                employee.Position = newParameters.Position;
                employee.Address = newParameters.Address;
                File.Delete(databasePath);
                AddAll(allEmployees);
            }
        }

        private List<string> GetAllLines()
        {
            List<string> lines = new List<string>();
            if (File.Exists(databasePath))
            {
                var streamReader = new StreamReader(databasePath);
                string buffer;
                while ((buffer = streamReader.ReadLine()) != null)
                {
                    lines.Add(buffer);
                }
                streamReader.Close();
            }
            return lines;
        }
    }
}
