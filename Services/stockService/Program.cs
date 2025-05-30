using MassTransit;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// DbContext
builder.Services.AddDbContext<StockDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    options.UseSqlServer(connectionString);
    options.LogTo(Console.WriteLine, LogLevel.Information);
});

// Versionamento
builder.Services.AddApiVersioning(options =>
{
    options.ReportApiVersions = true;
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.ApiVersionReader = new UrlSegmentApiVersionReader();
});

// Autenticação JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = builder.Configuration["Authentication:Authority"];  // Exemplo: http://authservice:8080
        options.RequireHttpsMetadata = false; // Ajustar para true em produção
        options.Audience = builder.Configuration["Authentication:Audience"];   // Exemplo: "stocksservice"
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = true,
            ValidateIssuer = true,
            // Outras validações, se necessário
        };
    });

builder.Services.AddAuthorization();

// MassTransit
builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<ProductCreatedConsumer>();

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(new Uri("amqp://rabbitmq:5672"), host =>
        {
            host.Username("guest");
            host.Password("guest");
        });

        cfg.ReceiveEndpoint("product-created-queue", e =>
        {
            e.ConfigureConsumer<ProductCreatedConsumer>(context);
        });
    });
});

// Cache Redis
builder.Services.AddScoped<ICachingService, RedisCachingService>();

builder.Services.AddStackExchangeRedisCache(o =>
{
    o.InstanceName = "instance";
    o.Configuration = "redis:6379";
});

// Serviços da aplicação
builder.Services.AddScoped<StockService>();

// Controllers
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<StockDbContext>();
    db.Database.Migrate();
}

// Pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();  // <-- Middleware de autenticação
app.UseAuthorization();   // <-- Middleware de autorização

app.MapControllers();

app.Run();
