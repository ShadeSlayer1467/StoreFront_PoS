using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Data
{
    public class Customer
    {
        public int CustomerID { get; set; }
        public string CustomerName { get; set; }
        public int SalesmanID { get; set; }
        public string CustomerInfo { get; set; }
        public string PhoneNumber { get; set; }
        public string Fax { get; set; }
        public List<string> AuthorizedBuyers { get; set; }

        public Customer()
        {
            AuthorizedBuyers = new List<string>();
        }
    }
}
