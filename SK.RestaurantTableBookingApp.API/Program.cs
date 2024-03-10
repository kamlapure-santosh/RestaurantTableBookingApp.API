
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Serilog;
using SK.RestaurantTableBookingApp.API;
using SK.RestaurantTableBookingApp.Data;
using SK.RestaurantTableBookingApp.Service;
using System.Net;

namespace RestaurantTableBookingApp.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Configure Serilog with the settings
            Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .WriteTo.Debug()
            .MinimumLevel.Information()
            .Enrich.FromLogContext()
            .CreateBootstrapLogger();

            try
            {
                var builder = WebApplication.CreateBuilder(args);
                var configuration = builder.Configuration;
                //------------------------------------ApplicationInsights

                builder.Services.AddApplicationInsightsTelemetry();

                builder.Host.UseSerilog((context, services, loggerConfiguration) => loggerConfiguration
                .WriteTo.ApplicationInsights(
                  services.GetRequiredService<TelemetryConfiguration>(),
                  TelemetryConverter.Events));

                Log.Information("Starting the application...");

                //------------------------------------ApplicationInsights
                //------------------------------------Add services

                // Add services to the container.
                //AddScoped =DI configuration, one instance per http request
                //AddTransient = every single time you inject / ask for instance, new instance is given|
                //AddSingleton = // for whole app only one instance, danger for common use as its not thread safe

                builder.Services.AddScoped<IRestaurantRepository, RestaurantRepository>();
                builder.Services.AddScoped<IRestaurantService, RestaurantService>();
                //------------------------------------Add services

                builder.Services.AddDbContext<RestaurantTableBookingDbContext>(options =>
                    options.UseSqlServer(configuration.GetConnectionString("DbContext") ?? "")
                        .EnableSensitiveDataLogging()//Only use for Development, not for production, its for getting database related issues in console
                    );

                builder.Services.AddControllers();
                // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
                builder.Services.AddEndpointsApiExplorer();
                builder.Services.AddSwaggerGen();

                var app = builder.Build();

                //------------------------------------ExceptionHandler
                //Exception hanlding. Create a middleware and include that here
                // Enable Serilog exception logging
                app.UseExceptionHandler(errorApp =>
                {
                    errorApp.Run(async context =>
                    {
                        var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
                        var exception = exceptionHandlerPathFeature?.Error;

                        Log.Error(exception, "Unhandled exception occurred. {ExceptionDetails}", exception?.ToString());
                        Console.WriteLine(exception?.ToString());
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        await context.Response.WriteAsync("An unexpected error occurred. Please try again later.");
                    });
                });
                app.UseMiddleware<RequestResponseLoggingMiddleware>();
                //------------------------------------ExceptionHandler
                //------------------------------------SwaggerUI

                // Configure the HTTP request pipeline.
                //if (app.Environment.IsDevelopment())
                //{
                app.UseSwagger();
                app.UseSwaggerUI();
                //}
                //------------------------------------SwaggerUI

                app.UseHttpsRedirection();

                app.UseAuthorization();


                app.MapControllers();

                app.Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
    }
}