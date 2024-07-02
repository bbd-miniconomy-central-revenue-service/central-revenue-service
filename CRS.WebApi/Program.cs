using CRS.WebApi.Data;
using CRS.WebApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Converters;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(opts =>
    {
        var userPoolId = Environment.GetEnvironmentVariable("USERPOOL_ID")!;
        opts.Authority = $"https://cognito-idp.eu-west-1.amazonaws.com/{userPoolId}";
        opts.MetadataAddress = $"https://cognito-idp.eu-west-1.amazonaws.com/{userPoolId}/.well-known/openid-configuration";
        opts.IncludeErrorDetails = true;
        opts.RequireHttpsMetadata = false;
        opts.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            RoleClaimType = "cognito:groups"
        };
    });

builder.Services.AddAuthorization(options => {
    options.AddPolicy("admin", policy => policy.RequireClaim("cognito:groups", "admin"));
    options.AddPolicy("user", policy => policy.RequireClaim("cognito:groups", "user"));
});

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

app.UseAuthorization();
app.MapControllers();

app.Run();
