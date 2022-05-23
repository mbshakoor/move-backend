using Microsoft.AspNetCore.Mvc.RazorPages.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TruckApp.Models
{
    public class CategoryDetail
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public int BranchId { get; set; }
        public string Image { get; set; }
        public string Icon { get; set; }
        public int VendorId { get; set; }
        public Decimal BaseFare { get; set; }
        public Decimal PerKm { get; set; }
        public string Description { get; set; }
        public string Dimensions { get; set; }
        public int LoadCapacity { get; set; }
        public string PlateNo { get; set; }
        public string Property { get; set; }
        public bool AllowBidding { get; set; }
        public string Status { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreateDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime UpdateDate { get; set; }
        public int UpdatedBy { get; set; }
    }
}
