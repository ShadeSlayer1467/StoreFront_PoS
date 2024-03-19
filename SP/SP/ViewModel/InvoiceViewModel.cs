using SP.AddInventoryItem;
using SP.Data;
using SP.DataManipulators;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;

namespace SP.ViewModel
{
    public class InvoiceViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private DbManager _dbManager;
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


            InvoiceViewProperties = new InvoiceViewProperties()
            {
                EmployeeID = 1456,
                EmployeeName = "Matthew Read",
                CustomerID = 0,
                CustomerName = "CASH CASH CASH",
                SalesmanID = 0,
                CustomerInfo = "Walk in Cash Customer",
                PhoneNumber = "",
                Fax = "",
                AuthorizedBuyers = new List<string> { },
                SelectedAuthorizedBuyer = "",

                PartsList = new ObservableCollection<InvoiceViewProperties.InvoicePart>(),

                PartNumber = "",
                PartDescription = "",
                PartQuantity = "",
                PartPrice = "",
                SubTotal = 0
            };
            //InvoiceViewProperties = new InvoiceViewProperties()
            //{
            //    EmployeeID = 9999,
            //    EmployeeName = "Test",
            //    CustomerID = 9999,
            //    CustomerName = "Test",
            //    SalesmanID = 9999,
            //    CustomerInfo = "Test",
            //    PhoneNumber = "Test",
            //    Fax = "Test",
            //    AuthorizedBuyers = new List<string> { "Test" },
            //    SelectedAuthorizedBuyer = "Test1",

            //    PartsList = new ObservableCollection<InvoiceViewProperties.InvoicePart>(),

            //    PartNumber = "Test",
            //    PartDescription = "Test",
            //    PartQuantity = "Test",
            //    PartPrice = "Test",
            //    SubTotal = 9999
            //};
            _dbManager = new DbManager();


            AddOrder = new RelayCommand(AddOrderExecute);

            
        }
        public void ClearPartBoxes()
        {
            InvoiceViewProperties.PartNumber = "";
            InvoiceViewProperties.PartDescription = "";
            InvoiceViewProperties.PartQuantity = "";
            InvoiceViewProperties.PartPrice = "";
            InvoiceViewProperties.PhoneNumber = "";
            InvoiceViewProperties.Fax = "";
            InvoiceViewProperties.SelectedAuthorizedBuyer = null;
            OnPropertyChanged(nameof(InvoiceViewProperties));
        }
        private Invoice MapToInvoice(InvoiceViewProperties invoiceViewProperties)
        {
            var invoice = new Invoice
            {
                EmployeeId = invoiceViewProperties.EmployeeID,
                CustomerId = invoiceViewProperties.CustomerID,
                SalesmanId = invoiceViewProperties.SalesmanID,
                AuthorizedBuyer = invoiceViewProperties.SelectedAuthorizedBuyer,
                PartsList = invoiceViewProperties.PartsList
                    .Select(part => new KeyValuePair<string, int>(part.PartNumber, (int)part.Quantity))
                    .ToList(),
                SubTotal = invoiceViewProperties.PartsList.Sum(part => part.Total),
                // You might need to calculate tax and total based on your business logic
                Tax = 0, // Placeholder value
                Total = invoiceViewProperties.PartsList.Sum(part => part.Total), // Placeholder value
                Date = DateTime.Now,
                InvoiceNumber = _dbManager.GetNextInvoiceNumber(),
                PurchaseOrder = "N/A"
            };

            return invoice;
        }
        private void AddOrderExecute(object obj)
        {
            var _invoice = MapToInvoice(InvoiceViewProperties);

            Console.WriteLine(InvoiceHandler.Print(_invoice));
        }
        public bool PartNumberEnterKeyDown(object partNumber)
        {
            var part = _dbManager.GetPart(partNumber?.ToString());
            if (part != null)
            {
                InvoiceViewProperties.PartDescription = part.PartDescription;
                InvoiceViewProperties.PartPrice = part.Price.ToString();
                OnPropertyChanged(nameof(InvoiceViewProperties));
                return true;
            } else
            {
                // Create and show DataInputWindow
                var dataInputWindow = new DataInputWindow();
                dataInputWindow.ShowDialog();

                // Access data entered by the user
                var partData = dataInputWindow.PartData;

                // Add part to inventory
                _dbManager.InsertPart(part);

            }
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
