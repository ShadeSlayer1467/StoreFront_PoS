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
            Parts = new List<Part>();
        }
        public void AddPart(string partNumber, string description, decimal price, int quantity)
        {
            Parts.Add(new Part
            {
                PartNumber = partNumber,
                Description = description,
                Price = price,
                Quantity = quantity
            });
        }
        public void UpdatePart(string partNumber, string description, decimal price, int quantity)
        {
            var existingPart = Parts.Find(p => p.PartNumber == partNumber);
            existingPart.Description = description;
            existingPart.Price = price;
            existingPart.Quantity = quantity;
        }
        public void RemovePart(string partNumber)
        {
            Parts.RemoveAll(p => p.PartNumber == partNumber);
        }
        public void AddPartQuantity(string partNumber, int quantity)
        {
            var existingPart = Parts.Find(p => p.PartNumber == partNumber);
            existingPart.Quantity += quantity;
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
