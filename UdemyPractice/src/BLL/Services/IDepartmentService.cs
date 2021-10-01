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
        Task<Department> InsertAsync(InsertDepartmentRequestModel reqModel);
        Task<Department> Update(string code, UpdateDepartmentRequestModel reqModel);
        Task<Department> Delete(string code);
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

        public async Task<Department> InsertAsync(InsertDepartmentRequestModel reqModel)
        {
            var department = new Department()
            {
                Code = reqModel.Code,
                Name = reqModel.Name
            };

            await _departmentRepository.CreateAsync(department);
            await _unitOfWork.Commit();

            return department;
        }

        public async Task<Department> Update(string code, UpdateDepartmentRequestModel reqModel)
        {
            var department = await _departmentRepository.FirstOrDefaultAsync(x => x.Code == code);
            var updateDepartment = new Department()
            {
                Name = reqModel.Name
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
    }
}