using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TruckApp.Dtos
{
    public class BookingForAdd
    {
        public int CategoryId { get; set; }
        public int CustomerId { get; set; }
        public int BranchId { get; set; }
        public string PickupLat { get; set; }
        public string PickupLong { get; set; }
        public string PickupAddress { get; set; }
        public string PickupLocationType { get; set; }
        public string PickupPersonName { get; set; }
        public long PickupPersonNumber { get; set; }
        public string DropOffLat { get; set; }
        public string DropOffLong { get; set; }
        public string DropOffAddress { get; set; }
        public string DropOffLocationType { get; set; }
        public string DropOffPersonName { get; set; }
        public long DropOffPersonNumber { get; set; }
        public DateTime DelivaryDateFrom { get; set; }
        public DateTime DeliveryDateTo { get; set; }
        public int LoadType { get; set; }

        public Decimal EstimateWeight { get; set; }
        public int WeightUnit { get; set; }
        public string GoodsDescription { get; set; }
        public string PaymentVia { get; set; }
        public string PromoCode { get; set; }
        public Decimal EstimatedPrice { get; set; }
        public DateTime CreateDate { get; set; }
        public string Status { get; set; }
        public string Reason { get; set; }
        public bool IsBooked { get; set; }
        public int VendorId { get; set; }

    }
}
