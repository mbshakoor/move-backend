using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
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
    public class UsersController : ControllerBase
    {
        //private readonly DataContext _context;
        private readonly IUserRepository _repo;
        private readonly IConfiguration _config;
        SpecialCharacterValidation SCValidation = new SpecialCharacterValidation();
        public UsersController(IUserRepository repo, IConfiguration config)
        {
            _repo = repo;
            _config = config;
            //_context = context;
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(UserForLoginDto userForLoginDto)
        {
            var userFromRepo = await _repo.Login(userForLoginDto);

            if (userFromRepo == null)
                return Unauthorized();
            else if (SCValidation.Validate(userForLoginDto) != "Ok")
                return BadRequest("Only   .?,';:!-@    Special characters are allowed");
            string json = JsonConvert.SerializeObject(userFromRepo);

            return Ok(json);
        }
        [HttpGet("getusers")]
        public async Task<IActionResult> GetUsers()
        {
            var user = await _repo.GetUsers();
            //string json = JsonConvert.SerializeObject(User);
            return Ok(user);
        }
        [HttpPut("assignusergroup")]
        public async Task<IActionResult> AssignUserGroup(UserForUserGroupForDto assignBranchAndUserGroupForDto)
        {
            string json = await _repo.AssignUserGroup(assignBranchAndUserGroupForDto);
            if (json == null || json == "null" || json == "[]" || json == "")
            {
                return NotFound();
            }
            return Ok(json);
        }
        [HttpPut("assignbranch")]
        public async Task<IActionResult> AssignBranch(UserForBranchForDto userForBranchForDto)
        {
            string json = await _repo.AssignBranch(userForBranchForDto);
            if (json == null || json == "null" || json == "[]" || json == "")
            {
                return NotFound();
            }
            return Ok(json);
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegisterDto userForRegisterDto)
        {
            try
            {
                MailAddress m = new MailAddress(userForRegisterDto.EmailAddress);
                if (SCValidation.Validate(userForRegisterDto) != "Ok")
                    return BadRequest("Only   .?,';:!-@    Special characters are allowed");
                var user = await _repo.Register(userForRegisterDto);
                return Ok(user);
            }
            catch (FormatException)
            {
                return Ok("Email Address Format is not valid");
            }
        }
        [HttpPost("deactiveuser")]
        public async Task<IActionResult> DeactiveUser(UserForDeactiveDto userForDeactiveDto)
        {
            var user = await _repo.DeactiveUser(userForDeactiveDto);
            return Ok();
        }
        [HttpGet("resetpassword")]
        public async Task<IActionResult> ResetPassword(string Username, string OldPassword, string NewPassword)
        {
            var user = await _repo.ResetPassword(Username, OldPassword, NewPassword);
            return Ok(user);
        }
        [HttpGet("validateotp")]
        public async Task<IActionResult> ValidateOTP(string Username, string OTP)
        {
            var user = await _repo.ValidateOTP(Username, OTP);
            return Ok(user);
        }
        [HttpGet("forgotpassword")]
        public async Task<IActionResult> ForgotPassword(string Username)
        {
            var user = await _repo.ForgotPassword(Username);
            return Ok(user);
        }
        [HttpGet("resetpasswordwithoutoldpassword")]
        public async Task<IActionResult> ResetPasswordWithoutOldPassword(string Username, string NewPassword)
        {
            var user = await _repo.ResetPasswordWithoutOldPassword(Username,NewPassword);
            return Ok(user);
        }
    }
}
