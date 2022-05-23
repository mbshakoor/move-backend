using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TruckApp.Data;
using TruckApp.Models;

namespace TruckApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeightUnitsController : ControllerBase
    {
        private readonly IWeightUnitRepository _repo;
        private readonly IConfiguration _config;
        SpecialCharacterValidation SCValidation = new SpecialCharacterValidation();
        public WeightUnitsController(IWeightUnitRepository repo, IConfiguration config)
        {
            _repo = repo;
            _config = config;
        }

        // GET: api/Roles
        [HttpGet("getweightunit")]
        public async Task<ActionResult<IEnumerable<WeightUnit>>> GetWeightUnit()
        {
            string json = await _repo.GetWeightUnit();
            if (json == null || json == "null" || json == "[]" || json == "")
                return NotFound();
            return Ok(json);
        }

        // GET: api/Roles/5
        [HttpGet("getweightunitbyid/{id}")]
        public async Task<ActionResult<WeightUnit>> GetWeightUnit(int id)
        {
            string json = await _repo.GetWeightUnitById(id);
            if (json == null || json == "null" || json == "[]" || json == "")
                return NotFound();
            return Ok(json);
        }

        // PUT: api/Roles/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("updateweightunit")]
        public async Task<IActionResult> UpdateWeightUnit(WeightUnitUpdateForDto weightUnitUpdateForDto)
        {
            WeightUnit weightunit = new WeightUnit
            {
                Id = weightUnitUpdateForDto.Id,
                Name = weightUnitUpdateForDto.Name
            };
            string json = await _repo.UpdateWeightUnit(weightunit);
            if (json == null || json == "null" || json == "[]" || json == "")
                return NotFound();
            else if (SCValidation.Validate(weightUnitUpdateForDto) != "Ok")
                return BadRequest("Only   .?,';:!-@    Special characters are allowed");
            return Ok(json);
        }

        // POST: api/Roles
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost("addweightunit")]
        public async Task<ActionResult<WeightUnit>> AddWeightUnit(WeightUnitAddForDto weightUnitAddForDto)
        {
            WeightUnit weightUnit = new WeightUnit
            {
                Name = weightUnitAddForDto.Name,
                IsActive = true
            };
            string json = await _repo.AddWeightUnit(weightUnit);
            if (json == null || json == "null" || json == "[]" || json == "")
            {
                return NotFound();
            }
            else if (SCValidation.Validate(weightUnitAddForDto) != "Ok")
                return BadRequest("Only   .?,';:!-@    Special characters are allowed");
            return Ok(json);
        }

        // DELETE: api/Roles/5
        [HttpDelete("deleteweightunit")]
        public async Task<ActionResult<WeightUnit>> DeleteWeightUnit(int id)
        {
            string json = await _repo.DeleteWeightUnit(id);
            if (json == null || json == "null" || json == "[]" || json == "")
                return NotFound();
            return Ok(json);
        }
    }
}
