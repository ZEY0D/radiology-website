using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using RadiologyHIS.API.Data;
using RadiologyHIS.API.Data.Service;
using RadiologyHIS.API.Data.Services;
using RadiologyHIS.API.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add CORS (needed for frontend Axios integration)
builder.Services.AddCors();

// Database (PostgreSQL with Npgsql)
builder.Services.AddDbContext<HISContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("HISConnection")));

// Dependency Injection
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IPatientService, PatientService>();
builder.Services.AddScoped<IDoctorService, DoctorService>();
builder.Services.AddScoped<IAppointmentService, AppointmentService>();
builder.Services.AddScoped<IScanService, ScanService>();
builder.Services.AddScoped<IInquiryService, InquiryService>();

var app = builder.Build();

// Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// HTTPS redirection
app.UseHttpsRedirection();

// Enable static file serving
app.UseStaticFiles(); // Serves from wwwroot

// Serve profile images via /images/profile
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "profile")),
    RequestPath = "/images/profile"
});

// CORS (allow frontend access)
app.UseCors(policy =>
    policy.AllowAnyOrigin()
          .AllowAnyMethod()
          .AllowAnyHeader());

// Map routes
app.MapControllers();

app.Run();
