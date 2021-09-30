using System;
using System.Threading.Tasks;
using DLL.ApplicationDbContext;

namespace DLL.UoW
{
    public interface IUnitOfWork : IDisposable
    {
        Task<bool> Commit();
    }

    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task<bool> Commit()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}