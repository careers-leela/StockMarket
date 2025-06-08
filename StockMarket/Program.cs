using StockMarket.Repository;
using StockMarket.Service;

namespace StockMarket
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            builder.Services.AddScoped<IStockService, StockService>();
            builder.Services.AddSingleton<IStockRepository, StockRepository>();
            builder.Services.AddControllers();
            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();

            //Add Authentication here
            //app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
