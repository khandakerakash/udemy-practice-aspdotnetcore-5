using BLL.Request;
using BLL.Services;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BLL.BllDependency
{
    public static class BllDependency
    {
        public static void AllBllDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            ServiceDependency(services);
            FluentValidationDependency(services);
        }
        private static void ServiceDependency(IServiceCollection services)
        {
            services.AddTransient<IDepartmentService, DepartmentService>();
        }
        private static void FluentValidationDependency(IServiceCollection services)
        {
            services.AddTransient<IValidator<InsertDepartmentRequestModel>, InsertDepartmentRequestModelValidator>();
        }
    }
}