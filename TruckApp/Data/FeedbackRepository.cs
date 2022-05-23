using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SQLitePCL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TruckApp.Models;

namespace TruckApp.Data
{
    public class FeedbackRepository : IFeedbackRepository
    {
        private readonly DataContext _context;
        public FeedbackRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<string> GetFeedback()
        {
            var feedback = await _context.Feedback.ToListAsync();
            string json = JsonConvert.SerializeObject(feedback);
            return json;
        }
        public async Task<string> GetFeedbackByUserId(int userid)
        {
            var feedback = await _context.Feedback.Where(x=>x.CreatedBy==userid).ToListAsync();
            string json = JsonConvert.SerializeObject(feedback);
            return json;
        }

        public async Task<string> GetFeedbackById(int id)
        {
            var feedback = await _context.Feedback.FirstOrDefaultAsync(x => x.Id == id);
            string json = JsonConvert.SerializeObject(feedback);
            return json;
        }
        public async Task<string> AddFeedback(Feedback feedback)
        {
            await _context.Feedback.AddAsync(feedback);
            int result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                string bidList = await GetFeedbackByUserId(feedback.CreatedBy);
                return bidList;
            }
            return "[]";
        }
        public async Task<string> UpdateFeedback(Feedback feedback)
        {
            string feedbackData = "";
            var feedbackList = await _context.Feedback.FirstOrDefaultAsync(x => x.Id == feedback.Id);
                feedbackList.FeedbackTypeId = feedback.FeedbackTypeId;
                feedbackList.Suggestion = feedback.Suggestion;
                feedbackList.UpdateDate = DateTime.Now;
                feedbackList.UpdatedBy = feedback.UpdatedBy;
                _context.Entry(feedbackList).State = EntityState.Modified;
                try
                {
                    int result = await _context.SaveChangesAsync();
                    if (result > 0)
                    {
                        feedbackData = await GetFeedbackByUserId(feedback.UpdatedBy);
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                }
            return feedbackData;
        }
        public async Task<bool> FeedbackExist(int id)
        {
            var feedback = await _context.Feedback.AnyAsync(x => x.Id == id);
            return feedback;
        }
    }
}
