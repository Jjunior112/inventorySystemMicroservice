using MassTransit;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Swagger
builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();

// DbContext
builder.Services.AddDbContext<OperationDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    options.UseSqlServer(connectionString);
    options.LogTo(Console.WriteLine, LogLevel.Information);
});

// API Versioning
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
        options.Authority = builder.Configuration["Authentication:Authority"]; // exemplo: http://authservice:8080
        options.RequireHttpsMetadata = false; // ajustar conforme ambiente
        options.Audience = builder.Configuration["Authentication:Audience"]; // exemplo: "operationservice"
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = true,
            ValidateIssuer = true
            // Configurações adicionais se precisar
        };
    });

builder.Services.AddAuthorization();

// MassTransit
builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<OperationCreatedConsumer>();

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(new Uri("amqp://rabbitmq:5672"), host =>
        {
            host.Username("guest");
            host.Password("guest");
        });

        cfg.ReceiveEndpoint("operation-created-queue", e =>
        {
            e.ConfigureConsumer<OperationCreatedConsumer>(context);
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

// Application Services
builder.Services.AddScoped<OperationService>();

// Controllers + JsonEnumConverter
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

var app = builder.Build();

// Aplica migrações DB automaticamente
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<OperationDbContext>();
    db.Database.Migrate();
}

// Middleware pipeline
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
