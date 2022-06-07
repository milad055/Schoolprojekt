using Microsoft.EntityFrameworkCore;
using SchoolApi.Data;
using SchoolApi.Interfaces;
using SchoolApi.Repositories;
using SchoolApi.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


// Skapa databas koppling...
builder.Services.AddDbContext<CourseContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("Sqlite"))
);

// Sätt upp Identity hanteringen.

builder.Services.AddIdentity<IdentityUser, IdentityRole>(

  options =>

    {

      options.Password.RequireLowercase = true;

      options.Password.RequireUppercase = true;

      options.Password.RequiredLength = 6;

      options.Password.RequireNonAlphanumeric = false;



      options.User.RequireUniqueEmail = true;



      options.Lockout.MaxFailedAccessAttempts = 5;

      options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);

    }

).AddEntityFrameworkStores<CourseContext>();
builder.Services.AddAuthentication(options =>

{

  options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;

  options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

}).AddJwtBearer(options =>

{

  options.TokenValidationParameters = new TokenValidationParameters

  {

    ValidateIssuerSigningKey = true,

    IssuerSigningKey = new SymmetricSecurityKey(

          Encoding.ASCII.GetBytes(builder.Configuration.GetValue<string>("apiKey"))

      ),

    ValidateLifetime = true,

    ValidateAudience = false,

    ValidateIssuer = false,

    ClockSkew = TimeSpan.Zero

  };

});

// Depency injection för våra egna Interface och klasser...
// builder.Services.AddScoped<Interface, konkret klass som implementerar föregånde interface>...
builder.Services.AddScoped<ICourseRepository, CourseRepository>();
builder.Services.AddScoped<IStudentsRepository, StudentsRepository>();
builder.Services.AddScoped<ITeachersRepository, TeachersRepository>();


// Add automapper...
builder.Services.AddAutoMapper(typeof(AutoMappersProfiles).Assembly);

builder.Services.AddControllers();
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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
