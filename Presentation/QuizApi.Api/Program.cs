using QuizAPI.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.FileProviders;
using QuizApi.Domain.StaticVariables;
using QuizApi.Api;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddPersistenceServices();
builder.Services.AddControllers();

// JWT Authentication
var key = Encoding.UTF8.GetBytes(StaticVariables.SecureKey); // Güvenli bir anahtar belirleyin
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("0iABIYPO/aKD9vnMEtoPzJM+9Tn7hrUrBmClVKSoo1o=")) // Burada sabit anahtar kullanýlmalý
    };
});

// Swagger yapýlandýrmasý
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "QuizAPI", Version = "v1" });

    // JWT Authentication ayarlarý
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token in the format 'Bearer {your token}'",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "QuizAPI v1");
        c.RoutePrefix = "swagger"; // Swagger UI'ye eriþim yolu
    });
}

app.UseCors(builder =>
{
    builder.WithOrigins("https://localhost:7265") // Frontend URL'si
           .AllowAnyHeader()
           .AllowAnyMethod();
});

app.UseHttpsRedirection();

// Statik dosyalarýn sunulmasý
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(Directory.GetCurrentDirectory(), "Frontend")),
    RequestPath = "/Frontend"
});

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
//https://localhost:7265/Frontend/Views/Login/Login.html
//https://localhost:7265/Frontend/Views/Home/Home.html