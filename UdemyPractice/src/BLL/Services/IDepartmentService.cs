using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.Request;
using DLL.Model;
using DLL.Repository;
using DLL.UoW;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services
{
    public interface IDepartmentService
    {
        Task<List<Department>> GetAllAsync();
        Task<Department> GetAAsync(string code);
        Task<Department> InsertAsync(DepartmentInsertRequestModel request);
        Task<Department> Update(string code, DepartmentUpdateRequestModel request);
        Task<Department> Delete(string code);
        Task<bool> IsCodeExists(string code);
        Task<bool> IsNameExists(string name);
    }

    public class DepartmentService : IDepartmentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentService(IUnitOfWork unitOfWork, IDepartmentRepository departmentRepository)
        {
            _unitOfWork = unitOfWork;
            _departmentRepository = departmentRepository;
        }
        
        public async Task<List<Department>> GetAllAsync()
        {
            return await _departmentRepository.Queryable(null).ToListAsync();
        }

        public async Task<Department> GetAAsync(string code)
        {
            return await _departmentRepository.FirstOrDefaultAsync(x => x.Code == code);
        }

        public async Task<Department> InsertAsync(DepartmentInsertRequestModel request)
        {
            var department = new Department()
            {
                Code = request.Code,
                Name = request.Name
            };

            await _departmentRepository.CreateAsync(department);
            await _unitOfWork.Commit();

            return department;
        }

        public async Task<Department> Update(string code, DepartmentUpdateRequestModel request)
        {
            var department = await _departmentRepository.FirstOrDefaultAsync(x => x.Code == code);
            var updateDepartment = new Department()
            {
                Name = request.Name
            };
            await _departmentRepository.Update(updateDepartment);
            await _unitOfWork.Commit();
            return department;
        }

        public async Task<Department> Delete(string code)
        {
            var department = await _departmentRepository.FirstOrDefaultAsync(x => x.Code == code);
            await _departmentRepository.Delete(department);
            await _unitOfWork.Commit();
            return department;
        }

        public async Task<bool> IsCodeExists(string code)
        {
            var department = await _departmentRepository.FirstOrDefaultAsync(x => x.Code == code);
            if (department == null)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> IsNameExists(string name)
        {
            var department = await _departmentRepository.FirstOrDefaultAsync(x=>x.Name == name);
            if (department == null)
            {
                return true;
            }

            return false;
        }
    }
}