using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TruckApp.Dtos;
using TruckApp.Models;

namespace TruckApp.Data
{
    public interface IRoleRepository
    {
        Task<string> GetRole();
        Task<string> GetRoleById(RoleForIdDto roleForIdDto);
        Task<string> AddRole(RoleForInsertDto roleForInsertDto);
        Task<string> UpdateRole(RoleForUpdateDto roleForUpdateDto);
        Task<string> DeleteRole(RoleForIdDto roleForIdDto);
    }
}
