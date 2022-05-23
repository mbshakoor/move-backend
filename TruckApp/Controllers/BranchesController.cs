using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Login.Models;
using TruckApp.Models;
using TruckApp.Data;
using Microsoft.Extensions.Configuration;
using Login.Dtos;
using System.Net.Mail;

namespace TruckApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BranchesController : ControllerBase
    {
        private readonly IBranchRepository _repo;
        private readonly IConfiguration _config;
        SpecialCharacterValidation SCValidation = new SpecialCharacterValidation();

        public BranchesController(IBranchRepository repo, IConfiguration config)
        {
            _repo = repo;
            _config = config;
        }

        // GET: api/branchs
        [HttpGet("getbranch")]
        public async Task<ActionResult<IEnumerable<Branch>>> GetBranch()
        {
            string json = await _repo.GetBranch();
            if (json == null || json == "null" || json == "[]" || json == "")
                return NotFound();
            return Ok(json);
        }

        // GET: api/branchs/5
        [HttpGet("getbranchbyid/{id}")]
        public async Task<ActionResult<Branch>> GetBranch(int id)
        {
            BranchForIdDto branchForIdDto = new BranchForIdDto
            {
                Id = id
            };
            string json = await _repo.GetBranchById(branchForIdDto);
            if (json == null || json == "null" || json == "[]" || json == "")
                return NotFound();
            return Ok(json);
        }

        // PUT: api/branchs/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("updatebranch")]
        public async Task<IActionResult> UpdateBranch(BranchForUpdateDto branchForUpdateDto)
        {
            try
            {
                MailAddress m = new MailAddress(branchForUpdateDto.Email);
                string json = await _repo.UpdateBranch(branchForUpdateDto);
                if (json == null || json == "null" || json == "[]" || json == "")
                    return NotFound();
                else if (SCValidation.Validate(branchForUpdateDto) != "Ok")
                    return BadRequest("Only   .?,';:!-@    Special characters are allowed");
                return Ok(json);

            }
            catch (FormatException)
            {
                return Ok("Email Address format is not valid");
            }
        }

        // POST: api/branchs
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost("addbranch")]
        public async Task<ActionResult<Branch>> AddBranch(BranchForAddDto branchForInsertDto)
        {
            try
            {
                MailAddress m = new MailAddress(branchForInsertDto.Email);

                string json = await _repo.AddBranch(branchForInsertDto);
                if (json == null || json == "null" || json == "[]" || json == "")
                {
                    return NotFound();
                }
                else if (SCValidation.Validate(branchForInsertDto) != "Ok")
                    return BadRequest("Only   .?,';:!-@    Special characters are allowed");
                return Ok(json);
            }
            catch (FormatException)
            {
                return Ok("Email Address format is not valid");
            }
        }
        // DELETE: api/branchs/5
        [HttpDelete("deletebranch")]
        public async Task<ActionResult<Branch>> DeleteBranch(int id)
        {
            BranchForIdDto branchForIdDto = new BranchForIdDto
            {
                Id = id
            };
            string json = await _repo.DeleteBranch(branchForIdDto);
            if (json == null || json == "null" || json == "[]" || json == "")
                return NotFound();
            return Ok(json);
        }
    }
}
