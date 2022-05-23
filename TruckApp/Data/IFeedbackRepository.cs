using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TruckApp.Models;

namespace TruckApp.Data
{
    public interface IFeedbackRepository
    {
        Task<string> GetFeedback();
        Task<string> GetFeedbackByUserId(int userid);
        Task<string> GetFeedbackById(int id);
        Task<string> AddFeedback(Feedback feedback);
        Task<string> UpdateFeedback(Feedback feedback);
        //Task<string> DeleteFeedback(int id);
        Task<bool> FeedbackExist(int id);
    }
}
