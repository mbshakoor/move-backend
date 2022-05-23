using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TruckApp.Models;

namespace TruckApp.Data
{
    public interface IFeedbackTypeRepository
    {
        Task<string> GetFeedbackType();
        Task<string> GetFeedbackTypebyId(int id);
        Task<string> AddFeedbackType(FeedbackType feedbackType);
        Task<string> UpdateFeedbackType(FeedbackType feedbackType);
        Task<string> DeleteFeedbackType(int id);
        Task<bool> FeedbackTypeExist(int id);
    }
}
