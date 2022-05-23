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
    public class LoadTypesController : ControllerBase
    {
        private readonly ILoadTypeRepository _repo;
        private readonly IConfiguration _config;
        SpecialCharacterValidation SCValidation = new SpecialCharacterValidation();
        public LoadTypesController(ILoadTypeRepository repo, IConfiguration config)
        {
            _repo = repo;
            _config = config;
        }

        // GET: api/LoadTypes
        [HttpGet("getloadtype")]
        public async Task<ActionResult<IEnumerable<LoadType>>> GetLoadType()
        {
            string json = await _repo.GetLoadType();
            if (json == null || json == "null" || json == "[]" || json == "")
                return NotFound();
            return Ok(json);
        }

        // GET: api/LoadTypes/5
        [HttpGet("getloadtypebyid/{id}")]
        public async Task<ActionResult<LoadType>> GetLoadType(int id)
        {
            string json = await _repo.GetLoadTypeById(id);
            if (json == null || json == "null" || json == "[]" || json == "")
                return NotFound();
            return Ok(json);
        }

        [HttpPut("updateloadtype")]
        public async Task<IActionResult> PutLoadType(LoadTypeUpdateForDto loadTypeUpdateForDto)
        {
            if (!await _repo.LoadTypeExist(loadTypeUpdateForDto.Id))
            {
                return NotFound();
            }
            LoadType loadType = new LoadType
            {
                Id = loadTypeUpdateForDto.Id,
                Name = loadTypeUpdateForDto.Name
            };
            string json = await _repo.UpdateLoadType(loadType);
            if (json == null || json == "null" || json == "[]" || json == "")
                return NotFound();
            else if (SCValidation.Validate(loadTypeUpdateForDto) != "Ok")
                return BadRequest("Only   .?,';:!-@    Special characters are allowed");
            return Ok(json);
        }

        [HttpPost("addloadtype")]
        public async Task<ActionResult<LoadType>> AddLoadType(LoadTypeAddForDto loadTypeAddForDto)
        {
            LoadType loadType = new LoadType
            {
                Name = loadTypeAddForDto.Name,
                IsActive = true
            };
            string json = await _repo.AddLoadType(loadType);
            if (json == null || json == "null" || json == "[]" || json == "")
                return NotFound();
            else if (SCValidation.Validate(loadTypeAddForDto) != "Ok")
                return BadRequest("Only   .?,';:!-@    Special characters are allowed");
            return Ok(json);
        }

        // DELETE: api/LoadTypes/5
        [HttpDelete("deleteloadtype/{id}")]
        public async Task<ActionResult<LoadType>> DeleteLoadType(int id)
        {
            if (!await _repo.LoadTypeExist(id))
            {
                return NotFound();
            }
            string json = await _repo.DeleteLoadType(id);
            if (json == null || json == "null" || json == "[]" || json == "")
                return NotFound();
            return Ok(json);

        }
    }
}
