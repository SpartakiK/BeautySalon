using Domain.Commands;
using Domain.Commands.Abstract;
using Data.Repositories;
using Data.Repositories.Abstract;
using EntityFrameworkCore.DbConnection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.Abstract;
using Services.Services;
using Services.Services.Abstract;
using Domain.Mappers.Abstract;
using Domain.Mappers;
using Domain.Abstract.Mappers;

namespace Web
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
            services.AddControllersWithViews();
            services.AddDbContext<DbConnect>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("Default"));
            });
            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(99999999);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IOrderRepository, OrderRepository>();
            services.AddTransient<IServiceRepository, ServiceRepository>();
            services.AddTransient<IEmployeeRepository, EmployeeRepository>();
            services.AddTransient<IOrderDetailRepository, OrderDetailRepository>();
            services.AddTransient<IUserCommands, UserCommands>();
            services.AddTransient<IUserServices, UserServices>();
            services.AddTransient<IServiceServices, ServiceServices>();
            services.AddTransient<IEmployeeServices, EmployeeServices>();
            services.AddTransient<IOrderServices, OrderServices>();
            services.AddTransient<IUserMapper, UserMapper>();
            services.AddTransient<IServiceMapper, ServiceMapper>();
            services.AddTransient<IOrderMapper, OrderMapper>();
            services.AddTransient<IEmployeeMapper, EmployeeMapper>();
            services.AddTransient<IOrderDetailMapper, OrderDetailMapper>();
            services.AddTransient<IOrderStatusHistoryMapper, OrderStatusHistoryMapper>();
            services.AddTransient<IOrderDetailRepository, OrderDetailRepository>();
            services.AddTransient<IOrderStatusHistoryRepository, OrderStatusHistoryRepository>();
            services.AddTransient<IOrderDetailServices, OrderDetailServices>();
            services.AddTransient<IOrderStatusHistoryServices, OrderStatusHistoryServices>();
            services.AddTransient<IEmployeesAndServicesServices, EmployeesAndServicesServices>();
            services.AddTransient<IEmployeesAndServicesMapper, EmployeesAndServicesMapper>();
            services.AddTransient<IEmployeesAndServicesRepository, EmployeesAndServicesRepository>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            
            app.UseRouting();

            app.UseAuthorization();

            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("Default", "{controller=Home}/{action=Index}/{id?}");
            });

        }
    }
}
