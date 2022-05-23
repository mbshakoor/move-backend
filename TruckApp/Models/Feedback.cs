using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TruckApp.Models
{
    public class Feedback
    {
        public int Id { get; set; }
        public int FeedbackTypeId { get; set; }
        public string Suggestion { get; set; }
        public DateTime CreateDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime UpdateDate { get; set; }
        public int UpdatedBy { get; set; }
    }
}
