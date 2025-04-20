using System.Text;
using CS_Base_Project.BLL.Helpers;
using CS_Base_Project.BLL.Services.Implements;
using CS_Base_Project.BLL.Services.Interfaces;
using CS_Base_Project.DAL.Data.Entities;
using CS_Base_Project.DAL.Data.Exceptions;
using CS_Base_Project.DAL.Data.Repositories;
using CS_Base_Project.DAL.Data.Repositories.Interfaces;
using CS_Base_Project.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;
// Add services to the container.
builder.Services.AddEndpointsApiExplorer(); // Required for Swagger UI
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo()
    {
        Title = "CS-Base-Project",
        Version = "v1",
        Description = "A base project for .NET applications"
    });
    
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. \n\r Enter 'Bearer' [space] and then your token in the text input below.\n\r Example: \"Bearer 12345abcdef\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer"
    });
    
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
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
            []
        }
    });
});

// Add database context
builder.Services.AddDbContext<AppDbContext>(options =>
    options
        .UseNpgsql(
            config.GetConnectionString("PostgresConnectionString"),
            npgsqlOptions => npgsqlOptions
                .EnableRetryOnFailure(
                    maxRetryCount: 5,
                    maxRetryDelay: TimeSpan.FromSeconds(30),
                    errorCodesToAdd: null
                )
            ) 
        .LogTo(Console.WriteLine, LogLevel.Information)
    );

// Add Authentication and Authorization using JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = config.GetSection("JWT:ValidIssuers").Get<string[]>()?[0],
            ValidAudience =config.GetSection("JWT:ValidAudiences").Get<string[]>()?[0],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWT:Key"]))
        };

        options.Events = new JwtBearerEvents
        {
            OnChallenge = context =>
            {
                // for 401
                context.HandleResponse();

                throw new UnauthorizedException(
                    $"Authentication failed: {context.Error ?? "invalid_token"}");
            },
            OnForbidden = _ => throw new ForbiddenException("You do not have permission to access this resource.")
        };
    });


builder.Services.AddLogging(loggingBuilder =>
{
    loggingBuilder.AddConsole();
    loggingBuilder.AddDebug();
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});

builder.Services.AddControllers();

// Add services to the container.
builder.Services.AddScoped<IUnitOfWork<AppDbContext>, UnitOfWork<AppDbContext>>();

builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddScoped<TokenHelper>();

// Add Roles
builder.Services.AddAuthorizationBuilder()
    .AddPolicy("RequireAdminRole", policy => policy.RequireRole(RoleEntity.Admin))
    .AddPolicy("RequireUserRole", policy => policy.RequireRole(RoleEntity.User));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlerMiddleware>();

app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

// using (var scope = app.Services.CreateScope())
// {
//     var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
//     dbContext.Database.Migrate(); // Applies any pending migrations
// }

app.Run();