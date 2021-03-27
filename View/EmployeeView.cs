using Lab1.Controllers;
using Lab1.Models;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Bogus;

namespace Lab1.View
{
    public partial class EmployeeView : Form
    {
        private readonly IEmployeeController _employeeController;
        private Employee _currentEmployee;

        public EmployeeView(IEmployeeController employeeController)
        {
            InitializeComponent();
            List<string> enumNames = new List<string>(Enum.GetNames(typeof(Position)));
            enumNames.Insert(0, "Not chosen");
            positionComboBox.DataSource = enumNames;
            _employeeController = employeeController;
            UpdateGrid();
        }

        private void UpdateGrid()
        {
            employeesGrid.DataSource = _employeeController.GetAll();
        }

        private void findButton_Click(object sender, EventArgs e)
        {
            UpdateCurrentEmployee();
            List<Employee> foundEmployees = _employeeController.Find(_currentEmployee);
            employeesGrid.DataSource = foundEmployees;
        }
        private Employee ParseFields()
        {
            string id = idTextBox.Text;
            string firstName = firstNameTextBox.Text;
            string secondName = secondNameTextBox.Text;
            string position = positionComboBox.Text;
            string address = addressTextBox.Text;
            return new Employee(id, firstName, secondName,
                position, address);
        }

        private void UpdateCurrentEmployee()
        {
            _currentEmployee = ParseFields();
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            UpdateCurrentEmployee();
            _employeeController.Add(_currentEmployee);
            UpdateGrid();
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            UpdateCurrentEmployee();
            _employeeController.Delete(_currentEmployee);
            UpdateGrid();
        }

        private void editButton_Click(object sender, EventArgs e)
        {
            _employeeController.Edit(_currentEmployee, ParseFields());
            UpdateCurrentEmployee();
            UpdateGrid();
        }

        private void employeesGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }
            DataGridView dataGrid = (DataGridView)sender;
            var row = dataGrid.Rows[e.RowIndex];

            idTextBox.Text = row.Cells[0].Value.ToString();
            firstNameTextBox.Text = row.Cells[1].Value.ToString();
            secondNameTextBox.Text = row.Cells[2].Value.ToString();
            positionComboBox.Text = row.Cells[3].Value.ToString();
            addressTextBox.Text = row.Cells[4].Value.ToString();
            UpdateCurrentEmployee();
        }

        private void EmployeeView_Activated(object sender, EventArgs e)
        {
            UpdateGrid();
        }

        private void fakeInfoButton_Click(object sender, EventArgs e)
        {
            Employee employee = new Employee();
            for (int i = 1; i <= 100; i++)
            {
                Faker faker = new Faker();
                Person person = faker.Person;
                employee.Id = i;
                employee.FirstName = person.FirstName;
                employee.SecondName = person.LastName;
                employee.Position = (Position)(i % 3);
                var address = person.Address;
                employee.Address = string.Join(", ", address.Street, 
                    address.City, address.ZipCode, address.State);
                _employeeController.Add(employee);
                UpdateGrid();
            }
        }
    }
}
