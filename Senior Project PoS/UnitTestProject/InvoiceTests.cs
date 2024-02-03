using System;
using PoS.UI.DataModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject
{
    [TestClass]
    public class InvoiceTests
    {
        [TestMethod]
        public void AddPart_IncreasesPartsListCount()
        {
            // Arrange
            var invoice = new Invoice();
            var initialCount = invoice.PartsList.Count;

            // Act
            invoice.AddPart("Brake Pad", 2);

            // Assert
            Assert.AreEqual(initialCount + 1, invoice.PartsList.Count);
        }

        [TestMethod]
        public void RemovePart_DecreasesPartsListCount()
        {
            // Arrange
            var invoice = new Invoice();
            invoice.AddPart("Brake Pad", 2);
            var initialCount = invoice.PartsList.Count;

            // Act
            invoice.RemovePart("Brake Pad");

            // Assert
            Assert.AreEqual(initialCount - 1, invoice.PartsList.Count);
        }

        [TestMethod]
        public void UpdatePart_ChangesQuantity()
        {
            // Arrange
            var invoice = new Invoice();
            invoice.AddPart("Brake Pad", 2);

            // Act
            invoice.UpdatePart("Brake Pad", 4); // Update this method's logic to actually update the part.
            var updatedPart = invoice.PartsList.Find(p => p.Key == "Brake Pad");

            // Assert
            Assert.AreEqual(4, updatedPart.Value);
        }

        [TestMethod]
        public void Print_ReturnsCorrectFormat()
        {
            // Arrange
            var invoice = new Invoice
            {
                InvoiceNumber = 123,
                Date = new DateTime(2024, 1, 1),
                EmployeeId = 1,
                CustomerId = 2,
                SalesmanID = 3,
                AuthorizedBuyer = "John Doe",
                PurchaseOrder = "PO123456",
                SubTotal = 100.00m,
                Tax = 7.00m,
                Total = 107.00m
            };
            invoice.AddPart("Brake Pad", 2);

            // Act
            var result = invoice.Print();

            // Assert
            Assert.IsTrue(result.Contains("Invoice Number: 123"));
            // Add more assertions to validate the entire structure of the printed invoice
            /*
             * var sb = new StringBuilder();
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
                    sb.AppendLine($"{part.Key} - {part.Value}");    }
                sb.AppendLine($"Subtotal: {SubTotal}");
                sb.AppendLine($"Tax: {Tax}");
                sb.AppendLine($"Total: {Total}");
            */
            Assert.IsTrue(
                    result.Contains("Date: 1/1/2024") &&
                    result.Contains("Employee ID: 1") &&
                    result.Contains("Customer ID: 2") &&
                    result.Contains("Salesman ID: 3") &&
                    result.Contains("Authorized Buyer: John Doe") &&
                    result.Contains("Purchase Order: PO123456") &&
                    result.Contains("Brake Pad - 2") &&
                    result.Contains("Subtotal: 100.00") &&
                    result.Contains("Tax: 7.00") &&
                    result.Contains("Total: 107.00")
                );
        }
    }
}