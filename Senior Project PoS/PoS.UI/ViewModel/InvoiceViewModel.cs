using PoS.UI.DataModel;
using PoS.UI.SaveData;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PoS.UI.ViewModel
{
    public class InvoiceViewModel : INotifyPropertyChanged
    {
        private Invoice _invoice;
        private InventoryManager _inventoryManager;
        public DateTime TodayDate { get; set; }

        public Invoice Invoice
        {
            get => _invoice;
            set
            {
                _invoice = value;
                OnPropertyChanged(nameof(Invoice));
            }
        }

        // Assuming your InventoryManager is also needed in the context of InvoiceViewModel
        public InventoryManager InventoryManager
        {
            get => _inventoryManager;
            set
            {
                _inventoryManager = value;
                OnPropertyChanged(nameof(InventoryManager));
            }
        }

        public ICommand SaveInvoiceCommand { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public InvoiceViewModel()
        {
            // Initialize InventoryManager and Invoice
            var db = new JsonDatabase();
            _inventoryManager = db.GetInventoryManager(); // Load inventory
            _invoice = new Invoice(); // Initialize a new invoice, could also be loaded

            TodayDate = DateTime.Now;

            SaveInvoiceCommand = new RelayCommand(SaveInvoice);
        }

        private void SaveInvoice(object parameter)
        {
            // Save logic here, for example:
            // You might want to save the Invoice to a file or database
            // This is just a placeholder to demonstrate the concept
            System.Diagnostics.Debug.WriteLine("Invoice saved!");

            System.Diagnostics.Debug.WriteLine(_invoice.Print());
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // Additional functionality as needed, for example:
        // Methods to add, update, remove parts from the Invoice.PartsList
        // Methods to interact with InventoryManager, like adding or removing stock as invoices are processed
    }

    
}
