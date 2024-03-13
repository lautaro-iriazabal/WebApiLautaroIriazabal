using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.Extensions.Options;
using WebApiLautaroIriazabal.database;
using WebApiLautaroIriazabal.Models;
using WebApiLautaroIriazabal.Service;


namespace WebApiLautaroIriazabal
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddScoped<UsuarioData>();
            builder.Services.AddScoped<ProductoData>();
            builder.Services.AddScoped<ProductoVendidoData>();
            builder.Services.AddScoped<VentaData>();


            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(policy => {
                    policy.AllowAnyMethod();
                    policy.AllowAnyOrigin();
                    policy.AllowAnyHeader();

                });

            });


            builder.Services.AddDbContext<CoderContext>(option =>    //Agrega un contexto de tipo DataBaseContext. aca se realiza la conexion.
            {
                string server = ".";
                string dataBase = "coderhouse";
                option.UseSqlServer(($"Server={server}; Database={dataBase}; Trusted_Connection=True;"));
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }


            app.UseCors();
            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();

        }
    }
}