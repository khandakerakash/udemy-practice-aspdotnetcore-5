using BLL.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BLL.BllDependency
{
    public static class BllDependency
    {
        public static void AllBllDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            ServiceDependency(services);
            ValidationDependency(services);
        }
        private static void ServiceDependency(IServiceCollection services)
        {
            services.AddTransient<IDepartmentService, DepartmentService>();
        }
        private static void ValidationDependency(IServiceCollection services)
        {
            //throw new System.NotImplementedException();
        }
    }
}