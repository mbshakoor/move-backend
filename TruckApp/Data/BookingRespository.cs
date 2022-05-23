using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using TruckApp.Dtos;
using TruckApp.Models;

namespace TruckApp.Data
{
    public class BookingRespository : IBookingRepository
    {
        private readonly DataContext _context;
        public BookingRespository(DataContext context)
        {
            _context = context;
        }
        public async Task<string> AddBooking(Booking booking)
        {
            string bookingData = "";
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var abc = await _context.Booking.ToListAsync();
                    var lastBookingRecord = await _context.Booking.OrderByDescending(x => x.Id).FirstOrDefaultAsync();

                    //================Add Booking===============================
                    string bookingId = lastBookingRecord == null ? "BKG-1" : "BKG-" + (int.Parse(lastBookingRecord.Id.Split('-')[lastBookingRecord.Id.Split('-').Length - 1].ToString()) + 1);
                    booking.Id = bookingId;
                    await _context.Booking.AddAsync(booking);
                    int result = await _context.SaveChangesAsync();
                    //================End Add Booking===========================

                    //================Update Category Status====================
                    var categoryList = await _context.CategoryDetail.FirstOrDefaultAsync(x => x.Id == booking.CategoryId);
                    categoryList.Status = "Booked";
                    _context.Entry(categoryList).State = EntityState.Modified;
                    int resultCategory = await _context.SaveChangesAsync();
                    //================Update Category Status====================

                    //bookingData = await _context.Booking.ToListAsync<Booking>();
                    bookingData = await GetBookingByCustomerId(booking.CustomerId,booking.BranchId);
                    transaction.Commit();
                }
                catch (Exception ex)
                {

                }
                return bookingData;
            }
        }

        public async Task<string> DeleteBooking(string Id, string Reason)
        {
            string bookingData = "";
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var bookingList = await _context.Booking.FirstOrDefaultAsync(x => x.Id.Equals(Id));
                    bookingList.Status = "0";
                    bookingList.Reason = Reason;
                    _context.Entry(bookingList).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                    bookingData = await GetBookingByCustomerId(bookingList.CustomerId,bookingList.BranchId);




                    //================Update Category Status====================
                    //Note: parameter for CategoryId is CategoryDetailId
                    var categoryList = await _context.CategoryDetail.FirstOrDefaultAsync(x => (x.CategoryId == bookingList.CategoryId));
                    categoryList.Status = "Available";
                    _context.Entry(categoryList).State = EntityState.Modified;
                    int resultCategory = await _context.SaveChangesAsync();
                    //================Update Category Status====================
                    transaction.Commit();
                }
                catch (DbUpdateConcurrencyException)
                {
                }
                return bookingData;
            }
        }

        //public async Task<List<Booking>> GetBooking()
        //{
        //    return await _context.Booking.ToListAsync<Booking>();
        //}
        public async Task<string> GetBookingByCustomerId(int customerid,int branchid)
        {
            var booking = await (from book in _context.Booking
                                 join category in _context.Category on book.CategoryId equals category.Id
                                 join loadType in _context.LoadType on book.LoadType equals loadType.Id
                                 join weightUnit in _context.WeightUnit on book.WeightUnit equals weightUnit.Id
                                 select new
                                 {
                                     CategoryName = category.Name,
                                     LoadType = loadType.Name,
                                     WeightUnit = weightUnit.Name,
                                     book.CustomerId,
                                     book.GoodsDescription,
                                     book.PaymentVia,
                                     book.PickupAddress,
                                     book.PickupLat,
                                     book.PickupLocationType,
                                     book.PickupLong,
                                     book.PickupPersonName,
                                     book.PickupPersonNumber,
                                     book.PromoCode,
                                     book.Status,
                                     book.Reason,
                                     book.CreateDate,
                                     book.DelivaryDateFrom,
                                     book.DeliveryDateTo,
                                     book.DropOffAddress,
                                     book.DropOffLat,
                                     book.DropOffLocationType,
                                     book.DropOffLong,
                                     book.DropOffPersonName,
                                     book.DropOffPersonNumber,
                                     book.EstimatedPrice,
                                     book.EstimateWeight,
                                     book.BranchId
                                 })
                     .Where(x => x.CustomerId == customerid)
                     .Where(x => x.BranchId == (branchid == 0 ? x.BranchId : branchid))
                     .ToListAsync();
            string json = JsonConvert.SerializeObject(booking);
            return json;
        }
        public async Task<Booking> GetBookingById(string Id)
        {
            return await _context.Booking.FirstOrDefaultAsync(x => x.Id.Equals(Id));
        }
        public async Task<string> GetBookingForBid(int CustomerId,int BranchId)
        {
            var booking = await (from book in _context.Booking
                                 join category in _context.Category on book.CategoryId equals category.Id
                                 join loadType in _context.LoadType on book.LoadType equals loadType.Id
                                 join weightUnit in _context.WeightUnit on book.WeightUnit equals weightUnit.Id
                                 select new
                                 {
                                     book,
                                     CategoryName = category.Name,
                                     LoadType = loadType.Name,
                                     WeightUnit = weightUnit.Name
                                 }).Where(x => x.book.BranchId == (BranchId == 0 ? x.book.BranchId : BranchId)).ToListAsync();
            string json = JsonConvert.SerializeObject(booking);
            return json;
            //var booking = _context.Booking.Where(x => x.CustomerId == CustomerId && x.Status.Equals("1") && x.IsBooked == false).ToListAsync();

        }
        public async Task<string> UpdateBooking(Booking booking)
        {
            string bookingData = "";
            string json = "";
            var bookingList = await _context.Booking.FirstOrDefaultAsync(x => x.Id == booking.Id);
            bookingList.PickupLat = booking.PickupLat;
            bookingList.PickupLong = booking.PickupLong;
            bookingList.PickupAddress = booking.PickupAddress;
            bookingList.PickupLocationType = booking.PickupLocationType;
            bookingList.PickupPersonName = booking.PickupPersonName;
            bookingList.PickupPersonNumber = booking.PickupPersonNumber;
            bookingList.DropOffLat = booking.DropOffLat;
            bookingList.DropOffLong = booking.DropOffLong;
            bookingList.DropOffAddress = booking.DropOffAddress;
            bookingList.DropOffLocationType = booking.DropOffLocationType;
            bookingList.DropOffPersonName = booking.DropOffPersonName;
            bookingList.DropOffPersonNumber = booking.DropOffPersonNumber;
            bookingList.PaymentVia = booking.PaymentVia;
            _context.Entry(bookingList).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
                bookingData = await GetBookingByCustomerId(bookingList.CustomerId,bookingList.BranchId);
            }
            catch (DbUpdateConcurrencyException)
            {
            }

            return bookingData;
        }
        public async Task<bool> BookingExist(string id)
        {
            bool bookingExist = await _context.Booking.AnyAsync(x => x.Id.Equals(id));
            return bookingExist;
        }

        public async Task<string> AddPromoCode(Booking booking)
        {
            string bookingData = "";
            var BookingList = await _context.Booking.FirstOrDefaultAsync(x => x.Id == booking.Id);
            BookingList.PromoCode = booking.PromoCode;
            _context.Entry(BookingList).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
                bookingData = await GetBookingByCustomerId(booking.CustomerId, BookingList.BranchId);
            }
            catch (DbUpdateConcurrencyException)
            {
            }
            return bookingData;

        }
    }
}
