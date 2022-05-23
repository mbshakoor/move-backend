using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TruckApp.Models;

namespace TruckApp.Data
{
    public interface IVehicleTypeRepository
    {
        Task<string> GetVehicleType();
        Task<string> GetVehicleTypeById(int id);
        Task<string> AddVehicleType(VehicleType VehicleType);
        Task<string> UpdateVehicleType(VehicleType VehicleType);
        Task<string> DeleteVehicleType(int id);
        Task<bool> VehicleTypeExist(int id);
    }
}
