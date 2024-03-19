using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace PayrollLib
{
    public class SalaryEmployee : IEmployee
    {
        public string ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal Salary { get; set; }
        public string Password { get; set; }

        public SalaryEmployee(string FirstName, string LastName, decimal Salary)
        {
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.Salary = Salary;
        }

        public decimal CalculatePay(double hoursWorked)
        {
            return Salary / 12;
        }
    }
}
