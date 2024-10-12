using CatApiApp.Data;
using CatApiApp.Services;
using Microsoft.EntityFrameworkCore;
using Refit;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Set database
builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Read CatApi settings from configuration
var catApiSettings = builder.Configuration.GetSection("CatApi");
var apiKey = catApiSettings["ApiKey"];
var baseUrl = catApiSettings["BaseUrl"];

// Configure Refit to use the API key for all requests
builder.Services.AddRefitClient<ICatApiClient>()
    .ConfigureHttpClient(c =>
    {
        c.BaseAddress = new Uri(baseUrl);
        c.DefaultRequestHeaders.Add("x-api-key", apiKey);
    });

// Register CatService in DI container
builder.Services.AddScoped<CatService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();