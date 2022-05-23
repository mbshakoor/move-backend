using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TruckApp.Models;

namespace TruckApp.Data
{
    public interface IWeightUnitRepository
    {
        Task<string> GetWeightUnit();
        Task<string> GetWeightUnitById(int id);
        Task<string> AddWeightUnit(WeightUnit WeightUnit);
        Task<string> UpdateWeightUnit(WeightUnit WeightUnit);
        Task<string> DeleteWeightUnit(int id);
        Task<bool> WeightUnitExist(int id);
    }
}
