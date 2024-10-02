using Attendance.Service;
using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Attendance
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddScoped<IDepartmentService, DepartmentService>();
            builder.Services.AddScoped<IStudentService, StudentService>();
            builder.Services.AddScoped<IHRService, HRService>();

            // Register DinkToPdf's IConverter service
            builder.Services.AddSingleton<IConverter, SynchronizedConverter>(provider =>
                new SynchronizedConverter(new DinkToPdf.PdfTools()));

            // Configure authentication
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LogoutPath = "/account/login";
                });

            // Build the application
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication(); // Make sure this is added to enable authentication
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Account}/{action=login}/{id?}");

            app.Run();
        }
    }
}



//using Attendance.Service;
//using DinkToPdf.Contracts;
//using Microsoft.AspNetCore.Authentication.Cookies;
//using Microsoft.AspNetCore.Mvc.ViewEngines;

//namespace Attendance
//{
//    public class Program
//    {
//        public static void Main(string[] args)
//        {

//            var builder = WebApplication.CreateBuilder(args);

//            // Add services to the container.
//            builder.Services.AddControllersWithViews();
//            builder.Services.AddScoped<IDepartmentService, DepartmentService>();
//            builder.Services.AddScoped<IStudentService, StudentService>();
//            builder.Services.AddScoped<IHRService, HRService>();

//            // Register DinkToPdf's IConverter service


//            // Add services to the container.
//            var app = builder.Build();
//            builder.Services.AddSingleton<DinkToPdf.Contracts.IConverter, DinkToPdf.SynchronizedConverter>(provider =>
//                new DinkToPdf.SynchronizedConverter(new DinkToPdf.PdfTools()));

//            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(a =>
//            {
//                a.LogoutPath = "/account/login";



//            });





//            // Configure the HTTP request pipeline.
//            if (!app.Environment.IsDevelopment())
//            {
//                app.UseExceptionHandler("/Home/Error");
//            }

//            app.UseStaticFiles();

//            app.UseRouting();

//            app.UseAuthorization();


//            app.MapControllerRoute(
//                name: "default",

//                pattern: "{controller=Account}/{action=login}/{id?}");



//            app.Run();
//        }
//    }
//}
