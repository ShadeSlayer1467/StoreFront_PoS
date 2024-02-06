using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoS.UI.DataModel
{
    public class InventoryManager
    {
        public List<Part> Parts { get; set; }
        public InventoryManager()
        {
            // adding parts for testing
            //Parts = new List<Part>()
            //{
            //    new Part { PartNumber = "1234", Description = "Part 1", Price = 10.00m, Quantity = 10 },
            //    new Part { PartNumber = "5678", Description = "Part 2", Price = 20.00m, Quantity = 20 },
            //    new Part { PartNumber = "91011", Description = "Part 3", Price = 30.00m, Quantity = 30 },
            //    new Part { PartNumber = "121314", Description = "Part 4", Price = 40.00m, Quantity = 40 },
            //    new Part { PartNumber = "151617", Description = "Part 5", Price = 50.00m, Quantity = 50 },
            //};
        }
        public void AddPartToDatabase(string partNumber, string description, decimal price, int quantity)
        {
            Parts.Add(new Part
            {
                PartNumber = partNumber,
                Description = description,
                Price = price,
                Quantity = quantity
            });
        }
        public void UpdatePartInformation(string partNumber, string description, decimal price, int quantity)
        {
            var existingPart = Parts.Find(p => p.PartNumber == partNumber);
            existingPart.Description = description;
            existingPart.Price = price;
            existingPart.Quantity = quantity;
        }
        public void RemovePartFromDatabase(string partNumber)
        {
            Parts.RemoveAll(p => p.PartNumber == partNumber);
        }
        public void AddPartQuantity(string partNumber, int quantity)
        {
            var existingPart = Parts.Find(p => p.PartNumber == partNumber);
            existingPart.Quantity += quantity;
        }
        public Part GetPartInfo(string partNumber)
        {
            var part = Parts.Find(p => p.PartNumber == partNumber);
            return part;
        }
        public string Print()
        {
            var sb = new StringBuilder();
            sb.AppendLine("Inventory:");
            foreach (var part in Parts)
            {
                sb.AppendLine($"Part Number: {part.PartNumber}");
                sb.AppendLine($"Description: {part.Description}");
                sb.AppendLine($"Price: {part.Price}");
                sb.AppendLine($"Quantity: {part.Quantity}");
                sb.AppendLine();
            }
            return sb.ToString();
        }
    }
}
