using SP.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace SP.DataManipulators
{
    public class DbManager
    {
        private readonly string _connectionString;

        public DbManager()
        {
            _connectionString = "Data Source=(localdb)\\SeniorProject;Integrated Security=True;Connect Timeout=30;Encrypt=False;MultipleActiveResultSets=true;";
        }

        public void InsertCustomer(Customer customer)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("INSERT INTO Customer (CustomerID, CustomerName, SalesmanID, CustomerInfo, PhoneNumber, Fax) VALUES (@CustomerID, @CustomerName, @SalesmanID, @CustomerInfo, @PhoneNumber, @Fax)", connection);
                command.Parameters.AddWithValue("@CustomerID", customer.CustomerID);
                command.Parameters.AddWithValue("@CustomerName", customer.CustomerName);
                command.Parameters.AddWithValue("@SalesmanID", customer.SalesmanID);
                command.Parameters.AddWithValue("@CustomerInfo", customer.CustomerInfo);
                command.Parameters.AddWithValue("@PhoneNumber", customer.PhoneNumber);
                command.Parameters.AddWithValue("@Fax", customer.Fax);

                connection.Open();
                command.ExecuteNonQuery();

                if (customer.AuthorizedBuyers != null)
                {
                    foreach (var buyer in customer.AuthorizedBuyers)
                    {
                        var buyerCommand = new SqlCommand("INSERT INTO AuthorizedBuyers (CustomerID, BuyerName) VALUES (@CustomerID, @BuyerName)", connection);
                        buyerCommand.Parameters.AddWithValue("@CustomerID", customer.CustomerID);
                        buyerCommand.Parameters.AddWithValue("@BuyerName", buyer);
                        buyerCommand.ExecuteNonQuery();
                    }
                }
            }
        }
        public void UpdateCustomer(Customer customer)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("UPDATE Customer SET CustomerName = @CustomerName, SalesmanID = @SalesmanID, CustomerInfo = @CustomerInfo, PhoneNumber = @PhoneNumber, Fax = @Fax WHERE CustomerID = @CustomerID", connection);
                command.Parameters.AddWithValue("@CustomerID", customer.CustomerID);
                command.Parameters.AddWithValue("@CustomerName", customer.CustomerName);
                command.Parameters.AddWithValue("@SalesmanID", customer.SalesmanID);
                command.Parameters.AddWithValue("@CustomerInfo", customer.CustomerInfo);
                command.Parameters.AddWithValue("@PhoneNumber", customer.PhoneNumber);
                command.Parameters.AddWithValue("@Fax", customer.Fax);

                connection.Open();
                command.ExecuteNonQuery();

                var deletecommand = new SqlCommand("DELETE FROM AuthorizedBuyers WHERE CustomerID = @CustomerID", connection);
                deletecommand.Parameters.AddWithValue("@CustomerID", customer.CustomerID);
                deletecommand.ExecuteNonQuery();

                if (customer.AuthorizedBuyers.Count > 0)
                {
                    foreach (var buyer in customer.AuthorizedBuyers)
                    {
                        var buyerCommand = new SqlCommand("INSERT INTO AuthorizedBuyers (CustomerID, BuyerName) VALUES (@CustomerID, @BuyerName)", connection);
                        buyerCommand.Parameters.AddWithValue("@CustomerID", customer.CustomerID);
                        buyerCommand.Parameters.AddWithValue("@BuyerName", buyer);
                        buyerCommand.ExecuteNonQuery();
                    }
                }
            }
        }
        public Customer GetCustomer(int customerId)
        {
            var customer = new Customer();
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("SELECT * FROM Customer WHERE CustomerID = @CustomerID", connection);
                command.Parameters.AddWithValue("@CustomerID", customerId);

                var buyersCommand = new SqlCommand("SELECT BuyerName FROM AuthorizedBuyers WHERE CustomerID = @CustomerID", connection);
                buyersCommand.Parameters.AddWithValue("@CustomerID", customerId);

                connection.Open();
                using (var reader = command.ExecuteReader())
                using (var buyersReader = buyersCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        customer.CustomerID = (int)reader["CustomerID"];
                        customer.CustomerName = reader["CustomerName"].ToString();
                        customer.SalesmanID = (int)reader["SalesmanID"];
                        customer.CustomerInfo = reader["CustomerInfo"].ToString();
                        customer.PhoneNumber = reader["PhoneNumber"].ToString();
                        customer.Fax = reader["Fax"].ToString();

                        while (buyersReader.Read())
                        {
                            customer.AuthorizedBuyers.Add(buyersReader["BuyerName"].ToString());
                        }
                    }
                }
            }
            return customer;
        }

        public void InsertInvoice(Invoice invoice)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("INSERT INTO Invoice (Id, EmployeeId, CustomerId, SalesmanId, AuthorizedBuyer, SubTotal, Tax, Total, Date, InvoiceNumber, PurchaseOrder) VALUES (@Id, @EmployeeId, @CustomerId, @SalesmanId, @AuthorizedBuyer, @SubTotal, @Tax, @Total, @Date, @InvoiceNumber, @PurchaseOrder)", connection);
                command.Parameters.AddWithValue("@Id", invoice.Id);
                command.Parameters.AddWithValue("@EmployeeId", invoice.EmployeeId);
                command.Parameters.AddWithValue("@CustomerId", invoice.CustomerId);
                command.Parameters.AddWithValue("@SalesmanId", invoice.SalesmanId);
                command.Parameters.AddWithValue("@AuthorizedBuyer", invoice.AuthorizedBuyer);
                command.Parameters.AddWithValue("@SubTotal", invoice.SubTotal);
                command.Parameters.AddWithValue("@Tax", invoice.Tax);
                command.Parameters.AddWithValue("@Total", invoice.Total);
                command.Parameters.AddWithValue("@Date", invoice.Date);
                command.Parameters.AddWithValue("@InvoiceNumber", invoice.InvoiceNumber);
                command.Parameters.AddWithValue("@PurchaseOrder", invoice.PurchaseOrder);

                connection.Open();
                command.ExecuteNonQuery();

                // Insert PartsList entries
                foreach (var part in invoice.PartsList)
                {
                    var partCommand = new SqlCommand("INSERT INTO InvoiceParts (InvoiceId, PartNumber, Quantity) VALUES (@InvoiceId, @PartNumber, @Quantity)", connection);
                    partCommand.Parameters.AddWithValue("@InvoiceId", invoice.Id);
                    partCommand.Parameters.AddWithValue("@PartNumber", part.Key);
                    partCommand.Parameters.AddWithValue("@Quantity", part.Value);
                    partCommand.ExecuteNonQuery();
                }
            }
        }
        public Invoice GetInvoice(int invoiceId)
        {
            var invoice = new Invoice();

            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("SELECT * FROM Invoice WHERE Id = @Id", connection);
                command.Parameters.AddWithValue("@Id", invoiceId);

                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        invoice.Id = (int)reader["Id"];
                        invoice.EmployeeId = (int)reader["EmployeeId"];
                        invoice.CustomerId = (int)reader["CustomerId"];
                        invoice.SalesmanId = (int)reader["SalesmanId"];
                        invoice.AuthorizedBuyer = reader["AuthorizedBuyer"].ToString();
                        invoice.SubTotal = (decimal)reader["SubTotal"];
                        invoice.Tax = (decimal)reader["Tax"];
                        invoice.Total = (decimal)reader["Total"];
                        invoice.Date = (DateTime)reader["Date"];
                        invoice.InvoiceNumber = (int)reader["InvoiceNumber"];
                        invoice.PurchaseOrder = reader["PurchaseOrder"].ToString();
                    }
                }

                if (invoice.Id > 0)
                {
                    command.CommandText = "SELECT PartNumber, Quantity FROM InvoiceParts WHERE InvoiceId = @InvoiceId";
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@InvoiceId", invoice.Id);

                    using (var reader = command.ExecuteReader())
                    {
                        invoice.PartsList = new List<KeyValuePair<string, int>>();
                        while (reader.Read())
                        {
                            var partNumber = reader["PartNumber"].ToString();
                            var quantity = (int)reader["Quantity"];
                            invoice.PartsList.Add(new KeyValuePair<string, int>(partNumber, quantity));
                        }
                    }
                }
            }

            return invoice;
        }
        public int GetNextInvoiceNumber()
        {
            int nextInvoiceNumber = 0;
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("SELECT MAX(InvoiceNumber) FROM Invoice", connection);
                connection.Open();
                var result = command.ExecuteScalar();
                if (result != DBNull.Value)
                {
                    nextInvoiceNumber = (int)result + 1;
                }
            }
            return nextInvoiceNumber;
        }
        public void InsertPart(Part part)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("INSERT INTO Part (PartNumber, PartDescription, Price, Quantity, StockLevel, MinimumRequired, StockLocation) VALUES (@PartNumber, @PartDescription, @Price, @Quantity, @StockLevel, @MinimumRequired, @StockLocation)", connection);
                command.Parameters.AddWithValue("@PartNumber", part.PartNumber);
                command.Parameters.AddWithValue("@PartDescription", part.PartDescription);
                command.Parameters.AddWithValue("@Price", part.Price);
                command.Parameters.AddWithValue("@Quantity", part.Quantity);
                command.Parameters.AddWithValue("@StockLevel", part.StockLevel);
                command.Parameters.AddWithValue("@MinimumRequired", part.MinimumRequired);
                command.Parameters.AddWithValue("@StockLocation", part.StockLocation);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
        public void UpdatePart(Part part)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("UPDATE Part SET PartDescription = @PartDescription, Price = @Price, Quantity = @Quantity, StockLevel = @StockLevel, MinimumRequired = @MinimumRequired, StockLocation = @StockLocation WHERE PartNumber = @PartNumber", connection);
                command.Parameters.AddWithValue("@PartNumber", part.PartNumber);
                command.Parameters.AddWithValue("@PartDescription", part.PartDescription);
                command.Parameters.AddWithValue("@Price", part.Price);
                command.Parameters.AddWithValue("@Quantity", part.Quantity);
                command.Parameters.AddWithValue("@StockLevel", part.StockLevel);
                command.Parameters.AddWithValue("@MinimumRequired", part.MinimumRequired);
                command.Parameters.AddWithValue("@StockLocation", part.StockLocation);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
        public void UpdatePartQuantity(string partNumber, int quantity)
        {
            var currentQuantity = GetPart(partNumber).Quantity;
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("UPDATE Part SET Quantity = @Quantity WHERE PartNumber = @PartNumber", connection);
                command.Parameters.AddWithValue("@PartNumber", partNumber);
                command.Parameters.AddWithValue("@Quantity", currentQuantity + quantity);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
        public Part GetPart(string partNumber)
        {
            var part = new Part();

            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("SELECT * FROM Part WHERE PartNumber = @PartNumber", connection);
                command.Parameters.AddWithValue("@PartNumber", partNumber);

                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        part.PartNumber = reader["PartNumber"].ToString();
                        part.PartDescription = reader["PartDescription"].ToString();
                        part.Price = (decimal)reader["Price"];
                        part.Quantity = (int)reader["Quantity"];
                        part.StockLevel = (int)reader["StockLevel"];
                        part.MinimumRequired = (int)reader["MinimumRequired"];
                        part.StockLocation = reader["StockLocation"].ToString();
                    }
                }
            }

            return part;
        }

    }
}