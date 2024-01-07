using MicroserviceEnlistment;
using MicroserviceEnlistment.Helpers.Interfaces;
using MicroserviceEnlistment.Helpers.Services;
using MicroserviceEnlistment.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSqlServer<UserContext>(builder.Configuration.GetConnectionString("MicroserviceEnlistmentConnectionStrings"));
builder.Services.AddTransient<IUserService, UserService>();


builder.Services.AddIdentity<User, IdentityRole>(options =>
{
    options.User.RequireUniqueEmail = false;
})
    .AddEntityFrameworkStores<UserContext>()
    .AddDefaultTokenProviders();


/*builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });*/

//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//            .AddJwtBearer(x => x.TokenValidationParameters = new TokenValidationParameters
//            {
//                ValidateIssuer = false,
//                ValidateAudience = false,
//                ValidateLifetime = true,
//                ValidateIssuerSigningKey = true,
//                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["jwtKey"]!)),
//                ClockSkew = TimeSpan.Zero
//            });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
