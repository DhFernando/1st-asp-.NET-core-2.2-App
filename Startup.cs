using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace EmployeeManagementSystem
{
    public class Startup
    {
        private IConfiguration _config;
        public Startup(IConfiguration config)
        {
            _config = config;
        }
        public void ConfigureServices(IServiceCollection services)
        {
             
            services.AddDbContextPool<AppDbContext>(options => options.UseSqlServer(_config.GetConnectionString("EmployeeDBConnection")));

            services.AddMvc(config=> {
                var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
                config.Filters.Add(new AuthorizeFilter(policy));
            }); 
            services.AddScoped<IEmployeeRepository, SQLEmployeeRepository>();
            services.AddSingleton<IDepartmentRepository, MockDepartmentRepository>();
            services.AddIdentity<ApplicationUser, IdentityRole>(option=> {
                option.Password.RequiredLength = 4;
                option.Password.RequiredUniqueChars = 1;
            }).AddEntityFrameworkStores<AppDbContext>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILogger<Startup> logger)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            // app.UseDefaultFiles(); 
            app.UseStaticFiles();
            // app.UseMvcWithDefaultRoute();
            app.UseAuthentication();
            app.UseMvc(routes=> {
                routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });

            app.UseMvc();

            app.Use(async (context , next) =>
            {
                await context.Response.WriteAsync("Something Went Worng");
                await next();
            });

            

        }
    }
}
