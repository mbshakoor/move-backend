using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TruckApp.Dtos;
using TruckApp.Models;

namespace TruckApp.Data
{
    public interface IBookingRepository
    {
        //Task<List<Booking>> GetBooking();
        Task<string> GetBookingByCustomerId(int customerid,int BranchId);
        Task<Booking> GetBookingById(string Id);
        Task<string> AddBooking(Booking booking);
        Task<string> UpdateBooking(Booking booking);
        Task<string> AddPromoCode(Booking booking);
        Task<string> DeleteBooking(string Id,string Reason);
        Task<bool> BookingExist(string id);
        Task<string> GetBookingForBid(int CustomerId,int BranchId);


    }
}
