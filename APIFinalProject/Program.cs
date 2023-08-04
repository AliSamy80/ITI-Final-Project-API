using APIFinalProject.Models;
using APIFinalProject.Repository.Base;
using APIFinalProject.Repository;
using APIFinalProject.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddScoped<IAuthManager, AuthManager>();
builder.Services.AddSwaggerGen();


var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(connectionString));




builder.Services.AddAuthentication();
builder.Services.ConfigureIdentity();

builder.Services.AddTransient(typeof(IRepository<>), typeof(MainRepository<>));
builder.Services.AddTransient<IUnitOfWork, UnitOFWork>();
//builder.Services.ConfigurationJWT(builder.Configuration);

builder.Services.AddCors(corsOptions =>
{
    corsOptions.AddPolicy("allowAllPolicy", corsBuilder =>
    {
        corsBuilder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.UseCors("allowAllPolicy");

app.UseStaticFiles();
app.MapControllers();

app.Run();
