using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

//CORS

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:5173")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Swagger

builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();

//UserDbContext

builder.Services.AddDbContext<UserDbContext>(options =>
{

    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

    options.UseSqlServer(connectionString);

    options.LogTo(Console.WriteLine, LogLevel.Information);


});

// versionamento 

builder.Services.AddApiVersioning(options =>
{
    options.ReportApiVersions = true;
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.ApiVersionReader = new UrlSegmentApiVersionReader();
});


//Services e repository

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();



builder.Services.AddControllers();


var app = builder.Build();

// Habilita CORS antes do MapControllers

app.UseCors("AllowFrontend");

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<UserDbContext>();
    db.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();


app.Run();


