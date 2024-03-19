using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using TimeSheetLib;

namespace PayrollLib
{
    public interface IEmployee : IPerson
    {
        decimal CalculatePay(double hours);
        string Password { get; set; }
    }
}
