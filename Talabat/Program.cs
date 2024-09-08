
using Microsoft.EntityFrameworkCore;
using Talabat.Repository.Data;

namespace Talabat
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // opene connection with database
            builder.Services.AddDbContext<TalabatDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("defaultConnection"));
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();


            // Update Database when project run
            var scope=app.Services.CreateScope();
            var services=scope.ServiceProvider;
            var dbcontext=services.GetRequiredService<TalabatDbContext>();
            await dbcontext.Database.MigrateAsync();
            // make dataseeding in database
            await TalabatDataSeed.SeedAsync(dbcontext);


            app.Run();
        }
    }
}
