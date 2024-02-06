using PoS.UI.DataModel;
using PoS.UI.SaveData;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace PoS.UI.ViewModel
{
    public class InvoiceViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private InventoryManager _inventoryManager;
        private InvoiceViewProperties _invoiceViewProperties;
        public InvoiceViewProperties InvoiceViewProperties
        {
            get { return _invoiceViewProperties; }
            set
            {
                _invoiceViewProperties = value;
                OnPropertyChanged();
            }
        }

        public ICommand AddOrder { get; set; }

        public InvoiceViewModel()
        {
            //InvoiceViewProperties = new InvoiceViewProperties(_invoice)
            //{
            //    EmployeeID = 1456,
            //    EmployeeName = "Matthew Read",
            //    CustomerID = 0,
            //    CustomerName = "CASH CASH CASH",
            //    SalesmanID = 0,
            //    CustomerInfo = "Walk in Cash Customer",
            //    PhoneNumber = "",
            //    Fax = "",
            //    AuthorizedBuyers = new List<string> { },
            //    SelectedAuthorizedBuyer = "",

            //    PartsList = new List<Part>(),

            //    PartNumber = "",
            //    PartDescription = "",
            //    PartQuantity = "",
            //    PartPrice = "",
            //    SubTotal = 0
            //};
            InvoiceViewProperties = new InvoiceViewProperties()
            {
                EmployeeID = 9999,
                EmployeeName = "Test",
                CustomerID = 9999,
                CustomerName = "Test",
                SalesmanID = 9999,
                CustomerInfo = "Test",
                PhoneNumber = "Test",
                Fax = "Test",
                AuthorizedBuyers = new List<string> { "Test" },
                SelectedAuthorizedBuyer = "Test1",

                PartsList = new ObservableCollection<InvoiceViewProperties.InvoicePart>(),

                PartNumber = "Test",
                PartDescription = "Test",
                PartQuantity = "Test",
                PartPrice = "Test",
                SubTotal = 9999
            };
            _inventoryManager = new JsonDatabase().GetInventoryManager();

            AddOrder = new RelayCommand(AddOrderExecute);
        }
        private void AddOrderExecute(object obj)
        {
            //TODO: Implement Invoice Manager for transaction number
            var _invoice = new Invoice(_invoiceViewProperties, new Random().Next(1,10000));
            Console.WriteLine(_invoice.Print());
        }
        public bool PartNumberEnterKeyDown(object partNumber)
        {
            var part = _inventoryManager.GetPartInfo(partNumber?.ToString());
            if (part != null)
            {
                InvoiceViewProperties.PartDescription = part.Description;
                InvoiceViewProperties.PartPrice = part.Price.ToString();
                OnPropertyChanged(nameof(InvoiceViewProperties));
                return true;
            }
            //TODO implement adding parts to inventory
            return false;
        }
        public void AddPartToInvoice()
        {
            InvoiceViewProperties.PartsList.Add(new InvoiceViewProperties.InvoicePart()
            {
                PartNumber = InvoiceViewProperties.PartNumber,
                Description = InvoiceViewProperties.PartDescription,
                Price = Convert.ToDecimal(InvoiceViewProperties.PartPrice),
                Quantity = Convert.ToDecimal(InvoiceViewProperties.PartQuantity),
                Total = Convert.ToDecimal(InvoiceViewProperties.PartPrice) * Convert.ToDecimal(InvoiceViewProperties.PartQuantity)
            });
            InvoiceViewProperties.PartNumber = "";
            InvoiceViewProperties.PartDescription = "";
            InvoiceViewProperties.PartQuantity = "";
            InvoiceViewProperties.PartPrice = "";

            InvoiceViewProperties.SubTotal = Math.Round(InvoiceViewProperties.PartsList.Sum(x => x.Total), 2);
            OnPropertyChanged(nameof(InvoiceViewProperties));
        }
    }
}
