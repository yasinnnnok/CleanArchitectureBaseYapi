using CleanArchitecture.Application.Behaviors;
using CleanArchitecture.Application.Services;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Repositories;
using CleanArchitecture.Persistance.Context;
using CleanArchitecture.Persistance.Repositories;
using CleanArchitecture.Persistance.Services;
using CleanArchitecture.WebApi.Middleware;
using CleanArchitecture.WebApi.OptionsSetup;
using FluentValidation;
using GenericRepository;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<ICarService, CarService>();
builder.Services.AddTransient<ExceptionMiddleware>()
    ;
builder.Services.AddScoped<ICarRepository, CarRepository>();
builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddScoped<IUnitOfWork>(cfr => cfr.GetRequiredService<AppDbContext>());

builder.Services.ConfigureOptions<JwtOptionsSetup>();
builder.Services.ConfigureOptions<JwtBearerOptionsSetup>();

builder.Services.AddAuthentication().AddJwtBearer();

builder.Services.AddAutoMapper(typeof(CleanArchitecture.Persistance.AssemblyRefence).Assembly);

// Add services to the container.
string connectionString = builder.Configuration.GetConnectionString("SqlServer");
builder.Services.AddDbContext<AppDbContext>(options =>options.UseSqlServer(connectionString));
//Identity
builder.Services.AddIdentity<User, IdentityRole>(options =>
{
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 1;
    options.Password.RequireUppercase = false;
}
    ).AddEntityFrameworkStores<AppDbContext>();



builder.Services.AddControllers()
    .AddApplicationPart(typeof(CleanArchitecture.Presentation.AssemblyReference).Assembly);

//mediaTr servis registerion u yap�ld�
builder.Services.AddMediatR(cfr => cfr.RegisterServicesFromAssembly(typeof(CleanArchitecture.Application.AssemblyReference).Assembly));

builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaivor<,>));
builder.Services.AddValidatorsFromAssembly(typeof(CleanArchitecture.Application.AssemblyReference).Assembly);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddlewareExtensions();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
