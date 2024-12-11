using cndcAPI.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Text;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Configurar Serilog
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Warning() // Solo registra Warning y niveles superiores
    .WriteTo.Console()
    .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day)
    .Enrich.FromLogContext()
    .CreateLogger();

builder.Host.UseSerilog();

try
{
    Log.Information("Iniciando aplicación...");

    // Configurar servicios
    ConfigureServices(builder);

    var app = builder.Build();

    // Configurar el pipeline de la aplicación
    ConfigureMiddleware(app);

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "La aplicación terminó de forma inesperada.");
}
finally
{
    Log.CloseAndFlush();
}

// Método para configurar los servicios
void ConfigureServices(WebApplicationBuilder builder)
{
    // CORS
    builder.Services.AddCors(options =>
    {
        options.AddPolicy("DefaultPolicy", policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
    });

    // Agregar controladores y servicios
    builder.Services.AddControllers();
    builder.Services.AddHttpContextAccessor();
    builder.Services.AddScoped<IUserService, UserService>();

    // Configurar Swagger
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(options =>
    {
        options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
        {
            In = ParameterLocation.Header,
            Name = "Authorization",
            Type = SecuritySchemeType.ApiKey
        });

        options.OperationFilter<SecurityRequirementsOperationFilter>();
    });

    // Configurar autenticación JWT
    var tokenKey = builder.Configuration.GetSection("AppSettings:Token").Value;
    if (string.IsNullOrEmpty(tokenKey))
    {
        throw new Exception("La clave JWT no está configurada en AppSettings:Token.");
    }

    builder.Services.AddAuthentication().AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            ValidateAudience = false,
            ValidateIssuer = false,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey))
        };
    });
}

// Método para configurar middleware
void ConfigureMiddleware(WebApplication app)
{

    app.UseSwagger();
    app.UseSwaggerUI(); 
     
  /*  if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }
    else
    {
        // Habilitar Swagger solo en una ruta específica
        var swaggerToken = app.Configuration.GetValue<string>("Swagger:Token");

        app.Map("/hidden-swagger", builder =>
        {
            builder.Use(async (context, next) =>
            {
                // Verificar token de acceso en la URL
                if (context.Request.Query["token"] != swaggerToken)
                {
                    context.Response.StatusCode = 403; // Prohibido
                    await context.Response.WriteAsync("Acceso no autorizado.");
                    return;
                }

                await next();
            });

            builder.UseSwagger();
            builder.UseSwaggerUI();
        });
    }*/
    
    app.UseHttpsRedirection();
    app.UseRouting();
    app.UseCors("DefaultPolicy");
    app.UseAuthentication();
    app.UseAuthorization();
    app.MapControllers();
}