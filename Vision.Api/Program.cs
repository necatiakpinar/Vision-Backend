using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Sinks.Elasticsearch;
using Vision.Api;
using Vision.Api.Middlewares;
using Vision.Application;
using Vision.Domain.Identity;
using Vision.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
{
    var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
    // Log.Logger = new LoggerConfiguration()
    //     .Enrich.FromLogContext()
    //     .WriteTo.Console()
    //     .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri("")) 
    //     {
    //         AutoRegisterTemplate = true,
    //         IndexFormat = $"vision-logs-{environment?.ToLower().Replace(".", "-")}-{DateTime.UtcNow:yyyy-MM}"
    //     })
    //     .CreateLogger();

    builder.Host.UseSerilog();

    builder.Services
        .AddPresentation()
        .AddApplication()
        .AddInfrastructure(builder.Configuration)
        .AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Vision Backend API", Version = "v1" });
            
            c.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "JWT Authorization header using the Bearer scheme. Example: 'Bearer {token}'",
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = JwtBearerDefaults.AuthenticationScheme
                        }
                    },
                    new string[] {}
                }
            });
        });
}

var app = builder.Build();
{
    app.UseStaticFiles();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Vision Backend API V1");
        c.InjectStylesheet("/swagger-ui/SwaggerDark.css");
        c.RoutePrefix = String.Empty;
    });
    
    app.UseMiddleware<RequestResponseLoggingMiddleware>();
    
    app.UseHttpsRedirection();
    app.UseRouting();
    app.UseAuthentication();
    app.UseAuthorization();
    app.MapControllers();
    app.MapRazorPages();
    app.MapDefaultControllerRoute();

    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        var roleManager = services.GetRequiredService<RoleManager<AppRole>>();

        var roles = new List<string> { "Admin", "Manager", "Member" };
        foreach (var role in roles)
        {
            var roleExists = await roleManager.RoleExistsAsync(role);
            if (!roleExists)
            {
                var appRole = new AppRole
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = role,
                    NormalizedName = role.ToUpperInvariant()
                };
                await roleManager.CreateAsync(appRole);
            }
        }
    }

    app.Run();
}
