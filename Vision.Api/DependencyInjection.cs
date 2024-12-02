using Vision.Api.Common.Mapping;

namespace Vision.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddRazorPages();  
        services.AddControllersWithViews();
        services.AddControllers();
        services.AddMappings();
        return services;
    }   
}