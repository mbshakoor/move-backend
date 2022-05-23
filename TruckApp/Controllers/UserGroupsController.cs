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
using TruckApp.Dtos;
using Login.Controllers;

namespace TruckApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserGroupsController : ControllerBase
    {
        private readonly IUserGroupRepository _repo;
        private readonly IConfiguration _config;
        SpecialCharacterValidation SCValidation = new SpecialCharacterValidation();
        public UserGroupsController(IUserGroupRepository repo, IConfiguration config)
        {
            _repo = repo;
            _config = config;
        }

        // GET: api/Roles
        [HttpGet("getusergroup")]
        public async Task<ActionResult<IEnumerable<UserGroup>>> GetUserGroup()
        {
            string json = await _repo.GetUserGroup();
            if (json == null || json == "null" || json == "[]" || json == "")
                return NotFound();
            return Ok(json);
        }

        // GET: api/Roles/5
        [HttpGet("getusergroupbyid/{id}")]
        public async Task<ActionResult<UserGroup>> GetUserGroup(int id)
        {
            UserGroupForIdDto userGroupForIdDto = new UserGroupForIdDto
            {
                id = id
            };
            string json = await _repo.GetUserGroupById(userGroupForIdDto);
            if (json == null || json == "null" || json == "[]" || json == "")
                return NotFound();
            return Ok(json);
        }

        // PUT: api/Roles/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("updateusergroup")]
        public async Task<IActionResult> UpdateUserGroup(UserGroupForUpdateDto userGroupForUpdateDto)
        {
            string json = await _repo.UpdateUserGroup(userGroupForUpdateDto);
            if (json == null || json == "null" || json == "[]" || json == "")
                return NotFound();
            else if (SCValidation.Validate(userGroupForUpdateDto) != "Ok")
                return BadRequest("Only   .?,';:!-@    Special characters are allowed");
            return Ok(json);
        }

        // POST: api/Roles
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost("addusergroup")]
        public async Task<ActionResult<UserGroup>> AddUserGroup(UserGroupForAddDto userGroupForInsertDto)
        {
            string json = await _repo.AddUserGroup(userGroupForInsertDto);
            if (json == null || json == "null" || json == "[]" || json == "")
            {
                return NotFound();
            }
            else if (SCValidation.Validate(userGroupForInsertDto) != "Ok")
                return BadRequest("Only   .?,';:!-@    Special characters are allowed");
            return Ok(json);
        }

        // DELETE: api/Roles/5
        [HttpDelete("deleteusergroup")]
        public async Task<ActionResult<UserGroup>> DeleteUserGroup(int id)
        {
            UserGroupForIdDto userGroupForIdDto = new UserGroupForIdDto
            {
                id = id
            };
            string json = await _repo.DeleteUserGroup(userGroupForIdDto);
            if (json == null || json == "null" || json == "[]" || json == "")
                return NotFound();
            return Ok(json);
        }
    }
}
