using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace TimeClockWPF
{
    /// <summary>
    /// Interaction logic for ChangePasswordWindow.xaml
    /// </summary>
    public partial class ChangePasswordWindow : Window
    {
        public string NewPasswordStr { get; private set; }

        public ChangePasswordWindow()
        {
            InitializeComponent();

            ConfirmButton.Click += ConfirmButton_Click;
            CancelButton.Click += CancelButton_Click;
        }

        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            if (NewPassword.Password == ConfirmPassword.Password)
            {
                NewPasswordStr = NewPassword.Password;

                DialogResult = true;
                Close();
            }
            else
            {
                MessageBox.Show("Passwords do not match.");
            }
        }


        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
