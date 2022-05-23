using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using TruckApp.Models;

namespace TruckApp.Data
{
    public interface IBidRepository
    {
        Task<string> GetBid(int branchid);
        Task<string> GetPendingBid(int branchid);
        Task<string> GetBidByCustomerId(int customerid,int branchid);
        Task<string> GetBidByVendorId(int vendorid, int branchid);
        Task<string> GetBidById(int id);
        Task<string> AddBid(Bid bid);
        Task<string> UpdateBid(Bid bid);
        Task<string> DeleteBid(int id);
        Task<bool> BidExist(int id);

    }
}
