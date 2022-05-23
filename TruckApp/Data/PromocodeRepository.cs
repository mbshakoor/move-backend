using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TruckApp.Dtos;
using TruckApp.Models;

namespace TruckApp.Data
{
    public class PromocodeRepository : IPromocodeRepository
    {
        private readonly DataContext _context;
        public PromocodeRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<string> GetPromoCode()
        {
            var Promocode = await _context.PromoCode.ToListAsync();
            string json = JsonConvert.SerializeObject(Promocode);
            return json;
        }

        public async Task<PromoCode> GetPromoCodeById(string id)
        {
            var Promocode = await _context.PromoCode.FirstOrDefaultAsync(x => x.Id.Equals(id));
            return Promocode;
            //string json = JsonConvert.SerializeObject(Promocode);
            //return json;
        }

        public async Task<bool> PromocodeExist(string id)
        {
            bool Promocode = await _context.PromoCode.AnyAsync(x => x.Id.Equals(id));
            return Promocode;
        }
        public async Task<string> AddPromoCode(PromoCode promoCode)
        {
            var lastPromoCodeRecord = await _context.PromoCode.OrderByDescending(x => x.Id).FirstOrDefaultAsync();
            string promoCodeId = lastPromoCodeRecord == null ? "PCD-1" : "PCD-" + (int.Parse(lastPromoCodeRecord.Id.Split('-')[1].ToString())+1).ToString();
            promoCode.Id = promoCodeId;
            await _context.PromoCode.AddAsync(promoCode);
            int result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                var Promocode = await _context.PromoCode.ToListAsync();
                string json = JsonConvert.SerializeObject(Promocode);
                return json;
            }
            else
                return "error";
        }
        public async Task<string> UpdatePromoCode(PromoCode promoCode)
        {
            string json = "";
            var PromocodeList = await _context.PromoCode.FirstOrDefaultAsync(x => x.Id.Equals(promoCode.Id));
            PromocodeList.Code = promoCode.Code;
            PromocodeList.Title = promoCode.Title;
            PromocodeList.DiscontPercent = promoCode.DiscontPercent;
            PromocodeList.EffectiveDate = promoCode.EffectiveDate;
            PromocodeList.EndDate = promoCode.EndDate;
            _context.Entry(PromocodeList).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
                var PromocodeData = await _context.PromoCode.ToListAsync();
                json = JsonConvert.SerializeObject(PromocodeData);
            }
            catch (DbUpdateConcurrencyException)
            {
            }
            return json;
        }

        public async Task<string> ExpirePromoCode(string id)
        {
            string json = "";
            var PromocodeList = await _context.PromoCode.FirstOrDefaultAsync(x => x.Id.Equals(id));
            PromocodeList.IsActive = false;
            _context.Entry(PromocodeList).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
                var PromocodeData = await _context.PromoCode.ToListAsync();
                json = JsonConvert.SerializeObject(PromocodeData);
            }
            catch (DbUpdateConcurrencyException)
            {
            }
            return json;
        }
    }
}
