using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TruckApp.Models;

namespace TruckApp.Data
{
    public class FeedbackTypeRepository : IFeedbackTypeRepository
    {
        private readonly DataContext _context;
        public FeedbackTypeRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<string> GetFeedbackType()
        {
            var feedback = await _context.FeedbackType.ToListAsync();
            string json = JsonConvert.SerializeObject(feedback);
            return json;
        }
        public async Task<string> GetFeedbackTypebyId(int id)
        {
            var feedback = await _context.FeedbackType.FirstOrDefaultAsync(x => x.Id == id);
            string json = JsonConvert.SerializeObject(feedback);
            return json;
        }
        public async Task<string> AddFeedbackType(FeedbackType feedbackType)
        {
            await _context.FeedbackType.AddAsync(feedbackType);
            int result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                var feedbackTypeList = await _context.FeedbackType.ToListAsync();
                string json = JsonConvert.SerializeObject(feedbackTypeList);
                return json;
            }
            return "[]";
        }
        public async Task<string> UpdateFeedbackType(FeedbackType feedbackType)
        {
            var feedbackTypeList = await _context.FeedbackType.FirstOrDefaultAsync(x => x.Id == feedbackType.Id);
            feedbackTypeList.Name = feedbackType.Name;
            _context.Entry(feedbackTypeList).State = EntityState.Modified;
            try
            {
                int result = await _context.SaveChangesAsync();
                if (result > 0)
                {
                    var feedbackTypeData = await _context.FeedbackType.ToListAsync();
                    string json = JsonConvert.SerializeObject(feedbackTypeData);
                    return json;
                }
            }
            catch (DbUpdateConcurrencyException) { }    
            return "[]";

        }
        public async Task<string> DeleteFeedbackType(int id)
        {
            var feedbackList = await _context.FeedbackType.FirstOrDefaultAsync(x => x.Id == id);
            feedbackList.IsActive = false;
            _context.Entry(feedbackList).State = EntityState.Modified;
            try
            {
                int result = await _context.SaveChangesAsync();
                if (result > 0)
                {
                    var feedbackTypeData = await _context.FeedbackType.ToListAsync();
                    string json = JsonConvert.SerializeObject(feedbackTypeData);
                    return json;
                }
            }
            catch (DbUpdateConcurrencyException) { }
            return "[]";
        }
        public async Task<bool> FeedbackTypeExist(int id)
        {
            bool result = await _context.FeedbackType.AnyAsync(x => x.Id == id);
            return result;
        }
    }
}
