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
    public class RolesController : ControllerBase
    {
        private readonly IRoleRepository _repo;
        private readonly IConfiguration _config;
        SpecialCharacterValidation SCValidation = new SpecialCharacterValidation();
        public RolesController(IRoleRepository repo, IConfiguration config)
        {
            _repo = repo;
            _config = config;
        }

        // GET: api/Roles
        [HttpGet("getrole")]
        public async Task<ActionResult<IEnumerable<Role>>> GetRole()
        {
            string json = await _repo.GetRole();
            if (json == null || json == "null" || json == "[]" || json == "")
                return NotFound();
            return Ok(json);
        }

        // GET: api/Roles/5
        [HttpGet("getrolebyid/{id}")]
        public async Task<ActionResult<Role>> GetRole(int id)
        {
            RoleForIdDto roleForIdDto = new RoleForIdDto
            {
                id = id
            };
            string json = await _repo.GetRoleById(roleForIdDto);
            if (json == null || json == "null" || json == "[]" || json == "")
                return NotFound();
            return Ok(json);
        }

        // PUT: api/Roles/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("updaterole")]
        public async Task<IActionResult> UpdateRole(RoleForUpdateDto roleForUpdateDto)
        {
            string json = await _repo.UpdateRole(roleForUpdateDto);
            if (json == null || json == "null" || json == "[]" || json == "")
                return NotFound();
            else if (SCValidation.Validate(roleForUpdateDto) != "Ok")
                return BadRequest("Only   .?,';:!-@    Special characters are allowed");
            return Ok(json);
        }

        // POST: api/Roles
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost("addrole")]
        public async Task<ActionResult<Role>> AddRole(RoleForInsertDto roleForInsertDto)
        {
            string json = await _repo.AddRole(roleForInsertDto);
            if (json == null || json == "null" || json == "[]" || json == "")
            {
                return NotFound();
            }
            else if (SCValidation.Validate(roleForInsertDto) != "Ok")
                return BadRequest("Only   .?,';:!-@    Special characters are allowed");
            return Ok(json);
        }

        // DELETE: api/Roles/5
        [HttpDelete("deleterole")]
        public async Task<ActionResult<Role>> DeleteRole(int id)
        {
            RoleForIdDto roleForIdDto = new RoleForIdDto
            {
                id = id
            };
            string json = await _repo.DeleteRole(roleForIdDto);
            if (json == null || json == "null" || json == "[]" || json == "")
                return NotFound();
            return Ok(json);
        }
    }
}
