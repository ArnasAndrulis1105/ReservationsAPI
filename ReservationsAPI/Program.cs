using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi;
using ReservationsAPI.Data;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ReservationsAPIContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ReservationsAPIContext") ?? throw new InvalidOperationException("Connection string 'ReservationsAPIContext' not found.")));


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazorFrontend",
        builder =>
        {
            // Update these ports to match your Blazor frontend
            builder.WithOrigins("https://localhost:7209", "http://localhost:5148")
                   .AllowAnyMethod()
                   .AllowAnyHeader()
                   .AllowCredentials();
        });
});
//builder.Services.AddMvc()
//                .AddJsonOptions(opt =>
//                {
//                    opt.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
//                });
// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
