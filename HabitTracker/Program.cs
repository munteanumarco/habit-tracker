using System.Text;
using HabitTracker.DbContexts;
using HabitTracker.Entities;
using HabitTracker.Middleware;
using HabitTracker.Repositories;
using HabitTracker.Repositories.Interfaces;
using HabitTracker.Services;
using HabitTracker.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

ConfigureServices(builder);

var app = builder.Build();

ConfigureApp(app);

app.Run();


void ConfigureServices(WebApplicationBuilder builder)
{
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();

    ConfigureSwagger(builder);
    ConfigureDatabase(builder);
    ConfigureAuthentication(builder);
    ConfigureIdentity(builder);
    
    builder.Services.AddScoped<IHabitTrackerRepository, HabitTrackerRepository>();
    builder.Services.AddScoped<IHabitService, HabitService>();
    builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
}

void ConfigureSwagger(WebApplicationBuilder builder)
{
    builder.Services.AddSwaggerGen(options =>
    {
        options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Name = "Authorization",
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
        });

        options.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                Array.Empty<string>()
            }
        });
    });
}

void ConfigureDatabase(WebApplicationBuilder builder)
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    builder.Services.AddDbContext<HabitTrackerDbContext>(options => options.UseNpgsql(connectionString));
}

void ConfigureAuthentication(WebApplicationBuilder builder)
{
    var jwtSettings = builder.Configuration.GetSection("JwtSettings");
    builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    }).AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings["Issuer"],
            ValidAudience = jwtSettings["Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]))
        };
    });
}

void ConfigureIdentity(WebApplicationBuilder builder)
{
    builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
        .AddEntityFrameworkStores<HabitTrackerDbContext>()
        .AddDefaultTokenProviders();
}

void ConfigureApp(WebApplication app)
{
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();
    app.UseAuthentication();
    app.UseAuthorization();
    app.UseMiddleware<RequestContextMiddleware>();
    app.MapControllers();
}