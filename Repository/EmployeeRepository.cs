using Lab1.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Lab1.Repository
{
    class EmployeeRepository : IEmployeeRepository
    {
        private const string databasePath = "employees.csv";

        public bool Add(Employee employee)
        {
            var employees = GetAll();
            var streamWriter = File.AppendText(databasePath);
            var isAdded = Add(employee, employees, streamWriter);
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
            var allEmployees = GetAll();
            var streamWriter = File.AppendText(databasePath);
            foreach (var employee in employees)
            {
                Add(employee, allEmployees, streamWriter);
            }
            streamWriter.Close();
        }

        public void Delete(Employee employee)
        {
            var employeesToDelete = Find(employee);
            var employees = GetAll();
            employees.RemoveAll((e) => employeesToDelete
                                            .Exists((emp) => emp.IsEqualTo(e)));
            File.Delete(databasePath);
            AddAll(employees);
        }

        public List<Employee> Find(Employee employee)
        {
            var allEmployees = GetAll();
            return allEmployees.FindAll((e) => e.IsCompatibleWith(employee));
        }

        public List<Employee> GetAll()
        {
            var lines = GetAllLines();
            return lines.Select(line => new Employee(line)).ToList();
        }

        public void Edit(Employee oldParameters, Employee newParameters)
        {
            var employeesFound = Find(oldParameters);
            var allEmployees = GetAll();
            if (employeesFound.Count == 1)
            {
                var employee = allEmployees
                    .Find((e) => e.IsEqualTo(employeesFound[0]));
                if (employee != null)
                {
                    employee.Id = newParameters.Id;
                    employee.FirstName = newParameters.FirstName;
                    employee.SecondName = newParameters.SecondName;
                    employee.Position = newParameters.Position;
                    employee.Address = newParameters.Address;
                }

                File.Delete(databasePath);
                AddAll(allEmployees);
            }
        }

        private List<string> GetAllLines()
        {
            var lines = new List<string>();
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
