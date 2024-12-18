using E_commers.Application.DependencyInjection;
using E_commers.Application.Mapping;
using E_commers.Infrastructure.DependencyInjection;
using Serilog;

namespace E_commerce
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day) // Log to file with rolling daily logs
                .CreateLogger();

            builder.Host.UseSerilog();
            Log.Logger.Information("Application is loading.....");

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddInfrastructuereService(builder.Configuration);
            builder.Services.AddApplicationService();

            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(policy =>
                    policy.AllowAnyHeader()
                          .AllowAnyMethod()
                          .AllowAnyOrigin());
            });


            try
            {
                var app = builder.Build();
                app.UseCors();
                app.UseSerilogRequestLogging();
                // Configure the HTTP request pipeline.
                if (app.Environment.IsDevelopment())
                {
                    app.UseSwagger();
                    app.UseSwaggerUI();
                }
                app.UseInfrastructureServece();

                app.UseHttpsRedirection();

                app.UseAuthorization();


                app.MapControllers();
                Log.Logger.Information("appllication is running.........");
                app.Run();
            }
            catch(Exception ex) 
            {
                Log.Logger.Error(ex, "appliaction failed to start");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
    }
}
