using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TruckApp.Dtos
{
    public class CategoryForInsertDto
    {
        public string Name { get; set; }
        public int ParentId { get; set; }
        public int BranchId { get; set; }
        public IFormFile Image { get; set; }
        public IFormFile Icon { get; set; }
        public Decimal BaseFare { get; set; }
        public Decimal PerKm { get; set; }
        public string Description { get; set; }
        public string Dimensions { get; set; }
        public int LoadCapacity { get; set; }
        public bool IsActive { get; set; }
        public bool AllowBidding { get; set; }
        public string PlateNo { get; set; }
        public string Property { get; set; }
        public int Type { get; set; }
        public int CreatedBy { get; set; }
        public int UpdatedBy { get; set; }
        public int VendorId { get; set; }
    }
}
