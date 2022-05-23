using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Login.Models;

namespace TruckApp.Models
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<VehicleType> VehicleType { get; set; }
        public virtual DbSet<Vendor> Vendor { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<Booking> Booking { get; set; }
        public virtual DbSet<LoadType> LoadType { get; set; }
        public virtual DbSet<WeightUnit> WeightUnit { get; set; }
        public virtual DbSet<Feedback> Feedback { get; set; }
        public virtual DbSet<FeedbackType> FeedbackType { get; set; }
        public virtual DbSet<PromoCode> PromoCode { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<Bid> Bid { get; set; }
        public virtual DbSet<CategoryDetail> CategoryDetail { get; set; }
        public virtual DbSet<Login.Models.UserGroup> UserGroup { get; set; }
        public virtual DbSet<Login.Models.Branch> Branch { get; set; }


    }
}
