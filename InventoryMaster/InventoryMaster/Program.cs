using InventoryMaster.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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
