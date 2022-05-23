using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TruckApp.Models;

namespace TruckApp.Data
{
    public class VehicleTypeRepository : IVehicleTypeRepository
    {

        private readonly DataContext _context;
        public VehicleTypeRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<string> GetVehicleType()
        {
            var VehicleType = await _context.VehicleType.ToListAsync();
            string json = JsonConvert.SerializeObject(VehicleType);
            return json;
        }
        public async Task<string> GetVehicleTypeById(int id)
        {
            var VehicleType = await _context.VehicleType.FirstOrDefaultAsync(x => x.Id == id);
            string json = JsonConvert.SerializeObject(VehicleType);
            return json;
        }
        public async Task<string> AddVehicleType(VehicleType VehicleType)
        {
            await _context.VehicleType.AddAsync(VehicleType);
            int result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                var VehicleTypeData = await _context.VehicleType.ToListAsync();
                string json = JsonConvert.SerializeObject(VehicleTypeData);
                return json;
            }
            return "[]";
        }
        public async Task<string> UpdateVehicleType(VehicleType VehicleType)
        {
            var VehicleTypeData = await _context.VehicleType.FirstOrDefaultAsync(x => x.Id == VehicleType.Id);
            VehicleTypeData.Name = VehicleType.Name;
            _context.Entry(VehicleTypeData).State = EntityState.Modified;
            try
            {
                int result = await _context.SaveChangesAsync();
                if (result > 0)
                {
                    var VehicleTypeList = await _context.VehicleType.ToListAsync();
                    string json = JsonConvert.SerializeObject(VehicleTypeList);
                    return json;
                }
            }
            catch (DbUpdateConcurrencyException) { }
            return "[]";
        }
        public async Task<string> DeleteVehicleType(int id)
        {
            var VehicleTypeData = await _context.VehicleType.FirstOrDefaultAsync(x => x.Id == id);
            VehicleTypeData.IsActive = false;
            _context.Entry(VehicleTypeData).State = EntityState.Modified;
            try
            {
                int result = await _context.SaveChangesAsync();
                if (result > 0)
                {
                    var VehicleTypeList = await _context.VehicleType.ToListAsync();
                    string json = JsonConvert.SerializeObject(VehicleTypeList);
                    return json;
                }
            }
            catch (DbUpdateConcurrencyException) { }
            return "[]";
        }
        public async Task<bool> VehicleTypeExist(int id)
        {
            bool result = await _context.VehicleType.AnyAsync(x => x.Id == id);
            return result;
        }
    }
}
