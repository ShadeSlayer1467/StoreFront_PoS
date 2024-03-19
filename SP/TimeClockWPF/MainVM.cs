using PayrollLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TimeCLockWPF;
using TimeSheetLib;

namespace TimeClockWPF
{
    public class MainVM : INotifyPropertyChanged
    {
        private readonly PayRollModel TimeSheetModel = new PayRollModel();
        #region INotify
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region Properties
        private string empID;
        public string EmployeeId
        {
            get { return empID; }
            set { empID = value; NotifyPropertyChanged(); }
        }
        private string date;
        public string Date
        {
            get { return date; }
            set { date = value; NotifyPropertyChanged(); }
        }
        private string time;
        public string Time
        {
            get { return time; }
            set { time = value; NotifyPropertyChanged(); }
        }
        private string clockButtonContent;
        public string ClockButtonContent
        {
            get { return clockButtonContent; }
            set { clockButtonContent = value; NotifyPropertyChanged(); }
        }
        private bool isEmployeeIdValid;
        public bool IsChangePasswordButtonEnabled { get => isEmployeeIdValid; set => SetProperty(ref isEmployeeIdValid, value); }
        protected bool SetProperty<T>(ref T field, T newValue, [CallerMemberName] string propertyName = null)
        {
            if (!Equals(field, newValue))
            {
                field = newValue;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
                return true;
            }

            return false;
        }
        private string password;
        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        bool IsClockedIn { get; set; }
        #endregion

        public ICommand ChangePasswordCommand { get; set; }
        public ICommand ClockCommand { get; set; }
        public ICommand IDLostFocus { get; set; }
        public ICommand PasswordEnterPressCommand { get; set; }
        

        public delegate void MainVMDel();
        public MainVMDel CloseFunc { get; set; }
        public delegate bool MainVMDelBool();
        public MainVMDelBool PasswordFocus { get; set; }

        public MainVM()
        {
            IsClockedIn = false; IsChangePasswordButtonEnabled = false;
            Date = DateTime.Now.ToShortDateString();
            Time = DateTime.Now.ToShortTimeString();
            ClockButtonContent = "Clock In";
            ChangePasswordCommand = new RelayCommand(ChangePassword);
            ClockCommand = new RelayCommand(ChangeClock);
            IDLostFocus = new RelayCommand(CheckIfIdExists);
            Password = "";
        }
        public void CheckIfIdExists()
        {
            IsChangePasswordButtonEnabled = false;

            if (!TimeSheetModel.DoesIdExist(EmployeeId))
            {
                // Show error message in dialog box popup
                MessageBox.Show("Employee ID does not exist.", "Invalid Employee ID", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (CheckIfCLockedIn()) ClockButtonContent = "Clock Out";
            else ClockButtonContent = "Clock In";

            PasswordFocus();
        }
        public void ChangePassword()
        {
            if (!TimeSheetModel.CheckLogin(EmployeeId, Password))
            {
                MessageBox.Show("Either ID or Password is incorrect", "Incorrect Login", MessageBoxButton.OKCancel, MessageBoxImage.Error);
                return;
            }

            var changePasswordWindow = new ChangePasswordWindow();

            if (changePasswordWindow.ShowDialog() == true)
            {
                string newPassword = changePasswordWindow.NewPasswordStr;
                TimeSheetModel.UpdatePassword(EmployeeId, newPassword);
                if (changePasswordWindow.NewPasswordStr.Length > 0)
                changePasswordWindow.NewPasswordStr.Remove(0, changePasswordWindow.NewPasswordStr.Length - 1);
                TimeSheetModel.SaveEmployees();
            }

        }
        public void ChangeClock()
        {
            if (!TimeSheetModel.CheckLogin(EmployeeId, Password)) MessageBox.Show("Either ID or Password is incorrect", "Incorrect Login", MessageBoxButton.OKCancel, MessageBoxImage.Error);
            else
            {
                TimeSheetModel.ChangeClock(EmployeeId, DateTime.Now);
                CloseFunc();
                TimeSheetModel.SaveEmployees();
                TimeSheetModel.SaveTimeSheet();
            }
        }
        private bool CheckIfCLockedIn() => TimeSheetModel.IsClockedIn(EmployeeId);
    }
}
