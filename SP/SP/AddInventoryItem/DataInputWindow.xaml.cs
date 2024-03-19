using SP.Data;
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
using System.Windows.Shapes;

namespace SP.AddInventoryItem
{
    public partial class DataInputWindow : Window
    {
        public Part PartData { get; private set; }

        public DataInputWindow()
        {
            InitializeComponent();
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            PartData = new Part
            {
                PartNumber = PartNumberTextBox.Text,
                PartDescription = DescriptionTextBox.Text,
                Price = decimal.Parse(PriceTextBox.Text),
                Quantity = int.Parse(QuantityTextBox.Text),
                StockLevel = int.Parse(StockLevelTextBox.Text),
                MinimumRequired = int.Parse(MinimumRequiredTextBox.Text),
                StockLocation = LocationTextBox.Text
            };
            this.Close();
        }
    }
}
