using InventoryMaster.Interfaces;
using InventoryMaster.Services;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Text.Json.Serialization;

namespace InventoryMaster
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // ������� �������� ���� � ��������� ����
            Log.Logger = new LoggerConfiguration()
            .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day, outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}")
            .CreateLogger();


            // ����������� ��������� ���� ������
            builder.Services.AddDbContext<ItemsDBContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // ����������� �������
            builder.Services.AddTransient<IItemService, ItemService>();

            // ���������� ������������
            builder.Services.AddControllers().AddJsonOptions(options =>
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

            // ���������� ��������� OpenAPI ������������
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddHostedService<ZeroQuantityItemsCleanupService>();//o


            Log.Information("������ ���� � ��������� ����");

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                // ��������� Swagger UI � ������ ����������
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();

<<<<<<< HEAD
            //JanarGay
=======
            // ���������� ��������� ��� ������������
>>>>>>> Nazarq
            app.MapControllers();

            app.Run();
        }
    }
}