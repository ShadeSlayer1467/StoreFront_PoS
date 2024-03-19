using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TimeClockWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainVM vm;
        private bool CheckLogin => vm.TimeSheetModel.CheckLogin(vm.EmployeeId, vm.Password);
        public int EmployeeId { get; set; } 
        public MainWindow(string EmployeeID = "-1")
        {
            InitializeComponent();
            vm = new MainVM();
            vm = DataContext as MainVM;
            vm.CloseFunc = Close;
            vm.PasswordFocus = PasswordBox.Focus;
            if (EmployeeID != "-1")
            {
                vm.EmployeeId = EmployeeID;
                EmpId.Focusable = false;
            }

            EmpId.Focus();
        }
        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is MainVM viewModel)
            {
                viewModel.Password = PasswordBox.Password;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            vm = DataContext as MainVM;

            if (CheckLogin)
                EmployeeId = Int32.TryParse(vm.EmployeeId, out int result) ? result : -1;
            else EmployeeId = -1;
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            vm = DataContext as MainVM;

            if (CheckLogin)
                EmployeeId = Int32.TryParse(vm.EmployeeId, out int result) ? result : -1;
            else EmployeeId = -1;
        }

        private void EmpId_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
