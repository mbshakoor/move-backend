using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using TruckApp.Data;
using TruckApp.Dtos;
using TruckApp.Models;

namespace TruckApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly IBookingRepository _repo;
        private readonly IConfiguration _config;
        SpecialCharacterValidation SCValidation = new SpecialCharacterValidation();
        public BookingsController(IBookingRepository repo, IConfiguration config)
        {
            _repo = repo;
            _config = config;
        }

        //// GET: api/Bookings
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Booking>>> GetBooking()
        //{
        //    var booking = await _repo.GetBooking();
        //    if (booking == null)
        //        NotFound();
        //    string json = JsonConvert.SerializeObject(booking);
        //    return Ok(json);
        //}

        [HttpGet("getbookingbycustomerid/{customerid}")]
        public async Task<ActionResult<IEnumerable<Booking>>> GetBookingByCustomerId(int customerid,int branchid)
        {
            var booking = await _repo.GetBookingByCustomerId(customerid,branchid);
            if (booking == null || booking == "null" || booking == "[]" || booking == "")
            {
                NotFound();
            }
            string json = JsonConvert.SerializeObject(booking);
            return Ok(json);
        }   
        // GET: api/Bookings/5
        [HttpGet("getbookingbyid/{id}")]
        public async Task<ActionResult<Booking>> GetBookingById(string id)
        {
            var booking = await _repo.GetBookingById(id);

            if (booking == null)
            {
                return NotFound();
            }
            string json = JsonConvert.SerializeObject(booking);
            return Ok(json);
        }

        // PUT: api/Bookings/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("updatebooking")]
        public async Task<IActionResult> UpdateBooking(BookingForUpdateDtos bookingForUpdateDtos)
        {
            if (! await _repo.BookingExist(bookingForUpdateDtos.Id))
            {
                return NotFound();
            }
            else
            {

                var bookingToUpdate = new Booking
                {
                    Id = bookingForUpdateDtos.Id,
                    PickupLat = bookingForUpdateDtos.PickupLat,
                    PickupLong = bookingForUpdateDtos.PickupLong,
                    PickupAddress = bookingForUpdateDtos.PickupAddress,
                    PickupLocationType = bookingForUpdateDtos.PickupLocationType,
                    PickupPersonName = bookingForUpdateDtos.PickupPersonName,
                    PickupPersonNumber = bookingForUpdateDtos.PickupPersonNumber,
                    DropOffLat = bookingForUpdateDtos.DropOffLat,
                    DropOffLong = bookingForUpdateDtos.DropOffLong,
                    DropOffAddress = bookingForUpdateDtos.DropOffAddress,
                    DropOffLocationType = bookingForUpdateDtos.DropOffLocationType,
                    DropOffPersonName = bookingForUpdateDtos.DropOffPersonName,
                    DropOffPersonNumber = bookingForUpdateDtos.DropOffPersonNumber,
                    PaymentVia = bookingForUpdateDtos.PaymentVia
                };
                var bookingList = await _repo.UpdateBooking(bookingToUpdate);
                string json = JsonConvert.SerializeObject(bookingList);
                return Ok(json);
            }
        }
        [HttpPut("addpromocode")]
        public async Task<ActionResult<Booking>> AddPromoCode(BookingForPromoCode bookingForPromoCode)
        {
            
            if (!await _repo.BookingExist(bookingForPromoCode.Id))
            {
                return NotFound();
            }
            else if (SCValidation.Validate(bookingForPromoCode) != "Ok")
                return BadRequest("Only   .?,';:!-@    Special characters are allowed");
            else
            {
                var booking = new Booking
                {
                    Id = bookingForPromoCode.Id,
                    PromoCode = bookingForPromoCode.PromoCode
                };
                string json = await _repo.AddPromoCode(booking);
                return Ok(json);
            }
        }
        // POST: api/Bookings
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost("addbooking")]
        public async Task<ActionResult<Booking>> AddBooking(BookingForAdd bookingForAdd)
        {
            var bookingToInsert = new Booking
            {
                CategoryId = bookingForAdd.CategoryId,
                CustomerId = bookingForAdd.CustomerId,
                BranchId = bookingForAdd.BranchId,
                PickupLat = bookingForAdd.PickupLat,
                PickupLong = bookingForAdd.PickupLong,
                PickupAddress = bookingForAdd.PickupAddress,
                PickupLocationType = bookingForAdd.PickupLocationType,
                PickupPersonName = bookingForAdd.PickupPersonName,
                PickupPersonNumber = bookingForAdd.PickupPersonNumber,
                DropOffLat = bookingForAdd.DropOffLat,
                DropOffLong = bookingForAdd.DropOffLong,
                DropOffAddress = bookingForAdd.DropOffAddress,
                DropOffLocationType = bookingForAdd.DropOffLocationType,
                DropOffPersonName = bookingForAdd.DropOffPersonName,
                DropOffPersonNumber = bookingForAdd.DropOffPersonNumber,
                DelivaryDateFrom = bookingForAdd.DelivaryDateFrom,
                DeliveryDateTo = bookingForAdd.DeliveryDateTo,
                LoadType = bookingForAdd.LoadType,
                EstimateWeight = bookingForAdd.EstimateWeight,
                WeightUnit = bookingForAdd.WeightUnit,
                GoodsDescription = bookingForAdd.GoodsDescription,
                PaymentVia = bookingForAdd.PaymentVia,
                PromoCode = bookingForAdd.PromoCode,
                EstimatedPrice = bookingForAdd.EstimatedPrice,
                CreateDate = bookingForAdd.CreateDate,
                Status = bookingForAdd.Status,
                Reason = bookingForAdd.Reason,
                IsBooked = bookingForAdd.IsBooked,
                VendorId = bookingForAdd.VendorId
            };
            
            var bookingList = await _repo.AddBooking(bookingToInsert);
            string json = JsonConvert.SerializeObject(bookingList);
            if (json == null || json == "[]" || json == "")
            {
                return NotFound();
            }
            return Ok(json);
        }
        
        // DELETE: api/Bookings/5
        [HttpDelete("deletebooking/{id}/{reason}")]
        public async Task<ActionResult<Booking>> DeleteBooking(string id, string reason)
        {
            if (!await _repo.BookingExist(id))
            {
                return NotFound();
            }
            var booking = await _repo.DeleteBooking(id, reason);
            if (booking == null)
            {
                return NotFound();
            }

            string json = JsonConvert.SerializeObject(booking);

            return Ok(json);
        }
        [HttpGet("getbookingforbid/{customerid}")]
        public async Task<ActionResult<Booking>> GetBookingForBid(int customerid, int branchid)
        {
            string booking = await _repo.GetBookingForBid(customerid,branchid);
            if (booking == null || booking == "[]")
            {
                return NotFound();
            }
            return Ok(booking);
        }
    }
}
