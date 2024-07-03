using CRS.WebApi;
using CRS.WebApi.Data;
using CRS.WebApi.Models;
using CRS.WebApi.Repositories;
using CRS.WebApi.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Converters;
// using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// builder.Services.AddAuthorization();
builder.Services.AddSingleton<WeatherService>();

// builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//     .AddJwtBearer();
// builder.Services.ConfigureOptions<JwtBearerConfigureOptions>();

string? allAllowedOrigins = builder.Configuration["AppSettings:AllowedOrigins"];

string[] allowedOrigins = [];

if (allAllowedOrigins != null)
{
    allowedOrigins = allAllowedOrigins.Split(",");
}

builder.Services.AddControllers().AddNewtonsoftJson(options => options.SerializerSettings.Converters.Add(new StringEnumConverter()));

string originsKey = "origins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: originsKey,
        policy =>
        {
            policy.WithOrigins(allowedOrigins)
                .WithMethods("GET")
                .AllowAnyHeader();
        });
});

IConfiguration config = new ConfigurationBuilder()
                          .SetBasePath(Directory.GetCurrentDirectory())
                          .AddJsonFile("appsettings.json", false, false)
                          .AddJsonFile($"appsettings.Production.json", true, true)
                          .AddEnvironmentVariables()
                          .Build();
                          
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

builder.Services.AddScoped<UnitOfWork>();

builder.Services.AddHostedService<BackgroundWorker>();

builder.Services.AddScoped<IScopedProcessingService, DefaultScopedProcessingService>();

builder.Services.AddScoped<TaxCalculatorFactory>();

builder.Services.AddScoped<TaxCalculatorService>();

builder.Services.AddHttpClient<HandOfZeusService>();
builder.Services.AddHttpClient<PersonaService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("/weatherforecast", (WeatherService weatherService) => weatherService.GetForecast())
    .WithName("GetWeatherForecast");
    // .RequireAuthorization();

// app.UseAuthentication();

app.UseOriginWhitelist([
    "retail_bank",
    "commercial_bank",
    "health_insurance",
    "life_insurance",
    "short_term_insurance",
    "health_care",
    "central_revenue",
    "labour",
    "stock_exchange",
    "real_estate_sales",
    "real_estate_agent",
    "short_term_lender",
    "home_loans",
    "electronics_retailer"
    ]);
// app.UseAuthorization();
// app.UseAuthentication();

    app.UseCors(originsKey);

app.MapControllers();

app.Run();
