using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Vision.Application.Repositories.Leaderboard;
using Vision.Application.Repositories.Mentor;
using Vision.Application.Repositories.OwnedFiles;
using Vision.Application.Repositories.ProfileImage;
using Vision.Application.Repositories.Role;
using Vision.Application.Repositories.SharedFiles;
using Vision.Application.Repositories.Subscriber;
using Vision.Application.Repositories.User;
using Vision.Application.Repositories.UserMedia;
using Vision.Application.Repositories.UserProfile;
using Vision.Application.Services;
using Vision.Domain.Configurations;
using Vision.Domain.Identity;
using Vision.Infrastructure.Authentication;
using Vision.Infrastructure.DBContexts;
using Vision.Infrastructure.Repositories.Leaderboard;
using Vision.Infrastructure.Repositories.Mentor;
using Vision.Infrastructure.Repositories.OwnedFiles;
using Vision.Infrastructure.Repositories.ProfileImage;
using Vision.Infrastructure.Repositories.Role;
using Vision.Infrastructure.Repositories.SharedFiles;
using Vision.Infrastructure.Repositories.Subscriber;
using Vision.Infrastructure.Repositories.User;
using Vision.Infrastructure.Repositories.UserMedia;
using Vision.Infrastructure.Repositories.UserProfile;
using Vision.Infrastructure.Services;
using Vision.Infrastructure.Settings;
using Vision.Infrastructure.Stores;

namespace Vision.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddAuth(configuration);

        services.ConfigureApplicationCookie(options =>
        {
            options.LoginPath = "/";
            options.AccessDeniedPath = "/";
        });

        services.Configure<SmtpSettings>(configuration.GetSection(SmtpSettings.SectionName));

        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        services.AddScoped<IEmailService, EmailService>();

        services.Configure<MongoDbConfig>(configuration.GetSection("MongoDB"));
        services.AddScoped<IMongoDbService, MongoDbService>();

        var mongoDbSettings = configuration.GetSection("MongoDB").Get<MongoDbConfig>();
        services.AddDbContext<VisionDbContext>(options =>
            options.UseMongoDB(mongoDbSettings?.ConnectionString ?? "", mongoDbSettings?.DatabaseName ?? ""));

        var redisConnectionString = configuration["Redis:Configuration"];
        services.AddScoped<IRedisService, RedisService>(_ => new RedisService(redisConnectionString));

        AddRepositories(services);

        services.AddScoped<IGridFsService, GridFsService>();
        services.AddIdentity<User, AppRole>(options =>
            {
                options.Password.RequiredLength = 3;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
            })
            .AddUserStore<UserStore<User>>()
            .AddRoleStore<RoleStore<AppRole>>()
            .AddDefaultTokenProviders();
        return services;
    }
    private static void AddRepositories(IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();

        services.AddScoped<IUserReadRepository, UserReadRepository>();
        services.AddScoped<IUserWriteRepository, UserWriteRepository>();

        services.AddScoped<IRoleReadRepository, RoleReadRepository>();
        services.AddScoped<IRoleWriteRepository, RoleWriteRepository>();

        services.AddScoped<IUserProfileReadRepository, UserProfileReadRepository>();
        services.AddScoped<IUserProfileWriteRepository, UserProfileWriteRepository>();

        services.AddScoped<ILeaderboardReadRepository, LeaderboardReadRepository>();
        services.AddScoped<ILeaderboardWriteRepository, LeaderboardWriteRepository>();

        services.AddScoped<IProfileImageReadRepository, ProfileImageReadRepository>();
        services.AddScoped<IProfileImageWriteRepository, ProfileImageWriteRepository>();

        services.AddScoped<IUserMediaReadRepository, UserMediaReadRepository>();
        services.AddScoped<IUserMediaWriteRepository, UserMediaWriteRepository>();
        
        services.AddScoped<ISharedFilesReadRepository, SharedFilesReadRepository>();
        services.AddScoped<ISharedFilesWriteRepository, SharedFilesWriteRepository>();
        
        services.AddScoped<IOwnedFilesReadRepository, OwnedFilesReadRepository>();
        services.AddScoped<IOwnedFilesWriteRepository, OwnedFilesWriteRepository>();

        services.AddScoped<IMentorReadRepository, MentorReadRepository>();
        services.AddScoped<IMentorWriteRepository, MentorWriteRepository>();
        
        services.AddScoped<ISubscriberReadRepository, SubscribeReadRepository>();
        services.AddScoped<ISubscriberWriteRepository, SubscribeWriteRepository>();
        
    }

    private static IServiceCollection AddAuth(this IServiceCollection services, ConfigurationManager configuration)
    {
        var jwtSettings = new JwtSettings();
        configuration.Bind(JwtSettings.SectionName, jwtSettings);

        services.AddSingleton(Options.Create(jwtSettings));
        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();

        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme; // Varsayılanı JWT yapıyoruz
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret)),
                };

                options.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        Console.WriteLine("Token doğrulama hatası: " + context.Exception.Message);
                        return Task.CompletedTask;
                    },
                    OnChallenge = context =>
                    {
                        // 401 Unauthorized döndürüyoruz, login yönlendirmesi yapmıyoruz.
                        context.HandleResponse();
                        context.Response.StatusCode = 401;
                        context.Response.ContentType = "application/json";
                        var result = System.Text.Json.JsonSerializer.Serialize(new { error = "You are not authorized" });
                        return context.Response.WriteAsync(result);
                    }
                };
            });


        return services;
    }
}