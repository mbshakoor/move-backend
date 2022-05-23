using Login.Controllers;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TruckApp.Dtos;
using TruckApp.Models;

namespace TruckApp.Data
{
    public interface IUserGroupRepository
    {
        Task<string> GetUserGroup();
        Task<string> GetUserGroupById(UserGroupForIdDto UserGroupForIdDto);
        Task<string> AddUserGroup(UserGroupForAddDto UserGroupForInsertDto);
        Task<string> UpdateUserGroup(UserGroupForUpdateDto UserGroupForUpdateDto);
        Task<string> DeleteUserGroup(UserGroupForIdDto UserGroupForIdDto);
    }
}
