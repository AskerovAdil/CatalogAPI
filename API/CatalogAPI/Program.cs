using CatalogAPI.Models;
using DataAccess;
using DataAccess.Abstract;
using DataAccess.Repositories;
using CatalogAPI.Profiles;
using Domain.Abstract;
using Domain.Services;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Events;

namespace CatalogAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Host.ConfigureLogging(logging =>
            {
                logging.ClearProviders();
                logging.AddSerilog(new LoggerConfiguration()
                    .MinimumLevel.Debug()
                    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                    .Enrich.FromLogContext()
                    .WriteTo.Console()
                    .WriteTo.File("log.txt", rollingInterval: RollingInterval.Day)
                    .CreateLogger());
            });

            builder.Services.AddScoped<ICategoryService, CategoryServices>();
            builder.Services.AddScoped<IProductService, ProductServices>();
            builder.Services.AddScoped<IEntityBaseRepository<Category>, EntityBaseRepository<Category>>();
            builder.Services.AddScoped<IEntityBaseRepository<Product>, EntityBaseRepository<Product>>();
            builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));  

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();


            app.MapControllers();

            app.Run();
        }
    }
}