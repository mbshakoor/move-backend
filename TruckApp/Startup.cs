using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using Pomelo.EntityFrameworkCore.MySql.Storage;
using TruckApp.Data;
using TruckApp.Models;

namespace TruckApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IBookingRepository, BookingRespository>();
            services.AddScoped<IPromocodeRepository, PromocodeRepository>();
            services.AddScoped<IBidRepository, BidRepository>();
            services.AddScoped<IFeedbackRepository, FeedbackRepository>();
            services.AddScoped<IFeedbackTypeRepository, FeedbackTypeRepository>();
            services.AddScoped<ILoadTypeRepository, LoadTypeRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IVehicleTypeRepository, VehicleTypeRepository>();
            services.AddScoped<IWeightUnitRepository, WeightUnitRepository>();
            services.AddScoped<IUserGroupRepository, UserGroupRepository>();
            services.AddScoped<IBranchRepository, BranchRepository>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(name: "v1", new OpenApiInfo { Title = "My API", Version = "v1" });
            });
            //if (Configuration["Provider"] == "MSSql")
            //{


            //string sqlConnectionString = Configuration.GetConnectionString("DevConnection");
            services.AddDbContext<DataContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DevConnection")));



            //}
            //services.AddDbContext<DataContext>(options => options
            //.UseMySql(Configuration.GetConnectionString("MariaDBConnection"), mysqlOptions => mysqlOptions.ServerVersion(new ServerVersion(new Version(14, 4, 12), ServerType.MariaDb))));
            //        else if (Configuration["Provider"] == "MySql")
            //        {



            //            string sqlConnectionString = Configuration.GetConnectionString("MySql");
            //            services.AddDbContext<DataContext>(options =>
            //            options.UseMySql(
            //            sqlConnectionString
            //            //b => b.MigrationsAssembly("AspNetCoreMultipleProject")
            //        )
            //);
            //        }



            services.AddCors();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint(url: "/swagger/v1/swagger.json", name: "My API V1");
            });
            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            //app.UseCors(
            //    //options => options.WithOrigins("http://localhost:3032", "", "").AllowAnyMethod()
            //);


            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
