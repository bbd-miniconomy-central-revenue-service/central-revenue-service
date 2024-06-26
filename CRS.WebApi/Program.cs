using CRS.WebApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddMvc().AddNewtonsoftJson();

builder.Services.AddDbContext<CrsdbContext>((provider, options) => {
    IConfiguration config = provider.GetRequiredService<IConfiguration>();
    options.UseSqlServer(config.GetConnectionString("DBCon"));
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(
    c =>
    {
        c.EnableAnnotations();
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "CentralRevenueServiceAPI", Version = "v1" });
    });

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();
app.MapControllers();

app.Run();
