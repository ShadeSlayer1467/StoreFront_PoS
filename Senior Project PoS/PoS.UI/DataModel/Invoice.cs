using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoS.UI.DataModel
{
    public class Invoice
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public int CustomerId { get; set; }
        public int SalesmanID { get; set; }
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
        }
        // add a part to the invoice
        public void AddPart(string part, int quantity)
        {
            PartsList.Add(new KeyValuePair<string, int>(part, quantity));
        }
        // remove a part from the invoice
        public void RemovePart(string part)
        {
            PartsList.RemoveAll(p => p.Key == part);
        }
        // update the quantity of a part in the invoice
        public void UpdatePart(string part, int quantity)
        {
            var existingPart = PartsList.Find(p => p.Key == part);
            PartsList.Remove(existingPart);
            PartsList.Add(new KeyValuePair<string, int>(part, quantity));
        }
        // print
        public string Print()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"Invoice Number: {InvoiceNumber}");
            sb.AppendLine($"Date: {Date}");
            sb.AppendLine($"Employee ID: {EmployeeId}");
            sb.AppendLine($"Customer ID: {CustomerId}");
            sb.AppendLine($"Salesman ID: {SalesmanID}");
            sb.AppendLine($"Authorized Buyer: {AuthorizedBuyer}");
            sb.AppendLine($"Purchase Order: {PurchaseOrder}");
            sb.AppendLine("Parts:");
            foreach (var part in PartsList)
            {
                sb.AppendLine($"{part.Key} - {part.Value}");
            }
            sb.AppendLine($"Subtotal: {SubTotal}");
            sb.AppendLine($"Tax: {Tax}");
            sb.AppendLine($"Total: {Total}");
            return sb.ToString();
        }
    }
}
