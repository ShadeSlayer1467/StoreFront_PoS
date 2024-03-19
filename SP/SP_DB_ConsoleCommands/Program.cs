using SP.Data;
using SP.DataManipulators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP_DB_ConsoleCommands
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Creating instances of customers
            var customer1 = new Customer
            {
                CustomerID = 1,
                CustomerName = "Customer 1",
                SalesmanID = 101,
                CustomerInfo = "Info about Customer 1",
                PhoneNumber = "123456789",
                Fax = "987654321",
                AuthorizedBuyers = new List<string> { "Buyer 1", "Buyer 2" }
            };

            // Inserting customer into the database
            var dbManager = new DbManager();
            //dbManager.InsertCustomer(customer1);

            // Creating instances of parts
            var part1 = new Part
            {
                PartNumber = "P001",
                PartDescription = "Part 1",
                Price = 10.99m,
                Quantity = 100,
                StockLevel = 95,
                MinimumRequired = 10,
                StockLocation = "Warehouse A"
            };

            // Inserting part into the database
            //dbManager.InsertPart(part1);

            // Creating instances of invoices
            var invoice1 = new Invoice
            {
                Id = 1,
                EmployeeId = 201,
                CustomerId = 1, // Assuming this matches the ID of customer1
                SalesmanId = 101, // Assuming this matches the SalesmanID of customer1
                AuthorizedBuyer = "Buyer 1", // Assuming this is one of the authorized buyers
                SubTotal = 100.00m, // Placeholder values
                Tax = 10.00m,
                Total = 110.00m,
                Date = DateTime.Now,
                InvoiceNumber = dbManager.GetNextInvoiceNumber(), // Get the next invoice number from the database
                PurchaseOrder = "PO123",
                PartsList = new List<KeyValuePair<string, int>>
                {
                    new KeyValuePair<string, int>("P001", 5) // Assuming "P001" matches the PartNumber of part1
                }
            };

            // Inserting invoice into the database
            //dbManager.InsertInvoice(invoice1);
        }
    }
}
