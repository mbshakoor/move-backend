using System.Threading.Tasks;
using TruckApp.Models;

namespace TruckApp.Data
{
    public interface IVendorRepository
    {
        Task<string> GetVendor();
        Task<string> GetVendorById(int Id);
        Task<string> UpdateVendor(Vendor vendor);
        Task<string> AddVendor(Vendor vendor);
        Task<string> DeleteVendor(int id);
        Task<bool> VendorExist(int id);

    }
}
