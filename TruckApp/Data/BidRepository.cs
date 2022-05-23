using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using TruckApp.Controllers;
using TruckApp.Models;

namespace TruckApp.Data
{
    public class BidRepository : IBidRepository
    {
        private readonly DataContext _context;
        public BidRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<string> GetBid(int branchid)
        {
            var bid = await _context.Bid.Where(x=>x.BranchId == (branchid == 0 ? x.BranchId : branchid)).ToListAsync();
            //var bidList = (from bid in _context.Bid
            //               select bid
            //
            //           ).Where(x => x.BranchId == (branchid == 0 ? x.BranchId : branchid)).ToListAsync();
            string json = JsonConvert.SerializeObject(bid);
            return json;
        }
        public async Task<string> GetPendingBid(int branchid)
        {
            var bid = await _context.Bid.Where(x => (x.Status == "Pending") && (x.BranchId == (branchid == 0 ? x.BranchId : branchid))).ToListAsync();
            string json = JsonConvert.SerializeObject(bid);
            return json;
        }
        public async Task<string> GetBidByCustomerId(int customerid,int branchid)
        {
            var booking = await (from bids in _context.Bid
                                 join book in _context.Booking on bids.BookingId equals book.Id
                                 select new
                                 {
                                     bids.BookingId,
                                     bids.Id,
                                     bids.PriceDetail,
                                     bids.QuotedPrice,
                                     bids.Status,
                                     bids.UpdateDate,
                                     bids.VendorId,
                                     bids.CreateDate,
                                     bids.DeliveryDate,
                                     book.CustomerId,
                                     bids.BranchId
                                 }
                                 ).Where(x => (x.CustomerId == customerid) && (x.BranchId == (branchid == 0 ? x.BranchId : branchid))).ToListAsync();
            string json = JsonConvert.SerializeObject(booking);

            var bid = await _context.Bid.ToListAsync();
            //string json = JsonConvert.SerializeObject(bid);
            return json;
        }
        public async Task<string> GetBidByVendorId(int vendorId,int branchid)
        {
            var booking = await (from bids in _context.Bid
                                 join book in _context.Booking on bids.BookingId equals book.Id
                                 select new
                                 {
                                     bids.BookingId,
                                     bids.Id,
                                     bids.PriceDetail,
                                     bids.QuotedPrice,
                                     bids.Status,
                                     bids.UpdateDate,
                                     bids.VendorId,
                                     bids.CreateDate,
                                     bids.DeliveryDate,
                                     bids.BranchId,
                                     book.CustomerId
                                 }
                                 ).Where(x => (x.VendorId == vendorId) && (x.BranchId == (x.BranchId == 0 ? x.BranchId : branchid))).ToListAsync();
            string json = JsonConvert.SerializeObject(booking);

            var bid = await _context.Bid.ToListAsync();
            //string json = JsonConvert.SerializeObject(bid);
            return json;
        }
        public async Task<string> GetBidById(int id)
        {
            var bid = await _context.Bid.FirstOrDefaultAsync(x => x.Id == id);
            string json = JsonConvert.SerializeObject(bid);
            return json;
        }
        public async Task<string> AddBid(Bid bid)
        {
            await _context.Bid.AddAsync(bid);
            int result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                string bidList = await GetPendingBid(bid.BranchId);
                return bidList;
            }
            return "[]";
        }
        public async Task<string> UpdateBid(Bid bid)
        {
            //var bidData = new List<Bid>();
            string bidData = "";
            //string json = "[]";
            var bidList = await _context.Bid.FirstOrDefaultAsync(x => x.Id == bid.Id);
            if (!bidList.Status.Equals("Rejected"))
            {
                bidList.DeliveryDate = bid.DeliveryDate;
                bidList.PriceDetail = bid.PriceDetail;
                bidList.QuotedPrice = bid.QuotedPrice;
                bidList.UpdateDate = DateTime.Now;
                _context.Entry(bidList).State = EntityState.Modified;
                try
                {
                    int result = await _context.SaveChangesAsync();
                    if (result > 0)
                    {
                        bidData = await GetPendingBid(0);
                        //bidData = await _context.Bid.ToListAsync();
                        //json = JsonConvert.SerializeObject(bidData);
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                }
            }
            return bidData;
        }
        public async Task<string> DeleteBid(int id)
        {
            //string json = "[]";
            string bidList = "";
            var bid = await _context.Bid.FirstOrDefaultAsync(x => x.Id == id);
            bid.Status = "Rejected";
            _context.Entry(bid).State = EntityState.Modified;
            try
            {
                int result = await _context.SaveChangesAsync();
                if (result > 0)
                {
                    bidList = await GetPendingBid(bid.BranchId);
                    //var bidList = _context.Bid.ToListAsync();
                    //json = JsonConvert.SerializeObject(bidList);
                }
            }
            catch (DbUpdateConcurrencyException) { }
            return bidList;
        }
        public async Task<bool> BidExist(int id)
        {
            var bid = await _context.Bid.AnyAsync(x => x.Id == id);
            return bid;
        }

    }
}
