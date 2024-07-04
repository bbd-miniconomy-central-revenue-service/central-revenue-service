using CRS.WebApi;
using CRS.WebApi.Data;
using CRS.WebApi.Models;
using CRS.WebApi.Repositories;
using CRS.WebApi.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Converters;

var builder = WebApplication.CreateBuilder(args);

string? allAllowedOrigins = builder.Configuration["AppSettings:AllowedOrigins"];

string[] allowedOrigins = [];

if (allAllowedOrigins != null)
{
    allowedOrigins = allAllowedOrigins.Split(",");
}

builder.Services.AddControllers().AddNewtonsoftJson(options => options.SerializerSettings.Converters.Add(new StringEnumConverter()));


builder.Services.AddDbContext<CrsdbContext>((provider, options) =>
{
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
        options.OperationFilter<CustomOperationFilter>();
    });
builder.Services.AddSwaggerGenNewtonsoftSupport();

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

builder.Services.AddScoped<UnitOfWork>();

builder.Services.AddHostedService<BackgroundWorker>();

builder.Services.AddScoped<IScopedProcessingService, DefaultScopedProcessingService>();

builder.Services.AddScoped<TaxCalculatorFactory>();

builder.Services.AddScoped<TaxCalculatorService>();

builder.Services.AddScoped<PaymentVerificationService>();

IConfiguration config = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", false, false)
    .AddJsonFile($"appsettings.Production.json", true, true)
    .AddEnvironmentVariables()
    .Build();

builder.Services.AddHttpClient<HandOfZeusService>();
builder.Services.AddHttpClient<PersonaService>();
builder.Services.AddHttpClient<CommercialBankService>();

var app = builder.Build();

app.UseCors(originsKey);

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseSwagger();
app.UseSwaggerUI();

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
    "electronics_retailer",
    "food_retailer"
    ]);

app.MapControllers();

app.Run();
