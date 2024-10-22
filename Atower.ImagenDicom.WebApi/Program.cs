using Atower.ImagenDicom.WebApi.Aplicacion.Interfaces;
using Atower.ImagenDicom.WebApi.Aplicacion.Servicios;
using Atower.ImagenDicom.WebApi.Dominio.Interfaces;
using Atower.ImagenDicom.WebApi.Infraestrutura.Repositorios;
using FellowOakDicom;
using FellowOakDicom.Network.Client;

namespace Atower.ImagenDicom.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddFellowOakDicom();

          //  builder.Services.AddSingleton<IDicomClientFactory, DicomClientFactory>();
            builder.Services.AddSingleton<IDicomRepositorio, DicomRepositorio>();
            builder.Services.AddScoped<IDicomServicio, DicomServicio>();

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            DicomSetupBuilder.UseServiceProvider(app.Services);

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}
