using PersonLib.SaveData;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeSheetLib;

namespace PayrollLib
{
    public class Payroll
    {
        public Dictionary<string, IEmployee> Employees { get; set; }
        public TimeSheet TimeSheet { get; set; }
        public List<PayStub> PayStubs { get; set; }

        public Payroll()
        {
            TimeSheet = (new JsonReadSave()).LoadTimeSheet();
            Employees = new Dictionary<string, IEmployee>();
            PayStubs = new List<PayStub>();
        }
        public void SaveTimesheet()
        {
            (new JsonReadSave()).SaveTimeSheet(TimeSheet);
        }
        public void ProcessPayroll(DateTime BeginDate, DateTime EndDate)
        {
            PayStubs = new List<PayStub>();
            // for every employee, calculate pay by looping through the time sheet and clock for
            // that employee on a day and subtracting the clock in times from the clock out times

            foreach (KeyValuePair<string, List<TimeClock>> employeeClocks in TimeSheet.Clocks)
            {
                // get the employee from the dictionary using the employee id as the key
                IEmployee employee = Employees[employeeClocks.Key];

                // get the clocks for the employee
                IEnumerable<TimeClock> clocks = employeeClocks.Value;

                // group the clocks by date between the range of dates passed in to the method
                IEnumerable<IGrouping<DateTime, TimeClock>> days = clocks
                    .Where(c => c.Time.Date >= BeginDate.Date && c.Time.Date <= EndDate.Date)
                    .GroupBy(c => c.Time.Date);

                double totalHoursWorked = GetTotalHoursWorked(employee, days);

                var stub = new PayStub();
                var pay = employee.CalculatePay(totalHoursWorked);
                stub.EmployeeFirstName = employee.FirstName;
                stub.EmployeeLastName = employee.LastName;
                stub.GrossPay = pay;
                stub.PayPeriodStart = days.First().Key;
                stub.PayPeriodEnd = days.Last().Key;

                PayStubs.Add(stub);

                Console.WriteLine($"Employee {employee.FirstName} {employee.LastName} worked {totalHoursWorked} hours between {days.First().Key.ToShortDateString()} and {days.Last().Key.ToShortDateString()} and earned {pay:C}");
            }
        }
        private double GetTotalHoursWorked(IEmployee employee, IEnumerable<IGrouping<DateTime, TimeClock>> days)
        {
            double totalHoursWorked = 0;

            foreach (IGrouping<DateTime, TimeClock> day in days)
            {

                totalHoursWorked += GetHoursFromDay(day);
            }
            return totalHoursWorked;
        }
        private double GetHoursFromDay(IGrouping<DateTime, TimeClock> day)
        {
            double totalHoursWorked = 0;
            var clockInEvents = day.Where(c => c.ClockInOut == ClockInOut.In).OrderBy(c => c.Time).ToList();
            var clockOutEvents = day.Where(c => c.ClockInOut == ClockInOut.Out).OrderBy(c => c.Time).ToList();

            if (clockInEvents.Count != clockOutEvents.Count)
            {
                throw new InvalidOperationException("Mismatched clock-in and clock-out events.");
            }

            for (int i = 0; i < clockInEvents.Count; i++)
            {
                var clockInTime = clockInEvents[i].Time;
                var clockOutTime = clockOutEvents[i].Time;

                if (clockOutTime <= clockInTime)
                {
                    throw new InvalidOperationException($"Clock-out event at {clockOutTime} occurred before or at the same time as the clock-in event at {clockInTime}.");
                }

                var hoursWorked = clockOutTime.Subtract(clockInTime).TotalHours;
                totalHoursWorked += hoursWorked;
            }
            return totalHoursWorked;
        }
    }
}
