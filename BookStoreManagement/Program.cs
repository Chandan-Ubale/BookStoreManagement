using BookStoreManagement.Core.Interfaces;
using BookStoreManagement.Core.Models;
using BookStoreManagement.Infra.Data;
using BookStoreManagement.Infra.Repositories;
using BookStoreManagement.Services.Services;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Bind appsettings.json -> BookstoreDatabaseSettings
builder.Services.Configure<BookstoreDatabaseSettings>(
    builder.Configuration.GetSection("BookstoreDatabaseSettings"));

// Register infrastructure and services
builder.Services.AddSingleton<MongoDbContext>();
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IBookService, BookService>();

// Add controllers + Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "BookStoreManagement API",
        Version = "v1",
        Description = "API for managing books in a bookstore",
        Contact = new OpenApiContact
        {
            Name = "Your Name",
            Email = "your@email.com"
        }
    });
    c.EnableAnnotations();
});

var app = builder.Build();

// Enable Swagger only in development
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "BookStoreManagement API v1");
    });
}

app.UseAuthorization();
app.MapControllers();
app.Run();
