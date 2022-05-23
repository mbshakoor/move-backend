using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TruckApp.Models;

namespace TruckApp.Data
{
    public class LoadTypeRepository : ILoadTypeRepository
    {

        private readonly DataContext _context;
        public LoadTypeRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<string> GetLoadType()
        {
            var loadType = await _context.LoadType.ToListAsync();
            string json = JsonConvert.SerializeObject(loadType);
            return json;
        }
        public async Task<string> GetLoadTypeById(int id)
        {
            var loadType = await _context.LoadType.FirstOrDefaultAsync(x => x.Id == id);
            string json = JsonConvert.SerializeObject(loadType);
            return json;
        }
        public async Task<string> AddLoadType(LoadType loadType)
        {
            await _context.LoadType.AddAsync(loadType);
            int result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                var loadTypeData = await _context.LoadType.ToListAsync();
                string json = JsonConvert.SerializeObject(loadTypeData);
                return json;
            }
            return "[]";
        }
        public async Task<string> UpdateLoadType(LoadType loadType)
        {
            var loadTypeData = await _context.LoadType.FirstOrDefaultAsync(x => x.Id == loadType.Id);
            loadTypeData.Name = loadType.Name;
            _context.Entry(loadTypeData).State = EntityState.Modified;
            try
            {
                int result = await _context.SaveChangesAsync();
                if (result > 0)
                {
                    var loadTypeList = await _context.LoadType.ToListAsync();
                    string json = JsonConvert.SerializeObject(loadTypeList);
                    return json;
                }
            }
            catch (DbUpdateConcurrencyException) { }
            return "[]";
        }
        public async Task<string> DeleteLoadType(int id)
        {
            var loadTypeData = await _context.LoadType.FirstOrDefaultAsync(x => x.Id == id);
            loadTypeData.IsActive = false;
            _context.Entry(loadTypeData).State = EntityState.Modified;
            try
            {
                int result = await _context.SaveChangesAsync();
                if (result > 0)
                {
                    var loadTypeList = await _context.LoadType.ToListAsync();
                    string json = JsonConvert.SerializeObject(loadTypeList);
                    return json;
                }
            }
            catch (DbUpdateConcurrencyException) { }
            return "[]";
        }
        public async Task<bool> LoadTypeExist(int id)
        {
            bool result = await _context.LoadType.AnyAsync(x => x.Id == id);
            return result;
        }
    }
}
