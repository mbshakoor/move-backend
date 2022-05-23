using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TruckApp.Dtos
{
    public class BookingForUpdateDtos
    {
        public string Id { get; set; }
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
        public string PaymentVia { get; set; }

    }
}
