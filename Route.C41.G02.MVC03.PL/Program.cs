using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Route.C41.G02.DAL.Data;
using Route.C41.G02.DAL.Helpers;
using Route.C41.G02.DAL.Models;
using Route.C41.G02.MVC03.PL.Helpers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace Route.C41.G02.MVC03.PL
{
    public class Program
    {
		// Enrty Point
        public static void Main(string[] args)
        {
            var Builder = WebApplication.CreateBuilder(args);

			#region Configure Services

			Builder.Services.AddControllersWithViews();// Register built-in services reqiured by MVC

			/// CLR will create an object from "ApplicationDbContext" each time user need this object at the same request
			///services.AddTransient<ApplicationDbContext>();

			/// CLR will create an object from "ApplicationDbContext" & store it in heap as long as user is still in the same request
			/// & After reequest end object will be unreachable
			///services.AddScoped<ApplicationDbContext>();
			///services.AddScoped<DbContextOptions<ApplicationDbContext>>();

			// This CoonnectionString is n't valid --> 1. Plain Text   2. Because CoonnectionString is changing from enviroment to another
			Builder.Services.AddDbContext<ApplicationDbContext>(options =>
			{
				options.UseSqlServer(Builder.Configuration.GetConnectionString("DefaultConnection"));
			});

			//services.AddScoped<IDepartmentRepository, DepartmentRepository>();
			//services.AddScoped<IEmployeeRepository, EmployeeRepository>();
			Builder.Services.AddApplicationServices();

			// CLR will craete an object from 'ApplicationDbContext' and store this object in heap as long as user open a session with server
			//services.AddSingleton<ApplicationDbContext>();

			Builder.Services.AddAutoMapper(M => M.AddProfile(new MappingProfiles()));

			//services.AddScoped<UserManager<ApplicationUser>>();
			//services.AddScoped<SignInManager<ApplicationUser>>();
			//services.AddScoped<RoleManager<IdentityRole>>();

			Builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
			{
				options.Password.RequiredUniqueChars = 2;
				options.Password.RequireDigit = true;
				options.Password.RequireNonAlphanumeric = true;
				options.Password.RequiredLength = 6;
				options.Password.RequireUppercase = true;
				options.Password.RequireLowercase = true;

				options.Lockout.AllowedForNewUsers = true;
				options.Lockout.MaxFailedAccessAttempts = 5;
				options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);

				options.User.RequireUniqueEmail = true;

			})
					.AddEntityFrameworkStores<ApplicationDbContext>()
					.AddDefaultTokenProviders();

			Builder.Services.ConfigureApplicationCookie(options =>
			{
				options.LoginPath = "/Account/SignIn";
				options.AccessDeniedPath = "/Home/Error";
			});

			//services.AddAuthentication();

			#endregion

			var app = Builder.Build();

			#region Configure Ketrel Middilewares

			if (app.Environment.IsDevelopment())
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

			app.UseAuthentication();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute(
					name: "default",
					pattern: "{controller=Home}/{action=Index}/{id?}");
			});

			#endregion

			app.Run();
		}
	}
}
