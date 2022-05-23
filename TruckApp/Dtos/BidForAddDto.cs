using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TruckApp.Dtos
{
    public class BidForAddDto
    {
        public string BookingId { get; set; }
        public int BranchId { get; set; }
        public string PriceDetail { get; set; }
        public Decimal QuotedPrice { get; set; }
        public int VendorId { get; set; }
        public DateTime DeliverDate { get; set; }
    }
}
