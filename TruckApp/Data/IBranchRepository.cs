using Login.Dtos;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TruckApp.Dtos;
using TruckApp.Models;

namespace TruckApp.Data
{
    public interface IBranchRepository
    {
        Task<string> GetBranch();
        Task<string> GetBranchById(BranchForIdDto branchForIdDto);
        Task<string> AddBranch(BranchForAddDto branchForInsertDto);
        Task<string> UpdateBranch(BranchForUpdateDto branchForUpdateDto);
        Task<string> DeleteBranch(BranchForIdDto branchForIdDto);
    }
}
