using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
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
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository _repo;
        private readonly IConfiguration _config;
        private readonly IWebHostEnvironment _webHostEnvironment;
        SpecialCharacterValidation SCValidation = new SpecialCharacterValidation();
        public CategoriesController(ICategoryRepository repo, IConfiguration config, IWebHostEnvironment webHostEnvironment)
        {
            _repo = repo;
            _config = config;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: api/Categories
        [HttpGet("getcategories")]
        public async Task<ActionResult<IEnumerable<Category>>> GetAllCategory()
        {
            var category = await _repo.GetAllCategory();
            return Ok(category);
        }

        [HttpGet("getMoveCategories")]
        public async Task<ActionResult<IEnumerable<Category>>> GetMoveAllCategory()
        {
            var category = await _repo.GetMoveAllCategory();
            return Ok(category);
        }

        // GET: api/GetAllSubCategory
        [HttpGet("getallsubcategory")]
        public async Task<ActionResult<IEnumerable<Category>>> GetAllSubCategory()
        {
            var category = await _repo.GetAllSubCategory();
            return Ok(category);
        }

        [HttpGet("getMoveAllSubcategory")]
        public async Task<ActionResult<IEnumerable<Category>>> GetMoveAllSubCategory()
        {
            var category = await _repo.GetMoveAllSubCategory();
            return Ok(category);
        }

        // GET: api/Categories
        [HttpGet("getsubcategorybyparent/{parentid}")]
        public async Task<ActionResult<IEnumerable<Category>>> GetSubCategoryByParent(int parentid)
        {
            if (parentid == 0)
                return NotFound("[]");
            var category = await _repo.GetSubCategoryByParent(parentid);
            return Ok(category);
        }

        // GET: api/Categories
        [HttpGet("getMoveSubCategoryByParent/{parentid}")]
        public async Task<ActionResult<IEnumerable<Category>>> GetMoveSubCategoryByParent(int parentid)
        {
            if (parentid == 0)
                return NotFound("[]");
            var category = await _repo.GetMoveSubCategoryByParent(parentid);
            return Ok(category);
        }

        // GET: api/Categories
        //[HttpGet("getcategorywithdetail")]
        //public async Task<ActionResult<IEnumerable<Category>>> GetCategoryWithDetail()
        //{
        //    var category = await _repo.GetAllCategory();
        //    return Ok(category);
        //}
        // GET: api/Categories
        [HttpGet("getcategorywithdetail")]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategoryWithDetail()
        {
            var category = await _repo.GetCategoryWithDetail();
            return Ok(category);
        }
        //Vendor can have vehicles in different branches
        // GET: api/Categories
        [HttpGet("getcategorybyvendorid/{vendorid}")]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategoryByVendorId(int vendorid, int branchid)
        {
            var category = await _repo.GetCategoryByVendorId(vendorid, branchid);
            if (category == null || category == "null" || category == "[]" || category == "")
                return NotFound();
            return Ok(category);
        }
        // GET: api/Categories
        [HttpGet("getcategorybycategoryid/{vendorid}")]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategoryByCategoryId(int id, int vendorid, int branchid)
        {
            var category = await _repo.GetCategoryByCategoryId(id, vendorid, branchid);
            if (category == null || category == "null" || category == "[]" || category == "")
                return NotFound();
            return Ok(category);
        }
        // GET: api/Categories
        [HttpGet("getcategorybycategorydetailid/{id}")]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategoryByCategoryDetailId(int id)
        {
            var category = await _repo.GetCategoryByCategoryDetailId(id);
            if (category == null || category == "null" || category == "[]" || category == "")
                return NotFound();
            return Ok(category);
        }

        [HttpPost("addcategory")]
        public async Task<IActionResult> AddCategory([FromForm]CategoryForInsertDto categoryForInsertDto)
        {
            if (SCValidation.Validate(categoryForInsertDto) != "Ok")
                return BadRequest("Only   .?,';:!-@    Special characters are allowed");
            string Image = UploadFileAsync(categoryForInsertDto.Image);
            if (Image != "error")
            {
                string Icon = UploadFileAsync(categoryForInsertDto.Icon);
                if (Icon != "error")
                {


                    var categoryToInsert = new Category
                    {
                        Name = categoryForInsertDto.Name,
                        ParentId = categoryForInsertDto.ParentId,
                        IsActive = categoryForInsertDto.IsActive,
                        Type = categoryForInsertDto.Type,
                        CreateDate = DateTime.Now,
                        CreatedBy = categoryForInsertDto.CreatedBy

                    };

                    var categoryDetail = new CategoryDetail
                    {
                        BranchId = categoryForInsertDto.BranchId,
                        Image = Image,
                        Icon = Icon,
                        BaseFare = categoryForInsertDto.BaseFare,
                        PerKm = categoryForInsertDto.PerKm,
                        Description = categoryForInsertDto.Description,
                        Dimensions = categoryForInsertDto.Dimensions,
                        LoadCapacity = categoryForInsertDto.LoadCapacity,
                        PlateNo = categoryForInsertDto.PlateNo,
                        Property = categoryForInsertDto.Property,
                        AllowBidding = categoryForInsertDto.AllowBidding,
                        Status = "Available",
                        VendorId = categoryForInsertDto.VendorId,
                        CreateDate = DateTime.Now,
                        CreatedBy = categoryForInsertDto.CreatedBy,
                        IsActive = true
                    };
                    var categoryList = await _repo.AddCategory(categoryToInsert, categoryDetail);
                    if (categoryList == null || categoryList.Equals("[]") || categoryList.Equals(""))
                        return NotFound();
                    return Ok(categoryList);
                }
            }
            return NotFound();
        }
        // PUT: api/Categories/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("updatecategory")]
        public async Task<IActionResult> UpdateCategory([FromForm]CategoryForUpdateDto categoryForUpdateDto)
        {
            string categoryList = "";
            if (!await _repo.CategoryExist(categoryForUpdateDto.Id))
            {
                return NotFound();
            }
            else if (SCValidation.Validate(categoryForUpdateDto) != "Ok")
                return BadRequest("Only   .?,';:!-@    Special characters are allowed");
            else
            {
                string Image = UploadFileAsync(categoryForUpdateDto.Image);
                if (Image != "error")
                {
                    string Icon = UploadFileAsync(categoryForUpdateDto.Icon);
                    if (Icon != "error")
                    {
                        Category category = new Category
                        {
                            Name = categoryForUpdateDto.Name,
                            ParentId = categoryForUpdateDto.ParentId,
                            Type = categoryForUpdateDto.Type,
                            UpdateDate = DateTime.Now,
                            UpdatedBy = categoryForUpdateDto.UpdatedBy
                        };
                        CategoryDetail categoryDetail = new CategoryDetail
                        {
                            Id = categoryForUpdateDto.Id,
                            BranchId = categoryForUpdateDto.BranchId,
                            Description = categoryForUpdateDto.Description,
                            Dimensions = categoryForUpdateDto.Dimensions,
                            LoadCapacity = categoryForUpdateDto.LoadCapacity,
                            BaseFare = categoryForUpdateDto.BaseFare,
                            PerKm = categoryForUpdateDto.PerKm,
                            Icon = Icon,
                            Image = Image,
                            AllowBidding = categoryForUpdateDto.AllowBidding,
                            Property = categoryForUpdateDto.Property,
                            PlateNo = categoryForUpdateDto.PlateNo,
                            UpdateDate = DateTime.Now,
                            UpdatedBy = categoryForUpdateDto.UpdatedBy
                        };
                        categoryList = await _repo.UpdateCategory(category, categoryDetail);
                        if (categoryList == null || categoryList.Equals("[]") || categoryList.Equals(""))
                            return NotFound();
                    }
                }
                return Ok(categoryList);
            }
        }

        [HttpPut("changestatus")]
        public async Task<IActionResult> ChangeStatus(int id, string Status)
        {
            if (!await _repo.CategoryExist(id))
            {
                return NotFound();
            }
            string json = await _repo.ChangeStatus(id, Status);
            if (json == null || json.Equals("[]") || json.Equals(""))
                return NotFound();

            return Ok(json);
        }
        //[HttpPost("UploadFile")]  
        private string UploadFileAsync(IFormFile image)
        {
            string json = _repo.UploadFile(image, _webHostEnvironment.ContentRootPath);
            if (json == null || json == "null" || json == "[]" || json == "")
            {
                return "error";
            }
            return json;
        }
        //// DELETE: api/Categories/5
        //[HttpDelete("{id}")]
        //public async Task<ActionResult<Category>> DeleteCategory(int id)
        //{
        //    var category = await _context.Category.FindAsync(id);
        //    if (category == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Category.Remove(category);
        //    await _context.SaveChangesAsync();

        //    return category;
        //}


    }
}
