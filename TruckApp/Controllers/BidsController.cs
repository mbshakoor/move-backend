using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TruckApp.Data;
using TruckApp.Dtos;
using TruckApp.Models;

namespace TruckApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BidsController : ControllerBase
    {
        private readonly IBidRepository _repo;
        private readonly IConfiguration _config;
        SpecialCharacterValidation SCValidation = new SpecialCharacterValidation();
        public BidsController(IBidRepository repo, IConfiguration config)
        {
            _repo = repo;
            _config = config;
        }

        // GET: api/Bid/5
        [HttpGet("getbid")]
        public async Task<ActionResult<Bid>> GetBid(int branchid)
        {
            string json = await _repo.GetBid(branchid);
            if (json == null || json == "null" || json == "[]" || json == "")
                return NotFound();
            return Ok(json);
        }        // GET: api/Bid
        [HttpGet("getpendingbid")]
        public async Task<ActionResult<IEnumerable<Bid>>> GetPendingBid(int branchid)
        {
            string json = await _repo.GetPendingBid(branchid);
            if (json == null || json == null || json == "[]" || json == "")
                return NotFound();
            return Ok(json);
        }
        [HttpGet("getbidbycustomerid/{customerid}")]
        public async Task<ActionResult<IEnumerable<Bid>>> GetBidByCustomerId(int customerid,int branchid)
        {
            string json = await _repo.GetBidByCustomerId(customerid,branchid);
            if (json == null || json == null || json == "[]" || json == "")
                return NotFound();
            return Ok(json);
        }

        // GET: api/Bid/5
        [HttpGet("getbidbyid")]
        public async Task<ActionResult<Bid>> GetBidById(int id)
        {
            string json = await _repo.GetBidById(id);
            if (json == null || json == "null" || json == "[]" || json == "")
                return NotFound();
            return Ok(json);
        }

        // PUT: api/Bid/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("updatebid")]
        public async Task<IActionResult> UpdateBid(BidForUpdateDto bid)
        {
            if (!await _repo.BidExist(bid.Id))
            {
                return NotFound();
            }
            else if (SCValidation.Validate(bid) != "Ok")
                return BadRequest("Only   .?,';:!-@    Special characters are allowed");
            Bid bidList = new Bid
            {
                Id = bid.Id,
                PriceDetail = bid.PriceDetail,
                QuotedPrice = bid.QuotedPrice,
                UpdateDate=DateTime.Now,
                DeliveryDate = bid.DeliveryDate,
                BranchId = bid.BranchId
            };
            string json = await _repo.UpdateBid(bidList);
            if (json == null || json == null || json == "[]" || json == "")
            {
                return NotFound();
            }
            return Ok(json);
        }

        // POST: api/Bid
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost("addbid")]
        public async Task<ActionResult<Bid>> AddBid(BidForAddDto bid)
        {
            if (SCValidation.Validate(bid) != "Ok")
                return BadRequest("Only   .?,';:!-@    Special characters are allowed");
            Bid bidList = new Bid
            {
                BookingId = bid.BookingId,
                BranchId=bid.BranchId,
                PriceDetail = bid.PriceDetail,
                QuotedPrice = bid.QuotedPrice,
                Status = "Pending",
                VendorId = bid.VendorId,
                CreateDate = DateTime.Now,
                DeliveryDate=bid.DeliverDate
            };
            string json = await _repo.AddBid(bidList);
            if (json == null || json == null || json == "[]" || json == "")
            {
                return NotFound();
            }
            return Ok(json);
        }

        // DELETE: api/Bid/5
        [HttpDelete("deletebid")]
        public async Task<ActionResult<Bid>> DeleteBid(int id)
        {
            string json = await _repo.DeleteBid(id);
            if (json == null || json == null || json == "[]" || json == "")
            {
                return NotFound();
            }
            return Ok(json);
        }

    }
}
