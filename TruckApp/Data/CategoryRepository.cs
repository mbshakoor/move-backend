using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using TruckApp.Consts;
using TruckApp.Models;

namespace TruckApp.Data
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DataContext _context;
        public CategoryRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<string> AddCategory(Category category, CategoryDetail categoryDetail)
        {
            int result;
            using (var transaction = _context.Database.BeginTransaction())
            {
                await _context.Category.AddAsync(category);
                result = await _context.SaveChangesAsync();

                categoryDetail.CategoryId = category.Id;
                await _context.CategoryDetail.AddAsync(categoryDetail);
                result += await _context.SaveChangesAsync();
                transaction.Commit();

                if (result > 1)
                {
                    //var categoryList = await _context.Category.ToListAsync();
                    //string json = JsonConvert.SerializeObject(categoryList);
                    string json = await GetCategoryByVendorId(categoryDetail.VendorId, categoryDetail.BranchId);
                    return json;

                }
                else return "[]";
            }
        }
        public async Task<string> UpdateCategory(Category category, CategoryDetail categoryDetail)
        {
            string json = "";
            var categoryDetailList = await _context.CategoryDetail.FirstOrDefaultAsync(x => x.Id == categoryDetail.Id);
            var categoryList = await _context.Category.FirstOrDefaultAsync(x => x.Id == categoryDetailList.CategoryId);
            //var categoryList = (from c in _context.Category
            //                         join cd in _context.CategoryDetail on c.Id equals cd.CategoryId
            //                         select ne00000000000000000000000000000w
            //                         {
            //                             c.Id,
            //                             c.Name,
            //                             c.ParentId,
            //                             c.Type,
            //                             c.UpdateDate,
            //                             c.UpdatedBy,
            //                             cd.AllowBidding,
            //                             cd.BaseFare,
            //                             cd.Description,
            //                             cd.Dimensions,
            //                             cd.Icon,
            //                             cd.Image,
            //                             cd.LoadCapacity,
            //                             cd.PerKm,
            //                             cd.PlateNo,
            //                             cd.Property,
            //                             UpdateDetailCD = cd.UpdateDate,
            //                             UpdatedByCD = cd.UpdatedBy
            //                         });
            using (var transaction = _context.Database.BeginTransaction())
            {

                categoryList.Name = category.Name;
                categoryList.Type = category.Type;
                categoryList.UpdateDate = category.UpdateDate;
                categoryList.UpdatedBy = category.UpdatedBy;
                categoryList.ParentId = category.ParentId;

                categoryDetailList.BranchId = categoryDetail.BranchId;
                categoryDetailList.Icon = categoryDetail.Icon;
                categoryDetailList.Image = categoryDetail.Image;
                categoryDetailList.LoadCapacity = categoryDetail.LoadCapacity;
                categoryDetailList.BaseFare = categoryDetail.BaseFare;
                categoryDetailList.PerKm = categoryDetail.PerKm;
                categoryDetailList.Dimensions = categoryDetail.Dimensions;
                categoryDetailList.Description = categoryDetail.Description;
                categoryDetailList.PlateNo = categoryDetail.PlateNo;
                categoryDetailList.AllowBidding = categoryDetail.AllowBidding;
                categoryDetailList.Property = categoryDetail.Property;
                categoryDetailList.UpdateDate = categoryDetail.UpdateDate;
                categoryDetailList.UpdatedBy = categoryDetail.UpdatedBy;

                //CategoryDetail categoryDetails = new CategoryDetail
                //{
                //    CategoryId = category.Id,
                //    IsActive = true,
                //    VendorId = categoryDetail.VendorId,
                //    BaseFare = categoryDetail.BaseFare,
                //    Description = categoryDetail.Description,
                //    Dimensions = categoryDetail.Dimensions,
                //    Icon = categoryDetail.Icon,
                //    Image = categoryDetail.Image,
                //    LoadCapacity = categoryDetail.LoadCapacity,
                //    PerKm = categoryDetail.PerKm,
                //    PlateNo = categoryDetail.PlateNo,
                //    Property = categoryDetail.Property,
                //    CreateDate = categoryDetail.CreateDate,
                //    CreatedBy = categoryDetail.CreatedBy
                //}

                _context.Entry(categoryList).State = EntityState.Modified;

                try
                {
                    //Update Category
                    int result = await _context.SaveChangesAsync();

                    //Update CategoryDetail
                    _context.Entry(categoryDetailList).State = EntityState.Modified;
                    result += await _context.SaveChangesAsync();

                    if (result > 0)
                    {
                        //var categoryData = await _context.Category.ToListAsync();
                        var categoryData = await GetCategoryByVendorId(categoryDetailList.VendorId, categoryDetailList.BranchId);
                        json = JsonConvert.SerializeObject(categoryData);
                        transaction.Commit();
                    }
                    else
                        json = "[]";
                }
                catch (DbUpdateConcurrencyException)
                {
                }
            }
            return json;

        }
        public async Task<bool> CategoryExist(int Id)
        {
            return await _context.Category.AnyAsync(x => x.Id == Id);
        }
        public async Task<string> GetAllCategory()
        {
            var category = await _context.Category.ToListAsync<Category>();
            string json = JsonConvert.SerializeObject(category);
            return json;
        }

        public async Task<string> GetMoveAllCategory()
        {
            string json = "";
            try
            {
                var category = from c in _context.Category
                               join cDetail in _context.CategoryDetail on c.Id equals cDetail.CategoryId
                               where cDetail.IsActive == true && c.ParentId == 0 
                               select new CategoryModel
                               {
                                   CategoryId = c.Id,
                                   Name = c.Name,
                                   Icon = cDetail.Icon,
                                   Image = cDetail.Image
                               };
                var categoryList = category.ToList();
                foreach (var ctgry in categoryList)
                {
                    string imageFolder = Path.Combine(MoveConsts.IMAGES_DIRECTORY, MoveConsts.IMAGES_FOLDER);
                    string imageFilePath = Path.Combine(imageFolder, ctgry.Image);
                    string iconFilePath = Path.Combine(imageFolder, ctgry.Image);
                    Bitmap bitmap = new Bitmap(imageFilePath);
                    Bitmap bitmap1 = new Bitmap(iconFilePath);

                    using (var stream = new MemoryStream())
                    {
                        bitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                        ctgry.ImageByte = stream.ToArray();
                    }

                    using (var stream1 = new MemoryStream())
                    {
                        bitmap.Save(stream1, System.Drawing.Imaging.ImageFormat.Png);
                        ctgry.IconByte = stream1.ToArray();
                    }
                }
                json = JsonConvert.SerializeObject(categoryList);
            }
            catch (Exception exc)
            { 
            
            }

            return json;
        }


        public async Task<string> GetCategoryWithDetail()
        {
            var category = (from c in _context.Category
                            join cDetail in _context.CategoryDetail on c.Id equals cDetail.CategoryId
                            select new
                            {
                                CategoryId = c.Id,
                                c.Name,
                                c.ParentId,
                                c.Type,
                                c.CreateDate,
                                c.CreatedBy,
                                c.UpdateDate,
                                c.UpdatedBy,
                                cDetail.BaseFare,
                                cDetail.Description,
                                cDetail.Dimensions,
                                cDetail.Icon,
                                cDetail.Image,
                                cDetail.IsActive,
                                cDetail.LoadCapacity,
                                cDetail.PerKm,
                                cDetail.PlateNo,
                                cDetail.Property,
                                cDetail.Status,
                                cDetail.VendorId,
                                cDetail.Id
                            }
                    );
            string json = JsonConvert.SerializeObject(category);
            return json;
        }
        public async Task<string> GetCategoryByVendorId(int vendorId, int branchid)
        {
            var categoryData = await (from category in _context.Category
                                      join cDetail in _context.CategoryDetail on category.Id equals cDetail.CategoryId
                                      select new
                                      {
                                          CategoryId = category.Id,
                                          category.Name,
                                          category.ParentId,
                                          category.Type,
                                          category.CreateDate,
                                          category.CreatedBy,
                                          category.UpdateDate,
                                          category.UpdatedBy,
                                          cDetail.BaseFare,
                                          cDetail.Description,
                                          cDetail.Dimensions,
                                          cDetail.Icon,
                                          cDetail.Image,
                                          cDetail.IsActive,
                                          cDetail.LoadCapacity,
                                          cDetail.PerKm,
                                          cDetail.PlateNo,
                                          cDetail.Property,
                                          cDetail.Status,
                                          cDetail.VendorId,
                                          cDetail.Id,
                                          cDetail.BranchId
                                      }
                    ).Where(x => x.VendorId == vendorId).Where(x => x.BranchId == (branchid == 0 ? x.BranchId : branchid)).ToListAsync();
            string json = JsonConvert.SerializeObject(categoryData);
            return json;
        }
        public async Task<string> GetCategoryByCategoryId(int Id, int VendorId, int branchid)
        {
            var category = (from c in _context.Category
                            join cDetail in _context.CategoryDetail on c.Id equals cDetail.CategoryId
                            select new
                            {
                                CategoryId = c.Id,
                                c.Name,
                                c.ParentId,
                                c.Type,
                                c.CreateDate,
                                c.CreatedBy,
                                c.UpdateDate,
                                c.UpdatedBy,
                                cDetail.BaseFare,
                                cDetail.Description,
                                cDetail.Dimensions,
                                cDetail.Icon,
                                cDetail.Image,
                                cDetail.IsActive,
                                cDetail.LoadCapacity,
                                cDetail.PerKm,
                                cDetail.PlateNo,
                                cDetail.Property,
                                cDetail.Status,
                                cDetail.VendorId,
                                cDetail.Id,
                                cDetail.BranchId
                            }
                    ).Where(x => x.CategoryId == Id).
                    Where(x => x.VendorId == (VendorId == 0 ? x.VendorId : VendorId))
                    .Where(x => x.BranchId == (branchid == 0 ? x.BranchId : branchid));
            string json = JsonConvert.SerializeObject(category);
            return json;
        }
        public async Task<string> GetCategoryByCategoryDetailId(int Id)
        {
            var category = (from c in _context.Category
                            join cDetail in _context.CategoryDetail on c.Id equals cDetail.CategoryId
                            select new
                            {
                                CategoryId = c.Id,
                                c.Name,
                                c.ParentId,
                                c.Type,
                                c.CreateDate,
                                c.CreatedBy,
                                c.UpdateDate,
                                c.UpdatedBy,
                                cDetail.BaseFare,
                                cDetail.Description,
                                cDetail.Dimensions,
                                cDetail.Icon,
                                cDetail.Image,
                                cDetail.IsActive,
                                cDetail.LoadCapacity,
                                cDetail.PerKm,
                                cDetail.PlateNo,
                                cDetail.Property,
                                cDetail.Status,
                                cDetail.VendorId,
                                cDetail.Id
                            }
                    ).Where(x => x.Id == Id);
            string json = JsonConvert.SerializeObject(category);
            return json;
        }

        public async Task<string> ChangeStatus(int id, string Status)
        {
            string json = "";
            var category = await _context.CategoryDetail.FirstOrDefaultAsync(x => x.Id == id);
            category.Status = Status;
            _context.Entry(category).State = EntityState.Modified;
            try
            {
                int result = await _context.SaveChangesAsync();
                if (result > 0)
                    json = JsonConvert.SerializeObject(category);
                else
                    json = "[]";

            }
            catch (DbUpdateConcurrencyException)
            {

            }
            return json;
        }
        public string UploadFile(IFormFile file, string webRootPath)
        {
            string uniqueFileName = null;

            if (file != null && file.Length > 0)
            {
                //string folderName = "Upload";
                //string uploadsFolder = Path.Combine(webRootPath, folderName);
                //string folderName = MoveConsts.IMAGES_FOLDER;
                string uploadsFolder = Path.Combine(MoveConsts.IMAGES_DIRECTORY, MoveConsts.IMAGES_FOLDER);
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                //string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }
        public async Task<string> GetAllSubCategory()
        {
            var subCategory = await _context.Category.Where(x => x.ParentId > 0).ToListAsync<Category>();
            string json = JsonConvert.SerializeObject(subCategory);
            return json;
        }

        public async Task<string> GetMoveAllSubCategory()
        {
            string json = "";
            try
            {
                var category = from c in _context.Category
                               join cDetail in _context.CategoryDetail on c.Id equals cDetail.CategoryId
                               where cDetail.IsActive == true && c.ParentId > 0
                               select new CategoryModel
                               {
                                   CategoryId = c.Id,
                                   Name = c.Name,
                                   Icon = cDetail.Icon,
                                   Image = cDetail.Image,
                                   ParentId = c.ParentId
                               };
                var categoryList = category.ToList();
                foreach (var ctgry in categoryList)
                {
                    string imageFolder = Path.Combine(MoveConsts.IMAGES_DIRECTORY, MoveConsts.IMAGES_FOLDER);
                    string imageFilePath = Path.Combine(imageFolder, ctgry.Image);
                    string iconFilePath = Path.Combine(imageFolder, ctgry.Image);
                    Bitmap bitmap = new Bitmap(imageFilePath);
                    Bitmap bitmap1 = new Bitmap(iconFilePath);

                    using (var stream = new MemoryStream())
                    {
                        bitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                        ctgry.ImageByte = stream.ToArray();
                    }

                    using (var stream1 = new MemoryStream())
                    {
                        bitmap.Save(stream1, System.Drawing.Imaging.ImageFormat.Png);
                        ctgry.IconByte = stream1.ToArray();
                    }
                }
                json = JsonConvert.SerializeObject(categoryList);
            }
            catch (Exception exc)
            {

            }

            return json;
        }

        public async Task<string> GetSubCategoryByParent(int ParentId)
        {
            
            var subCategory = await _context.Category.Where(x=>x.ParentId==ParentId).ToListAsync<Category>();
            string json = JsonConvert.SerializeObject(subCategory);
            return json;
        }

        public async Task<string> GetMoveSubCategoryByParent(int ParentId)
        {
            string json = "";
            try
            {
                var category = from c in _context.Category
                               join cDetail in _context.CategoryDetail on c.Id equals cDetail.CategoryId
                               where c.ParentId == ParentId
                               select new CategoryModel
                               {
                                   CategoryId = c.Id,
                                   Name = c.Name,
                                   Icon = cDetail.Icon,
                                   Image = cDetail.Image,
                                   ParentId = c.ParentId
                               };
                var categoryList = category.ToList();
                foreach (var ctgry in categoryList)
                {
                    string imageFolder = Path.Combine(MoveConsts.IMAGES_DIRECTORY, MoveConsts.IMAGES_FOLDER);
                    string imageFilePath = Path.Combine(imageFolder, ctgry.Image);
                    string iconFilePath = Path.Combine(imageFolder, ctgry.Image);
                    Bitmap bitmap = new Bitmap(imageFilePath);
                    Bitmap bitmap1 = new Bitmap(iconFilePath);

                    using (var stream = new MemoryStream())
                    {
                        bitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                        ctgry.ImageByte = stream.ToArray();
                    }

                    using (var stream1 = new MemoryStream())
                    {
                        bitmap.Save(stream1, System.Drawing.Imaging.ImageFormat.Png);
                        ctgry.IconByte = stream1.ToArray();
                    }
                }
                json = JsonConvert.SerializeObject(categoryList);
            }
            catch (Exception exc)
            {

            }

            return json;
        }
    }
}
