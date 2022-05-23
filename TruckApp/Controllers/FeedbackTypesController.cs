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
    public class FeedbackTypesController : ControllerBase
    {
        private readonly IFeedbackTypeRepository _repo;
        private readonly IConfiguration _config;
        SpecialCharacterValidation SCValidation = new SpecialCharacterValidation();
        public FeedbackTypesController(IFeedbackTypeRepository repo, IConfiguration config)
        {
            _repo = repo;
            _config = config;
        }

        // GET: api/FeedbackTypes
        [HttpGet("getfeedbacktype")]
        public async Task<ActionResult<IEnumerable<FeedbackType>>> GetFeedbackType()
        {
            string json = await _repo.GetFeedbackType();
            if (json == null || json == "null" || json == "[]" || json == "")
            {
                return NotFound();
            }
            return Ok(json);
        }

        // GET: api/FeedbackTypes/5
        [HttpGet("getfeedbacktypebyid/{id}")]
        public async Task<ActionResult<FeedbackType>> GetFeedbackTypeId(int id)
        {
            string json = await _repo.GetFeedbackTypebyId(id);

            if (json == null || json == "null" || json == "[]" || json == "")
            {
                return NotFound();
            }

            return Ok(json);
        }

        [HttpPut("updatefeedbacktype")]
        public async Task<IActionResult> UpdateFeedback(FeedbackTypeForUpdateDto feedbackTypeForUpdateDto)
        {

            if (!await _repo.FeedbackTypeExist(feedbackTypeForUpdateDto.Id))
            {
                return NotFound();
            }

            FeedbackType feedback = new FeedbackType
            {
                Id = feedbackTypeForUpdateDto.Id,
                Name = feedbackTypeForUpdateDto.Name
            };
            
            string json = await _repo.UpdateFeedbackType(feedback);
            if (json == null || json == "null" || json == "[]" || json == "")
            {
                return NotFound();
            }
            else if (SCValidation.Validate(feedbackTypeForUpdateDto) != "Ok")
                return BadRequest("Only   .?,';:!-@    Special characters are allowed");
            return Ok(json);
        }

        [HttpPost("addfeedbacktype")]
        public async Task<ActionResult<FeedbackType>> AddFeedbackType(FeedbackTypeForInsertDto feedbackTypeForInsertDto)
        {
            FeedbackType feedbackType = new FeedbackType
            {
                Name = feedbackTypeForInsertDto.Name
            };
            string json = await _repo.AddFeedbackType(feedbackType);
            if (json == null || json == "null" || json == "[]" || json == "")
            {
                return NotFound();
            }
            else if (SCValidation.Validate(feedbackTypeForInsertDto) != "Ok")
                return BadRequest("Only   .?,';:!-@    Special characters are allowed");
            return Ok(json);
        }

        // DELETE: api/FeedbackTypes/5
        [HttpDelete("deletefeedbacktype")]
        public async Task<ActionResult<FeedbackType>> DeleteFeedbackType(int id)
        {
            string json = await _repo.DeleteFeedbackType(id);
            if (json == null || json == "null" || json == "[]" || json == "")
            {
                return NotFound();
            }
            return Ok(json);

        }
    }
}
