using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TruckApp.Dtos
{
    public class FeedbackForUpdateDto
    {
        public int Id { get; set; }
        public int FeedbackTypeId { get; set; }
        public string Suggestion { get; set; }
        public DateTime UpdateDate { get; set; }
        public int UpdatedBy { get; set; }
    }
}
    