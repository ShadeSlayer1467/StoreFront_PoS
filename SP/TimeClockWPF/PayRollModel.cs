using PayrollLib.SaveEmployeeData;
using PersonLib.SaveData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using TimeSheetLib;

namespace TimeCLockWPF
{
    public class PayRollModel
    {
        private PayrollLib.Payroll payroll = new PayrollLib.Payroll();
        public PayRollModel()
        {
            LoadEmployees();
        }
        public bool IsClockedIn(string employeeId) => payroll.TimeSheet.IsClockedIn(employeeId);
        private void LoadEmployees()
        {
            var readSaveEmployeeData = new JsonReadSaveEmployeeData();
            var list = readSaveEmployeeData.LoadEmployeeData();
            foreach (var item in list)
            {
                payroll.Employees.Add(item.ID, item);
            }
        }
        public void SaveEmployees()
        {
            var readSaveEmployeeData = new JsonReadSaveEmployeeData();
            readSaveEmployeeData.SaveEmployeeData(payroll.Employees.Values.ToList());
        }
        public void SaveTimeSheet() => payroll.SaveTimesheet();
        public void ChangeClock(string EmployeeId, DateTime time)
        {
            if (IsClockedIn(EmployeeId)) ClockOut(EmployeeId, time);
            else ClockIn(EmployeeId, time);
        }
        public bool CheckLogin(string id, string ss)
        {
            try
            {
                return (payroll.Employees[id].Password == ss);
            }
            catch (Exception ex) { return false; }
        }
        public bool UpdatePassword(string ID, string pass)
        {
            try
            {
                payroll.Employees[ID].Password = pass;
                SaveEmployees();
                return true;
            }
            catch (Exception ex) { return false; }
        }
        public void ClockIn(string ID, DateTime time)
        {
            IPerson person = payroll.Employees[ID];
            if (person == null) return;
            payroll.TimeSheet.AddClock(person, ClockInOut.In, time);
        }
        public void ClockOut(string ID, DateTime time)
        {
            IPerson person = payroll.Employees[ID];
            if (person == null) return;
            payroll.TimeSheet.AddClock(person, ClockInOut.Out, time);
        }
        public bool DoesIdExist(string ID)
        {
            return payroll.Employees.ContainsKey(ID);
        }
    }
}
