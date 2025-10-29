using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SharedLibrary.Extensions;

namespace SharedDataSource;

public static class DependencyInjection
{
    public static IServiceCollection AddSharedDataSource(this IServiceCollection services)
    {
        //services.AddDbContext<ApplicationDbContext>(options =>
        //    options.UseSqlServer("")
        //);

        services.AddServicesByAttribute();

        return services;
    }
}
