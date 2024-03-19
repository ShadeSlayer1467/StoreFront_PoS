using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Data
{
    public class Part
    {
        public string PartNumber { get; set; }
        public string PartDescription { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public int StockLevel { get; set; }
        public int MinimumRequired { get; set; }
        public string StockLocation { get; set; }
    }
}
