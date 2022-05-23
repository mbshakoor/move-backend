using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TruckApp.Models
{
    public class Vendor
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public long PhoneNo { get; set; }
        public string Address { get; set; }
        public bool IsActive { get; set; }
    }
}
