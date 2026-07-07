using Microsoft.EntityFrameworkCore;
using PetopiaWebApi.Models;
using Npgsql.EntityFrameworkCore.PostgreSQL;

namespace PetopiaWebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // 1. Retrieve and validate the connection string safely
            var connectionString = builder.Configuration.GetConnectionString("dbcs")
                ?? throw new InvalidOperationException("Connection string 'dbcs' not found.");

            // Add services to the container
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // 2. FIXED: Replaced YourDbContext with PetopiaDbContext and used the connectionString variable
            builder.Services.AddDbContext<PetopiaDbContext>(options =>
                options.UseNpgsql(connectionString));

            // Enable CORS 
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", builder =>
                {
                    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
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

            // Apply CORS policy
            app.UseCors("AllowAll");

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}