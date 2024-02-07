using InventoryMaster.Interfaces;
using InventoryMaster.Services;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace InventoryMaster
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // ����������� ��������� ���� ������
            builder.Services.AddDbContext<ItemsDBContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // ����������� �������
            builder.Services.AddScoped<IItemService, ItemService>();

            // ���������� ������������
            builder.Services.AddControllers().AddJsonOptions(options =>
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

            // ���������� ��������� OpenAPI ������������
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                // ��������� Swagger UI � ������ ����������
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();

            // ���������� ��������� ��� ������������
            app.MapControllers();

            app.Run();
        }
    }
}
