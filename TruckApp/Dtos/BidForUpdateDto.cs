using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TruckApp.Dtos
{
    public class BidForUpdateDto
    {
        public int Id { get; set; }
        public string PriceDetail { get; set; }
        public Decimal QuotedPrice { get; set; }
        public DateTime DeliveryDate { get; set; }
        public int  BranchId { get; set; }

    }
}
