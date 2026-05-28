using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using webapidemo.Data;
using webapidemo.Services;

namespace demowebapi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder =
                WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();

            builder.Services.AddOpenApi();

            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<AppDbContext>(
                options => options.UseSqlServer(
                    builder.Configuration
                    .GetConnectionString(
                        "DefaultConnection")));

            // Register Services

            builder.Services.AddScoped<
                IProductService,
                ProductService>();

            builder.Services.AddScoped<
                ICategoryService,
                CategoryService>();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();

                app.MapScalarApiReference();

                app.UseSwagger();

                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint(
                        "/swagger/v1/swagger.json",
                        "demowebapi v1");

                    options.RoutePrefix =
                        string.Empty;
                });
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}