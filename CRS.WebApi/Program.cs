using CRS.WebApi;
using CRS.WebApi.Data;
using CRS.WebApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Converters;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthorization();
builder.Services.AddSingleton<WeatherService>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer();
builder.Services.ConfigureOptions<JwtBearerConfigureOptions>();

builder.Services.AddControllers().AddNewtonsoftJson(options => options.SerializerSettings.Converters.Add(new StringEnumConverter()));

builder.Services.AddDbContext<CrsdbContext>((provider, options) => {
    IConfiguration config = provider.GetRequiredService<IConfiguration>();
    options.UseSqlServer(config.GetConnectionString("DBCon"));
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(
    options =>
    {
        options.EnableAnnotations();
        options.SwaggerDoc("v1", new OpenApiInfo { Title = "CentralRevenueServiceAPI", Version = "v1" });
        options.DocumentFilter<CustomModelDocumentFilter<VerificationRequest>>();
    });
builder.Services.AddSwaggerGenNewtonsoftSupport();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("/weatherforecast", (WeatherService weatherService) => weatherService.GetForecast())
    .WithName("GetWeatherForecast").RequireAuthorization()
    .RequireAuthorization();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
