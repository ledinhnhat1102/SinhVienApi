using Microsoft.EntityFrameworkCore;
using SinhVienApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddDbContext<SinhVienContext>(opt =>
	opt.UseSqlServer(builder.Configuration.GetConnectionString("SinhVienContext")));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure CORS
builder.Services.AddCors(options =>
{
	options.AddPolicy("AllowSpecificOrigin",
		builder => builder.WithOrigins("http://localhost:4200")
						  .AllowAnyHeader()
						  .AllowAnyMethod());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

// Use CORS policy
app.UseCors("AllowSpecificOrigin");

app.MapControllers();

app.Run();
