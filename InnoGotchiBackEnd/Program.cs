using BusinessAccessLayer.Mappings;
using BusinessAccessLayer.Services;
using BusinessAccessLayer.Validators;
using DataAccessLayer;
using DataAccessLayer.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Web_Api;
using Swashbuckle.AspNetCore.Filters;
using System.Text;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    options.OperationFilter<SecurityRequirementsOperationFilter>();
});

//services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IFarmService, FarmService>();
builder.Services.AddScoped<IUserFarmService, UserFarmService>();
builder.Services.AddScoped<IInnogotchiService, InnogotchiService>();
builder.Services.AddScoped<IInnogotchiBodyPartService, InnogotchiBodyPartService>();

//repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IFarmRepository, FarmRepository>();
builder.Services.AddScoped<IUserFarmRepository, UserFarmRepository>();
builder.Services.AddDbContext<ApplicationContext>();
builder.Services.AddScoped<IInnogotchiRepository, InnogotchiRepository>();
builder.Services.AddScoped<IDeadInnogotchiRepository, DeadInnogotchiRepository>();
builder.Services.AddScoped<IInnogotchiBodyPartRepository, InnogotchiBodyPartRepository>();

//automapper
builder.Services.AddAutoMapper(typeof(AutoMapperUserProfile));
builder.Services.AddAutoMapper(typeof(AutoMapperFarmProfile));
builder.Services.AddAutoMapper(typeof(AutoMapperUserFarmProfile));
builder.Services.AddAutoMapper(typeof(AutoMapperInnogotchiProfile));

//validators
builder.Services.AddScoped<IUserValidatorFactory, UserValidatorFactory>();
builder.Services.AddScoped<IFarmValidatorFactory, FarmValidatorFactory>();
builder.Services.AddScoped<IInnogotchiValidatorFactory, InnogotchiValidatorFactory>();

builder.Services.AddScoped<IApplicationContext, ApplicationContext>();

builder.Services.AddAuthentication().AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        ValidateAudience = false,
        ValidateIssuer = false,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                builder.Configuration.GetSection("AppSettings:Token").Value!))
    };
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("FarmId", policy =>
    {
        policy.RequireAssertion(context =>
        {
            var httpContext = context.Resource as HttpContext;
            if (httpContext == null)
            {
                return false; // Ensure that we have an HttpContext
            }

            var routeFarmId = httpContext.Request.Query["farmId"].ToString();
            var farmId = context.User.FindFirst("FarmId")?.Value;

            Console.WriteLine($"FarmId: {farmId}, routeFarmId: {routeFarmId}");

            return farmId == routeFarmId;
        });
    });
});




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
