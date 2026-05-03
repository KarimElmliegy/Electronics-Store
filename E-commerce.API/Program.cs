
using E_commerce.Core.Interfaces;
using E_commerce.Infrastructure.Data;
using E_commerce.Infrastructure.Data.SeedData;
using Microsoft.EntityFrameworkCore;

namespace E_commerce.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<AppDbContext>(options=>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services.AddScoped<IProductRepository, ProductRepository>(); 
            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));




            var app = builder.Build();


            try
            {
                using var scope = app.Services.CreateScope();
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<AppDbContext>();
                await context.Database.MigrateAsync();
                await StoreContextSeed.SeddAsynch(context);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.UseSwagger();
                app.UseSwaggerUI( s => 
                     {
                         s.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
                         s.RoutePrefix = ""; 
                    });
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
