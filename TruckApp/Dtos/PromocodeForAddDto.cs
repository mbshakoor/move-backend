using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TruckApp.Dtos
{
    public class PromocodeForAddDto
    {
        public string Code { get; set; }
        public string Title { get; set; }
        public int DiscontPercent { get; set; }
        public DateTime EffectiveDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }
    }
}
