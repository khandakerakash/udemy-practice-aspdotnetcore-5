using DLL.ApplicationDbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DLL.DllDependency
{
    public static class DllDependency
    {
        public static void AllDllDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(
                options => options.UseSqlServer("name=ConnectionStrings:DefaultConnection"));
        }
    }
}