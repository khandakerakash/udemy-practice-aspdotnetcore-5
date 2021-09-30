using DLL.ApplicationDbContext;
using DLL.Model;

namespace DLL.Repository
{
    public interface IDepartmentRepository : IRepositoryBase<Department>
    {
        
    }

    public class DepartmentRepository : RepositoryBase<Department>, IDepartmentRepository
    {
        public DepartmentRepository(AppDbContext context) : base(context)
        {
        }
    }
}