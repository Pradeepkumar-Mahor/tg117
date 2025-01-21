using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;
using System.Text;
using tg117.Domain;
using tg117.Domain.DbContext;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

byte[] secretBytes = new byte[64];
using (RandomNumberGenerator random = RandomNumberGenerator.Create())
{
    random.GetBytes(secretBytes);
}

string secretKey = Convert.ToBase64String(secretBytes);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(option =>
option.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection")));

//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//    .AddJwtBearer(options =>
//    {
//        options.TokenValidationParameters = new TokenValidationParameters
//        {
//            ValidateIssuer = true,
//            ValidateAudience = false,
//            ValidateLifetime = true,
//            ValidateIssuerSigningKey = true,
//            ValidIssuer = "FreeTrained",
//            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
//        };
//    });
//builder.Services.AddDataAccessService();

//builder.Services.AddAuthorization();

//builder.Services.AddIdentityApiEndpoints<AppUser>(options =>
//{
//    options.Password.RequiredLength = 8;
//    options.Password.RequireNonAlphanumeric = true;
//    options.Password.RequireDigit = true;
//    options.Password.RequireUppercase = true;
//    options.Password.RequireLowercase = true;
//})
//    .AddEntityFrameworkStores<AppDbContext>()
//    .AddDefaultTokenProviders();

builder.Services.AddDataAccessService();

builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
{
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireDigit = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
})
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
        //IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
    };
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("BasicPolicy", policy => policy.RequireRole(roles.RoleBasic));
    options.AddPolicy("AdminPolicy", policy => policy.RequireRole(roles.RoleAdmin));
    options.AddPolicy("SuperAdminPolicy", policy => policy.RequireRole(roles.RoleSuperAdmin));
});

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    _ = app.UseSwagger();
    _ = app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();