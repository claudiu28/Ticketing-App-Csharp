using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using Persistence.Utils;
using Persistence.Repositories;
using Persistence.Repositories.EF_Implementation;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text.Json.Serialization;
using Microsoft.IdentityModel.Tokens;
using System.Text;
var builder = WebApplication.CreateBuilder(args);


var config = new ConfigurationBuilder()
    .SetBasePath(AppContext.BaseDirectory)
    .AddJsonFile("AppSettings.json", optional: false, reloadOnChange: true)
    .Build();

var connectionString = config.GetConnectionString("bd.url") ?? throw new Exception("Connection string null");
var frontendUrl = config.GetValue<string>("frontend:url")
                  ?? throw new Exception("Frontend URL null");
var optionsBuilder = new DbContextOptionsBuilder<ContextDb>();
optionsBuilder.UseSqlite(connectionString);


builder.Services.AddDbContext<ContextDb>(opt => opt.UseSqlite(connectionString));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
             builder => builder
                 .WithOrigins("http://localhost:3000") 
                 .AllowAnyMethod()
                 .AllowAnyHeader()
                 .AllowCredentials());
});

builder.Services.AddScoped<IRepoMatch, MatchRepositoryEF>();
builder.Services.AddScoped<IRepoUser, UserRepositoryEF>();

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options => { 
    var secret = config["JwtSettings:Secret"] ?? throw new Exception("Secret is out!");
    var issuer = config["JwtSettings:Issuer"] ?? throw new Exception("Issuer is out!");
    var audience = config["JwtSettings:Audience"] ?? throw new Exception("Audience is out!");

    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = issuer,
        ValidAudience = audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret)),
        ClockSkew = TimeSpan.Zero
    };


});
builder.Services.AddSingleton<RestService.HelperJWT>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowSpecificOrigin");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.MapGet("/", context =>
{
    context.Response.Redirect("/swagger");
    return Task.CompletedTask;
});

app.Run();