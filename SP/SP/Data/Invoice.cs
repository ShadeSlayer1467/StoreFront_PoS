using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Data
{
    public class Invoice
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public int CustomerId { get; set; }
        public int SalesmanId { get; set; }
        public string AuthorizedBuyer { get; set; }
        public List<KeyValuePair<string, int>> PartsList { get; set; }

        public decimal SubTotal { get; set; }
        public decimal Tax { get; set; }
        public decimal Total { get; set; }

        public DateTime Date { get; set; }
        public int InvoiceNumber { get; set; }
        public string PurchaseOrder { get; set; }

        public Invoice()
        {
            PartsList = new List<KeyValuePair<string, int>>();
            Date = DateTime.Now;
            PurchaseOrder = "N/A"; // Default value, can be changed later if necessary
        }
    }
}
