using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Movies.API.Extensions;
using Movies.Presentation;

namespace Movies.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.ConfigureSql(builder.Configuration);

            // Add services to the container.

            builder.Services.AddControllers(opt => opt.ReturnHttpNotAcceptable = true)
                .AddApplicationPart(typeof(AssemblyReference).Assembly);

            builder.Services.AddSwaggerGen(opt => { opt.EnableAnnotations(); });
            builder.Services.AddRepositories();
            builder.Services.AddServiceLayer();

            var app = builder.Build();




            app.ConfigureExceptionHandler();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(opt =>
                {
                    opt.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                });
                await app.SeedDataAsync();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}
