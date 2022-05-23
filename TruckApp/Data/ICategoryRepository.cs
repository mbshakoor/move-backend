using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TruckApp.Models;

namespace TruckApp.Data
{
    public interface ICategoryRepository
    {
        Task<string> GetAllCategory();
        Task<string> GetMoveAllCategory();
        Task<bool> CategoryExist(int Id);
        Task<string> GetCategoryByVendorId(int vendorId,int branchid);
        Task<string> GetCategoryByCategoryId(int Id, int VendorId,int branchid);
        Task<string> GetCategoryByCategoryDetailId(int Id); 
        Task<string> AddCategory(Category category,CategoryDetail categoryDetail);
        Task<string> UpdateCategory(Category category, CategoryDetail categoryDetail);
        Task<string> ChangeStatus(int id,string Status);
        string UploadFile(IFormFile file,string webRootPath);
        Task<string> GetAllSubCategory();
        Task<string> GetMoveAllSubCategory();
        Task<string> GetSubCategoryByParent(int ParentId);
        Task<string> GetMoveSubCategoryByParent(int ParentId);
        Task<string> GetCategoryWithDetail();

    }
}
