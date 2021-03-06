using System;
using System.Collections.Generic;

namespace Lab1.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public Position? Position { get; set; }
        public string Address { get; set; }

        public bool IsValid()
        {
            return (Id > 0) && (!string.IsNullOrEmpty(FirstName)) && (!string.IsNullOrEmpty(SecondName))
                && (Position != null) && (!string.IsNullOrEmpty(Address));
        }

        public Employee() { }

        public Employee(string csvLine) : this(CsvHelper.Split(csvLine))
        { }

        public Employee(List<string> parameters) : this(parameters[0], parameters[1],
                parameters[2], parameters[3], parameters[4])
        { }

        public Employee(string idStr,
                        string firstNameStr,
                        string secondNameStr,
                        string positionStr,
                        string addressStr)
        {
            Id = 0;
            if (idStr != "")
            {
                Id = int.Parse(idStr);
            }
            FirstName = firstNameStr;
            SecondName = secondNameStr;
            Position = null;
            if (positionStr != "Not chosen" && positionStr != "")
            {
                Position = (Position)Enum.Parse(typeof(Position), positionStr);
            }
            Address = addressStr;
        }

        public Employee(int id,
                        string firstName,
                        string secondName,
                        Position? position,
                        string address)
        {
            Id = id;
            FirstName = firstName;
            SecondName = secondName;
            Position = position;
            Address = address;
        }

        public bool IsCompatibleWith(object obj)
        {
            Employee employee = (Employee)obj;
            bool equalId = (Id == employee.Id) || (employee.Id == 0);
            bool equalFirstName = (FirstName == employee.FirstName) || (employee.FirstName == "");
            bool equalSecondName = (SecondName == employee.SecondName) || (employee.SecondName == "");
            bool equalPosition = (Position == employee.Position) || (employee.Position == null);
            bool equalAddress = (Address == employee.Address) || (employee.Address == "");
            return equalId && equalFirstName && equalSecondName && equalPosition && equalAddress;
        }

        public bool IsEqualTo(object obj)
        {
            Employee employee = (Employee)obj;
            bool equalId = (Id == employee.Id);
            bool equalFirstName = (FirstName == employee.FirstName);
            bool equalSecondName = (SecondName == employee.SecondName);
            bool equalPosition = (Position == employee.Position);
            bool equalAddress = (Address == employee.Address);
            return equalId || equalFirstName && equalSecondName && equalPosition
                 && equalAddress;
        }

        public override string ToString()
        {
            return CsvHelper.Join(Id, FirstName, SecondName, Position, Address);
        }
    }
}
