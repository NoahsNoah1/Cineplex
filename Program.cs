using CinePlex.models;
using CinePlex.Models;
using CinePlex.Repositories;
using CinePlex.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Net.Sockets;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<MovieContext>(options =>
{
	options.UseSqlServer(builder.Configuration.GetConnectionString("MyDefaultCS"));
});
//after building Generic repos and Igeneric repos
builder.Services.AddScoped<IGenericRepository<Movie>, GenericRepository<Movie>>();
builder.Services.AddScoped<IGenericRepository<ticket>, GenericRepository<ticket>>();
builder.Services.AddScoped<IGenericRepository<show>, GenericRepository<show>>();
builder.Services.AddScoped<IGenericRepository<user>, GenericRepository<user>>();
builder.Services.AddScoped<IGenericRepository<theater>, GenericRepository<theater>>();
builder.Services.AddScoped<IuserRepository, userRepository>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IticketService, ticketService>();
builder.Services.AddScoped<IshowService, showService>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "https://localhost:7064",
            ValidAudience = "https://localhost:7064",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:SecurityKey"]))

        };
    });
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("MyBearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Description = "Jwt Authentication Header",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Id= "MyBearer",
                    Type= ReferenceType.SecurityScheme
                },
                In = ParameterLocation.Header,
                Name= "Bearer",
                Scheme= "oauth2"
            },
            new List<string>()
        }
    });
});
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
            policy =>
            {
                policy.AllowAnyOrigin();
                policy.AllowAnyHeader();
                policy.AllowAnyMethod();
            }
        );
  });
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors();
app.UseHttpsRedirection();
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
