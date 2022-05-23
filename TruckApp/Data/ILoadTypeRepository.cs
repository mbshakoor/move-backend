using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TruckApp.Models;

namespace TruckApp.Data
{
    public interface ILoadTypeRepository
    {
        Task<string> GetLoadType();
        Task<string> GetLoadTypeById(int id);
        Task<string> AddLoadType(LoadType loadType);
        Task<string> UpdateLoadType(LoadType loadType);
        Task<string> DeleteLoadType(int id);
        Task<bool> LoadTypeExist(int id);
    }
}
