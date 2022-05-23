using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TruckApp.Models;

namespace TruckApp.Data
{
    public class WeightUnitRepository : IWeightUnitRepository
    {

        private readonly DataContext _context;
        public WeightUnitRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<string> GetWeightUnit()
        {
            var WeightUnit = await _context.WeightUnit.ToListAsync();
            string json = JsonConvert.SerializeObject(WeightUnit);
            return json;
        }
        public async Task<string> GetWeightUnitById(int id)
        {
            var WeightUnit = await _context.WeightUnit.FirstOrDefaultAsync(x => x.Id == id);
            string json = JsonConvert.SerializeObject(WeightUnit);
            return json;
        }
        public async Task<string> AddWeightUnit(WeightUnit WeightUnit)
        {
            await _context.WeightUnit.AddAsync(WeightUnit);
            int result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                var WeightUnitData = await _context.WeightUnit.ToListAsync();
                string json = JsonConvert.SerializeObject(WeightUnitData);
                return json;
            }
            return "[]";
        }
        public async Task<string> UpdateWeightUnit(WeightUnit WeightUnit)
        {
            var WeightUnitData = await _context.WeightUnit.FirstOrDefaultAsync(x => x.Id == WeightUnit.Id);
            WeightUnitData.Name = WeightUnit.Name;
            _context.Entry(WeightUnitData).State = EntityState.Modified;
            try
            {
                int result = await _context.SaveChangesAsync();
                if (result > 0)
                {
                    var WeightUnitList = await _context.WeightUnit.ToListAsync();
                    string json = JsonConvert.SerializeObject(WeightUnitList);
                    return json;
                }
            }
            catch (DbUpdateConcurrencyException) { }
            return "[]";
        }
        public async Task<string> DeleteWeightUnit(int id)
        {
            var WeightUnitData = await _context.WeightUnit.FirstOrDefaultAsync(x => x.Id == id);
            WeightUnitData.IsActive = false;
            _context.Entry(WeightUnitData).State = EntityState.Modified;
            try
            {
                int result = await _context.SaveChangesAsync();
                if (result > 0)
                {
                    var WeightUnitList = await _context.WeightUnit.ToListAsync();
                    string json = JsonConvert.SerializeObject(WeightUnitList);
                    return json;
                }
            }
            catch (DbUpdateConcurrencyException) { }
            return "[]";
        }
        public async Task<bool> WeightUnitExist(int id)
        {
            bool result = await _context.WeightUnit.AnyAsync(x => x.Id == id);
            return result;
        }
    }
}
