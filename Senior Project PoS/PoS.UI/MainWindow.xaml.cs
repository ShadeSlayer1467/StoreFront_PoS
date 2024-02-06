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
        public enum FocusState
        {
            PartNumber,
            PartQuantity,
            PartPrice
        }
        private FocusState _focusState;
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
                bool partExists = vm.PartNumberEnterKeyDown(partNumber);

                if (partExists)
                {
                    QuantityTextBox.Clear();
                    QuantityTextBox.Text = "1.00";
                    QuantityTextBox.Focus();
                }
                else
                {
                    PartNumberTextBox.Clear();
                    PartNumberTextBox.Focus();
                }
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
                    _focusState = FocusState.PartNumber;
                    return;
                case "QuantityTextBox":
                    _focusState = FocusState.PartQuantity;
                    break;
                case "PriceTextBox":
                    _focusState = FocusState.PartPrice;
                    break;
                default:
                    break;
            }

            textBox.SelectAll();
        }
        private void Grid_PreviewGotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (e.NewFocus is TextBox targetTextBox)
            {
                bool shouldCancelFocus = false;

                switch (_focusState)
                {
                    case FocusState.PartNumber:
                        shouldCancelFocus = targetTextBox.Name != QuantityTextBox.Name;
                        break;
                    case FocusState.PartQuantity:
                        shouldCancelFocus = targetTextBox.Name != PriceTextBox.Name;
                        break;
                }

                if (shouldCancelFocus) e.OldFocus.Focus();

            }
        }

    }

}
