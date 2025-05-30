using Microsoft.EntityFrameworkCore;
using MassTransit;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// DbContext
builder.Services.AddDbContext<ProductDbContext>(options =>
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
        options.Authority = builder.Configuration["Authentication:Authority"];
        options.RequireHttpsMetadata = false;
        options.Audience = builder.Configuration["Authentication:Audience"];
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = true,
            ValidateIssuer = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true

        };
    });

builder.Services.AddAuthorization();

// MassTransit
builder.Services.AddMassTransit(x =>
{
    x.UsingRabbitMq((ctx, cfg) =>
    {
        cfg.Host(new Uri("amqp://rabbitmq:5672"), host =>
        {
            host.Username("guest");
            host.Password("guest");
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

// Serviços de aplicação
builder.Services.AddScoped<ProductService>();

// Controllers
builder.Services.AddControllers();

var app = builder.Build();

// Aplica migrações
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ProductDbContext>();
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
