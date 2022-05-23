using System;
using System.Collections.Generic;
using System.Linq;
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
    public class FeedbacksController : ControllerBase
    {
        private readonly IFeedbackRepository _repo;
        private readonly IConfiguration _config;
        SpecialCharacterValidation SCValidation = new SpecialCharacterValidation();

        public FeedbacksController(IFeedbackRepository repo, IConfiguration config)
        {
            _repo = repo;
            _config = config;
        }

        // GET: api/Feedbacks
        [HttpGet("getfeedback")]
        public async Task<ActionResult<IEnumerable<Feedback>>> GetFeedback()
        {
            string json = await _repo.GetFeedback();
            if (json == null || json == "null" || json == "" || json == "[]")
            {
                return NotFound();
            }
            return Ok(json);
        }

        // GET: api/Feedbacks/5
        [HttpGet("getfeedbackbyuserid/{userid}")]
        public async Task<ActionResult<Feedback>> GetFeedbackByUserId(int userid)
        {
            string json = await _repo.GetFeedbackById(userid);
            if (json == null || json == "null" || json == "" || json == "[]")
            {
                return NotFound();
            }
            return Ok(json);
        }
        // GET: api/Feedbacks/5
        [HttpGet("getfeedbackbyid/{id}")]
        public async Task<ActionResult<Feedback>> GetFeedbackById(int id)
        {
            string json = await _repo.GetFeedbackById(id);
            if (json == null || json == "null" || json == "" || json == "[]")
            {
                return NotFound();
            }
            return Ok(json);
        }

        [HttpPost("addfeedback")]
        public async Task<ActionResult<Feedback>> AddFeedback(AddFeedbackForDto feedbackForDto)
        {
            Feedback feedback = new Feedback
            {
                FeedbackTypeId = feedbackForDto.FeedbackTypeId,
                Suggestion = feedbackForDto.Suggestion,
                CreateDate = feedbackForDto.CreateDate,
                CreatedBy = feedbackForDto.CreatedBy
            };
            string json = await _repo.AddFeedback(feedback);
            if (json == null || json == "null" || json == "[]" || json == "")
            {
                return NotFound();
            }
            else if (SCValidation.Validate(feedbackForDto) != "Ok")
                return BadRequest("Only   .?,';:!-@    Special characters are allowed");

            return Ok(json);
        }
        [HttpPut("updatefeedback")]
        public async Task<IActionResult> UpdateFeedback(FeedbackForUpdateDto feedbackForUpdateDto)
        {
            string json = "";
            if (! await _repo.FeedbackExist(feedbackForUpdateDto.Id))
            {
                return NotFound();
            }
            else if (SCValidation.Validate(feedbackForUpdateDto) != "Ok")
                return BadRequest("Only   .?,';:!-@    Special characters are allowed");

            Feedback feedback = new Feedback
            {
                Id = feedbackForUpdateDto.Id,
                FeedbackTypeId = feedbackForUpdateDto.FeedbackTypeId,
                Suggestion = feedbackForUpdateDto.Suggestion,
                UpdateDate = feedbackForUpdateDto.UpdateDate,
                UpdatedBy = feedbackForUpdateDto.UpdatedBy
            };
            try
            {
                json = await _repo.UpdateFeedback(feedback);
                if(json==null || json=="null" || json == "[]" || json == "")
                {
                    return NotFound();
                }
            }
            catch (DbUpdateConcurrencyException)
            {
            }
            return Ok(json);

        }
    }
}
