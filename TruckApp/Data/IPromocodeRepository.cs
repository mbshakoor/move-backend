using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TruckApp.Dtos;
using TruckApp.Models;

namespace TruckApp.Data
{
    public interface IPromocodeRepository
    {
        Task<string> GetPromoCode();
        Task<PromoCode> GetPromoCodeById(string id);
        Task<bool> PromocodeExist(string id);
        Task<string> AddPromoCode(PromoCode promocode);
        Task<string> UpdatePromoCode(PromoCode promocode);
        Task<string> ExpirePromoCode(string id);

    }
}
