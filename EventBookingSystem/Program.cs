using Booking.Core.Models.Identity;
using Booking.Core.Repo.contract;
using Booking.Core.Services.contract;
using Booking.Repo;
using Booking.Repo.Data;
using Booking.Repo.Data.IdentitySeedsing;
using Bookings.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// App DB Context
builder.Services.AddDbContext<EventBookingDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Identity DB Context
builder.Services.AddDbContext<BookingIdentityDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("EventBookingidentity")));

//// Register Identity services
builder.Services.AddIdentity<AppUser, AppRole>(options =>
{

    options.Password.RequireDigit = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireLowercase = true;
    options.Password.RequiredLength = 6;
    
})
.AddEntityFrameworkStores<BookingIdentityDbContext>()
.AddDefaultTokenProviders();

// Add Repositories & Services
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IBookingService, BookingServices>();
builder.Services.AddScoped<IBookingRepository, BookingRepository>();
builder.Services.AddScoped<ITokenProvider, TokenProvider>();

// JWT Config
var jwtSettings = builder.Configuration.GetSection("JWT");
var jwtKey = Encoding.UTF8.GetBytes(jwtSettings["Key"]);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = jwtSettings["Issuer"],

        ValidateAudience = true,
        ValidAudience = jwtSettings["Audience"],

        ValidateLifetime = true,

        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(jwtKey),

        ClockSkew = TimeSpan.Zero
    };
});

builder.Services.AddAuthorization();

builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddCors(options =>
{
    options.AddPolicy("Policy", policy =>
    {
        policy.WithOrigins("http://localhost:3000")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

// Apply migrations and seeding
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var loggerFactory = services.GetRequiredService<ILoggerFactory>();

    try
    {
        var dbContext = services.GetRequiredService<EventBookingDbContext>();
        dbContext.Database.Migrate();

        var identityContext = services.GetRequiredService<BookingIdentityDbContext>();
        identityContext.Database.Migrate();

        var userManager = services.GetRequiredService<UserManager<AppUser>>();
      
    await AppIdentityDbContextSeed.SeedIdentityAsync(userManager);

        await EventsContextSeed.SeedAsync(dbContext);
    }
    catch (Exception ex)
    {
        var logger = loggerFactory.CreateLogger<Program>();
        logger.LogError(ex, "An exception occurred during migration or seeding.");
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseCors("Policy");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
