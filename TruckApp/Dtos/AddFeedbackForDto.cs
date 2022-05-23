using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TruckApp.Dtos
{
    public class AddFeedbackForDto
    {
        public int FeedbackTypeId { get; set; }
        public string Suggestion { get; set; }
        public DateTime CreateDate { get; set; }
        public int CreatedBy { get; set; }
        
    }
}
