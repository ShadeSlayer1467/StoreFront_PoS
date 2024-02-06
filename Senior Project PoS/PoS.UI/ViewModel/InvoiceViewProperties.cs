using PoS.UI.DataModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoS.UI.ViewModel
{
    public class InvoiceViewProperties
    {
        public class InvoicePart
        {
            public string PartNumber { get; set; }
            public string Description { get; set; }
            public decimal Price { get; set; }
            public decimal Quantity { get; set; }
            public decimal Total { get; set; }
        }
        public int EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public int CustomerID { get; set; }
        public string CustomerName { get; set; }
        public int SalesmanID { get; set; }
        public string CustomerInfo { get; set; }
        public string PhoneNumber { get; set; }
        public string Fax { get; set; }
        public List<string> AuthorizedBuyers { get; set; }
        public string SelectedAuthorizedBuyer { get; set; }
        public ObservableCollection<InvoicePart> PartsList { get; set; }


        public string PartNumber { get; set; }
        public string PartDescription { get; set; }
        public string PartQuantity { get; set; }
        public string PartPrice { get; set; }
        public decimal SubTotal { get; set; }

        public InvoiceViewProperties()
        {
            PartsList = new ObservableCollection<InvoicePart>();
        }

    }
}
