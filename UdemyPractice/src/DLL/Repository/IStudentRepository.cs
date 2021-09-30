using DLL.ApplicationDbContext;
using DLL.Model;

namespace DLL.Repository
{
    public interface IStudentRepository : IRepositoryBase<Student>
    {
        
    }

    public class StudentRepository : RepositoryBase<Student>, IStudentRepository
    {
        public StudentRepository(AppDbContext context) : base(context)
        {
        }
    }
}