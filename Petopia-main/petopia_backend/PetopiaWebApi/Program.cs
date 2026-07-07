using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PetopiaWebApi.Models;
using PetopiaWebApi.Services;

namespace PetopiaWebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Connection string: comes from appsettings.json locally, or from the
            // ConnectionStrings__dbcs environment variable in production (Render/Azure/etc).
            var connectionString = builder.Configuration.GetConnectionString("dbcs")
                ?? throw new InvalidOperationException("Connection string 'dbcs' not found. Set it in appsettings.json (local dev) or as the ConnectionStrings__dbcs environment variable (production).");

            // Add services to the container
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Register DbContext with the retrieved connection string
            builder.Services.AddDbContext<PetopiaDbContext>(options =>
                options.UseSqlServer(connectionString));

            builder.Services.AddSingleton<TokenService>();

            // JWT authentication
            var jwtKey = builder.Configuration["Jwt:Key"]
                ?? throw new InvalidOperationException("Jwt:Key is not configured. Set it in appsettings.json (local dev) or as the Jwt__Key environment variable (production).");
            var jwtIssuer = builder.Configuration["Jwt:Issuer"] ?? "PetopiaWebApi";

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtIssuer,
                    ValidAudience = jwtIssuer,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
                };
            });

            // CORS: allowed origins come from config (AllowedOrigins array in appsettings.json,
            // or the AllowedOrigins__0, AllowedOrigins__1... environment variables in production).
            var allowedOrigins = builder.Configuration.GetSection("AllowedOrigins").Get<string[]>()
                ?? new[] { "http://localhost:5173" };

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AppCors", policy =>
                {
                    policy.WithOrigins(allowedOrigins)
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseCors("AppCors");

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
