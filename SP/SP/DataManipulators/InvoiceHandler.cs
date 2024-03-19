using SP.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.DataManipulators
{
    public class InvoiceHandler
    {
        public static void AddPart(Invoice invoice, string partNumber, int quantity)
        {
            invoice.PartsList.Add(new KeyValuePair<string, int>(partNumber, quantity));
            UpdateInvoiceTotals(invoice);
        }
        public static void RemovePart(Invoice invoice, string partNumber)
        {
            invoice.PartsList.RemoveAll(p => p.Key == partNumber);
            UpdateInvoiceTotals(invoice);
        }
        public static void UpdatePart(Invoice invoice, string partNumber, int quantity)
        {
            var existingPartIndex = invoice.PartsList.FindIndex(p => p.Key == partNumber);
            if (existingPartIndex != -1)
            {
                invoice.PartsList.RemoveAt(existingPartIndex);
                invoice.PartsList.Add(new KeyValuePair<string, int>(partNumber, quantity));
                UpdateInvoiceTotals(invoice);
            }
        }
        private static void UpdateInvoiceTotals(Invoice invoice, decimal taxRate = 0)
        {
            invoice.SubTotal = 0m;
            foreach (var part in invoice.PartsList)
            {
                DbManager dbManager = new DbManager();
                decimal partPrice = dbManager.GetPart(part.Key).Price;
                invoice.SubTotal += partPrice * part.Value;
            }
            invoice.Tax = invoice.SubTotal * taxRate;
            invoice.Total = invoice.SubTotal + invoice.Tax;
        }
        public static string Print(Invoice invoice)
        {
            var sb = new StringBuilder();
            sb.AppendLine($"Invoice Number: {invoice.InvoiceNumber}");
            sb.AppendLine($"Date: {invoice.Date}");
            sb.AppendLine($"Employee ID: {invoice.EmployeeId}");
            sb.AppendLine($"Customer ID: {invoice.CustomerId}");
            sb.AppendLine($"Salesman ID: {invoice.SalesmanId}");
            sb.AppendLine($"Authorized Buyer: {invoice.AuthorizedBuyer}");
            sb.AppendLine($"Purchase Order: {invoice.PurchaseOrder}");
            sb.AppendLine("Parts:");
            foreach (var part in invoice.PartsList)
            {
                sb.AppendLine($"{part.Value} of {part.Key}");
            }
            sb.AppendLine($"Subtotal: {invoice.SubTotal:C}");
            sb.AppendLine($"Tax: {invoice.Tax:C}");
            sb.AppendLine($"Total: {invoice.Total:C}");
            return sb.ToString();
        }
    }

}
