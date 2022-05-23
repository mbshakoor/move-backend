using System;
using System.Collections.Generic;
using System.Linq;
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
    public class PromoCodesController : ControllerBase
    {
        private readonly IPromocodeRepository _repo;
        private readonly IConfiguration _config;
        SpecialCharacterValidation SCValidation = new SpecialCharacterValidation();
        public PromoCodesController(IPromocodeRepository repo, IConfiguration config)
        {
            _repo = repo;
            _config = config;
        }

        // GET: api/PromoCodes
        [HttpGet("getpromocodes")]
        public async Task<ActionResult<IEnumerable<PromoCode>>> GetPromoCode()
        {
            string json = await _repo.GetPromoCode();
            if (json == null || json == "[]")
            {
                return NotFound();
            }
            return Ok(json);
        }

        // GET: api/PromoCodes/5
        [HttpGet("getpromocodebyid/{id}")]
        public async Task<ActionResult<PromoCode>> GetPromoCodeById(string id)
        {
            var promoCode = await _repo.GetPromoCodeById(id);
            if (promoCode == null)
            {
                return NotFound();
            }

            string json = JsonConvert.SerializeObject(promoCode);
            
            return Ok(json);
        }

        // PUT: api/PromoCodes/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("updatepromocode")]
        public async Task<IActionResult> UpdatePromocode(PromocodeForUpdateDto promoCode)
        {
            if (!await _repo.PromocodeExist(promoCode.Id))
            {
                return NotFound();
            }
            var promoCodeList = new PromoCode
            {
                Id = promoCode.Id,
                Title = promoCode.Title,
                DiscontPercent = promoCode.DiscontPercent,
                EffectiveDate = promoCode.EffectiveDate,
                EndDate = promoCode.EndDate
            };
            string json = await _repo.UpdatePromoCode(promoCodeList);
            if (json == null || json == "[]")
            {
                return NotFound();
            }
            else if (SCValidation.Validate(promoCode) != "Ok")
                return BadRequest("Only   .?,';:!-@    Special characters are allowed");
            return Ok(json);
        }

        // POST: api/PromoCodes
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost("addpromocode")]
        public async Task<ActionResult<PromoCode>> AddPromocode(PromocodeForAddDto promoCode)
        {
            var promoCodeList = new PromoCode
            {
                Code = promoCode.Code,
                Title = promoCode.Title,
                DiscontPercent = promoCode.DiscontPercent,
                EffectiveDate = promoCode.EffectiveDate,
                EndDate = promoCode.EndDate,
                IsActive = promoCode.IsActive
            };
            string json = await _repo.AddPromoCode(promoCodeList);
            if (json == null || json == "[]")
                return NotFound();
            else if (SCValidation.Validate(promoCode) != "Ok")
                return BadRequest("Only   .?,';:!-@    Special characters are allowed");
            return Ok(json);
        }

        // DELETE: api/PromoCodes/5
        [HttpDelete("expirepromocode/{id}")]
        public async Task<ActionResult<PromoCode>> ExpirePromoCode(string id)
        {
            bool PromoCodeExist = await _repo.PromocodeExist(id);
            if (!PromoCodeExist)
            {
                return NotFound();
            }
            string json = await _repo.ExpirePromoCode(id);
            if (json == null || json == "[]")
                return NotFound();
            return Ok(json);
        }
    }
}
