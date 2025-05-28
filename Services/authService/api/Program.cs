using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

//Swagger 

builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();

//AuthDbContext

builder.Services.AddDbContext<AuthDbContext>((options) =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

    options.UseSqlServer(connectionString);
    options.LogTo(Console.WriteLine, LogLevel.Information);

});

//Versionamento

builder.Services.AddApiVersioning(options =>
{
    options.ReportApiVersions = true;
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.ApiVersionReader = new UrlSegmentApiVersionReader();
});


//Services e repository



builder.Services.AddControllers();



var app = builder.Build();


using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AuthDbContext>();

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

