using BusinessLayer;
using BusinessLayer.Interfaces;
using BussinesLayer.Interfaces;
using DataAccessLayer.Data;
using DataLayer.Repositories;
using DataLayer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// ?? 1. Add DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// ?? 2. Add Controllers
builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
        builder.AllowAnyOrigin() // Allow all origins
               .AllowAnyHeader() // Allow all headers
               .AllowAnyMethod()); // Allow all HTTP methods
});


builder.Services.AddScoped<IPharmacyRepository, PharmacyRepository>();  // Customer Repository
builder.Services.AddScoped<IPharmacyService, PharmacyService>();

builder.Services.AddScoped<IPostRepository, PostRepository>();
builder.Services.AddScoped<IPostService, PostService>();

builder.Services.AddScoped<IRequestRepository, RequestRepository>();
builder.Services.AddScoped<IRequestService, RequestService>();

builder.Services.AddScoped<ICartRepository, CartRepository>();
builder.Services.AddScoped<ICartService, CartService>();


builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// ?? 3. Add Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// ?? 4. Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("AllowAll");

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
