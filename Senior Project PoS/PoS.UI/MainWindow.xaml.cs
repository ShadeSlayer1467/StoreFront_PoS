using PoS.UI.ViewModel;
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

namespace PoS.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void PartNumberTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                var vm = (InvoiceViewModel)this.DataContext;
                var partNumber = ((TextBox)sender).Text;
                vm.UpdatePartInfoCommand.Execute(partNumber);
                QuantityTextBox.Clear();
                QuantityTextBox.Text = "1.00";
                QuantityTextBox.Focus();
            }
        }
        private void QuantityTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                // Move focus to Price TextBox
                if (QuantityTextBox.Text.Length == 0)
                    QuantityTextBox.Text = "1.00";
                PriceTextBox.Focus();
            }
        }
        private void PriceTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                var vm = (InvoiceViewModel)this.DataContext;
                vm.AddPartToInvoice();

                // Optionally, clear and move focus to the Part Number TextBox for a new entry
                PartNumberTextBox.Clear();
                PartDescriptionTextBox.Clear();
                QuantityTextBox.Clear();
                PriceTextBox.Clear();

                PartNumberTextBox.Focus();
            }
        }
        private void AddPart_GotFocus(object sender, RoutedEventArgs e)
        {
            var textBox = sender as TextBox;

            switch (textBox.Name)
            {
                case "PartNumberTextBox":
                    PartNumberTextBox.Clear();
                    return;
                //case "QuantityTextBox":
                //    break;
                //case "PriceTextBox":
                //    break;
                default:
                    break;
            }

            textBox.SelectAll();
        }
    }

}
