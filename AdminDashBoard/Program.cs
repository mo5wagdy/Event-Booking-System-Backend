using AdminDashBoard.Helper;
using Booking.Core.Models.Identity;
using Booking.Core.Repo.contract;
using Booking.Repo;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AdminDashBoard
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();


            builder.Services.AddDbContext<EventBookingDbContext>(options =>
            {
                options/*.UseLazyLoadingProxies()*/.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            // IdentityDbContext
            builder.Services.AddDbContext<BookingIdentityDbContext>(optionsBuilder =>
            {
                optionsBuilder.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"));
            });

          
            builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;
                options.Password.RequiredLength = 6;
            })
          .AddEntityFrameworkStores<BookingIdentityDbContext>()
            .AddDefaultTokenProviders();



            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            builder.Services.AddAutoMapper(typeof(MapsProfile));
            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Admin/Login";  // رابط صفحة تسجيل الدخول في الـ MVC
                options.AccessDeniedPath = "/Admin/AccessDenied"; // لو في صفحة مخصصة للرفض
            });


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
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

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Admin}/{action=Login}/{id?}");

            app.Run();
        }
    }
}
