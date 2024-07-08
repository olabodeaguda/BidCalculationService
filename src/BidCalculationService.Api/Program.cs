using BidCalculationService.Application;
using Microsoft.AspNetCore.Mvc;
using BidCalculationService.Infrastruture;
using BidCalculationService.Api.Helpers;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.AspNetCore.Diagnostics;
using System.Text.Json.Serialization;
using BidCalculationService.Api.Configurations;


var builder = WebApplication.CreateBuilder(args);

string env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";
var configuration = builder.Configuration
    .AddJsonFile($"appsettings.{env}.json", optional: true, reloadOnChange: true)
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables()
    .Build();

builder.Services.AddExceptionHandler<ExceptionHandler>();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(configuration);
builder.Services.AddDatabase(configuration);
builder.Services.AddOpenIDAuthentication(configuration);

builder.Services.AddCors(Options =>
{
    Options.AddPolicy("AllowCors",
        builder =>
        builder
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader()
            );
});

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddApiVersioning(options =>
{
    options.ReportApiVersions = true;
}).AddApiExplorer(
    options =>
    {
        options.GroupNameFormat = "'v'VVV";
        options.SubstituteApiVersionInUrl = true;
    });

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

var app = builder.Build();

app.UseCors("AllowCors");
app.UseSwagger();
app.UseSwaggerUI(_ =>
{
    _.SwaggerEndpoint("swagger/v1/swagger.json", "Car bidding calculation service");
    _.RoutePrefix = string.Empty;
_.OAuthClientId(configuration.GetValue<string>("JwtSettings:ClientId"));
    _.OAuthClientSecret(configuration.GetValue<string>("JwtSettings:ClientSecret"));
    _.OAuthUsePkce();
});

app.UseHttpsRedirection();
app.UseExceptionHandler(option =>
{
    option.Run(async context =>
    {
        var exceptionHandler = app.Services.GetRequiredService<IExceptionHandler>();
        await exceptionHandler.TryHandleAsync(context, context.Features.Get<IExceptionHandlerFeature>()?.Error!, CancellationToken.None);
    });
});

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
