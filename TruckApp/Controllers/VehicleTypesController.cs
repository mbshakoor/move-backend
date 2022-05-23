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
    public class VehicleTypesController : ControllerBase
    {
        private readonly IVehicleTypeRepository _repo;
        private readonly IConfiguration _config;
        SpecialCharacterValidation SCValidation = new SpecialCharacterValidation();
        public VehicleTypesController(IVehicleTypeRepository repo, IConfiguration config)
        {
            _repo = repo;
            _config = config;
        }

        // GET: api/Roles
        [HttpGet("getvehicletype")]
        public async Task<ActionResult<IEnumerable<VehicleType>>> GetVehicleType()
        {
            string json = await _repo.GetVehicleType();
            if (json == null || json == "null" || json == "[]" || json == "")
                return NotFound();
            return Ok(json);
        }

        // GET: api/Roles/5
        [HttpGet("getvehicletypebyid/{id}")]
        public async Task<ActionResult<VehicleType>> GetVehicleType(int id)
        {
            string json = await _repo.GetVehicleTypeById(id);
            if (json == null || json == "null" || json == "[]" || json == "")
                return NotFound();
            return Ok(json);
        }

        // PUT: api/Roles/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("updatevehicletype")]
        public async Task<IActionResult> UpdateVehicleType(VehicleTypeUpdateForDto vehicleTypeUpdateForDto)
        {
            VehicleType vehicleType = new VehicleType
            {
                Id = vehicleTypeUpdateForDto.Id,
                Name = vehicleTypeUpdateForDto.Name
            };
            string json = await _repo.UpdateVehicleType(vehicleType);
            if (json == null || json == "null" || json == "[]" || json == "")
                return NotFound();
            else if (SCValidation.Validate(vehicleTypeUpdateForDto) != "Ok")
                return BadRequest("Only   .?,';:!-@    Special characters are allowed");
            return Ok(json);
        }

        // POST: api/Roles
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost("addvehicletype")]
        public async Task<ActionResult<VehicleType>> AddVehicleType(VehicleTypeAddForDto vehicleTypeAddForDto)
        {
            VehicleType vehicleType = new VehicleType
            {
                Name = vehicleTypeAddForDto.Name,
                IsActive = true
            };
            string json = await _repo.AddVehicleType(vehicleType);
            if (json == null || json == "null" || json == "[]" || json == "")
            {
                return NotFound();
            }
            else if (SCValidation.Validate(vehicleTypeAddForDto) != "Ok")
                return BadRequest("Only   .?,';:!-@    Special characters are allowed");
            return Ok(json);
        }

        // DELETE: api/Roles/5
        [HttpDelete("deletevehicletype")]
        public async Task<ActionResult<VehicleType>> DeleteVehicleType(int id)
        {
            string json = await _repo.DeleteVehicleType(id);
            if (json == null || json == "null" || json == "[]" || json == "")
                return NotFound();
            return Ok(json);
        }
    }
}
