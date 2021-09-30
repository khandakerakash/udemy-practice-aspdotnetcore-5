using DLL.Repository;
using DLL.ApplicationDbContext;
using DLL.UoW;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DLL.DllDependency
{
    public static class DllDependency
    {
        public static void AllDllDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            DbContextDependency(services);
            UnitOfWorkDependency(services);
            RepositoryDependency(services);
        }

        private static void DbContextDependency(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(
                options => options.UseSqlServer("name=ConnectionStrings:DefaultConnection"));
        }

        private static void UnitOfWorkDependency(IServiceCollection services)
        {
            services.AddTransient<IUnitOfWork, UnitOfWork>();
        }

        private static void RepositoryDependency(this IServiceCollection services)
        {
            services.AddTransient<IDepartmentRepository, DepartmentRepository>();
            services.AddTransient<IStudentRepository, StudentRepository>();
        }
    }
}