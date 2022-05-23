using Microsoft.AspNetCore.Mvc.RazorPages.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TruckApp.Dtos
{
    public class UserForRegisterDto
    {
        //[Required]
        //public string Username { get; set; }
        //[Required]
        //public string Password { get; set; }
        //public string Name { get; set; }
        //public string PhoneNo { get; set; }
        //public string Address { get; set; }
        //public bool IsActive { get; set; }
        //public DateTime CreateDate { get; set; }
        //public DateTime UpdateDate { get; set; }
        //public int Role { get; set; }
        //public DateTime LastLoginDate { get; set; }
        //public string Image { get; set; }
        //public string AdminApproveStatus { get; set; }
        //public string CompanyName { get; set; }
        //public string EmailAddress { get; set; }
        //public string City { get; set; }
        //public int BranchId { get; set; }
        //public int UserGroupId { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        public string Name { get; set; }
        public long PhoneNo { get; set; }
        //public string Address { get; set; }
        //public bool IsActive { get; set; }
        //public DateTime CreateDate { get; set; }
        //public DateTime UpdateDate { get; set; }
        //public int Role { get; set; }
        ////public byte[] PasswordHash { get; set; }
        ////public byte[] PasswordSalt { get; set; }
        //public DateTime LastLoginDate { get; set; }
        //public string Image { get; set; }
        //public string AdminApproveStatus { get; set; }
        //public string CompanyName { get; set; }
        public string EmailAddress { get; set; }
        //public string City { get; set; }
        //public int BranchId { get; set; }
        //public int UserGroupId { get; set; }
    }
}
