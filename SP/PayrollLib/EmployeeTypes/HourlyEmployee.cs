using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using TimeSheetLib;

namespace PayrollLib
{
    public class HourlyEmployee : IEmployee
    {
        public string ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal HourlyRate { get; set; }
        public string Password {get; set; }

        public HourlyEmployee() { }
        public HourlyEmployee(string FirstName, string LastName, decimal HourlyRate)
        {
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.HourlyRate = HourlyRate;
        }
        public decimal CalculatePay(double hoursWorked)
        {
            return (HourlyRate * (decimal)hoursWorked);
        }
    }
}
