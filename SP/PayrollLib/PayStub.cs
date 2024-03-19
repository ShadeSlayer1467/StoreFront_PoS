using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollLib
{
    public struct PayStub
    {
        public string EmployeeID { get; set; }
        public string EmployeeFirstName { get; set; }
        public string EmployeeLastName { get; set; }
        public DateTime PayPeriodStart { get; set; }
        public DateTime PayPeriodEnd { get; set; }
        public decimal HoursWorked { get; set; }
        public decimal GrossPay { get; set; }
        public decimal NetPay { get; set; }
        public decimal Taxes { get; set; }
        public decimal Deductions { get; set; }
        public decimal OtherDeductions { get; set; }
        public decimal OtherEarnings { get; set; }
        public decimal TotalDeductions { get; set; }
        public decimal TotalEarnings { get; set; }
        public decimal TotalHours { get; set; }
        public decimal TotalPay { get; set; }
        public decimal TotalTaxes { get; set; }
        public decimal TotalNetPay { get; set; }
        public decimal TotalGrossPay { get; set; }
        public decimal TotalOtherDeductions { get; set; }
        public decimal TotalOtherEarnings { get; set; }
        public decimal TotalOtherTaxes { get; set; }
        public decimal TotalOtherNetPay { get; set; }
        public decimal TotalOtherGrossPay { get; set; }
        public decimal TotalOtherHours { get; set; }
        public decimal TotalOtherPay { get; set; }

    }
}
