using CRS.WebApi.Data;
using CRS.WebApi.Models;
using CRS.WebApi.Repositories;
using CRS.WebApi.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Converters;

var builder = WebApplication.CreateBuilder(args);

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
        options.OperationFilter<CustomOperationFilter>();
    });
builder.Services.AddSwaggerGenNewtonsoftSupport();

builder.Services.AddScoped<UnitOfWork>();

builder.Services.AddHostedService<BackgroundWorker>();

builder.Services.AddScoped<IScopedProcessingService, DefaultScopedProcessingService>();

builder.Services.AddScoped<TaxCalculatorFactory>();

builder.Services.AddScoped<TaxCalculatorService>();

IConfiguration config = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", false, false)
    .AddJsonFile($"appsettings.Production.json", true, true)
    .AddEnvironmentVariables()
    .Build();

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
app.UseAuthorization();
app.UseAuthentication();
app.MapControllers();

app.Run();
