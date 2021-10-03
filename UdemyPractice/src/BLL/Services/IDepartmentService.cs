using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.Request;
using DLL.Model;
using DLL.Repository;
using DLL.UoW;
using Microsoft.EntityFrameworkCore;
using Utils.AppExceptions;

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
            var department = await _departmentRepository.FirstOrDefaultAsync(x => x.Code == code);

            if (department == null)
            {
                throw new ApplicationValidationException("The department was not found.");
            }

            return department;
        }

        public async Task<Department> InsertAsync(DepartmentInsertRequestModel request)
        {
            var department = new Department()
            {
                Code = request.Code,
                Name = request.Name
            };

            await _departmentRepository.CreateAsync(department);

            if (await _unitOfWork.Commit())
            {
                throw new ApplicationValidationException("Something went wrong. Please try again later.");
            }

            return department;
        }

        public async Task<Department> Update(string code, DepartmentUpdateRequestModel request)
        {
            var department = await _departmentRepository.FirstOrDefaultAsync(x => x.Code == code);
            if (department == null)
            {
                throw new ApplicationValidationException("The department was not found.");
            }
            
            if (!string.IsNullOrWhiteSpace(request.Code))
            {
                var existsAlreadyCode = await _departmentRepository.FirstOrDefaultAsync(x => x.Code == request.Code);
                if (existsAlreadyCode != null)
                {
                    throw new ApplicationValidationException("This code already exists in our system.");
                }

                department.Code = request.Code;
            }
            
            if (!string.IsNullOrWhiteSpace(request.Name))
            {
                var existsAlreadyCode = await _departmentRepository.FirstOrDefaultAsync(x => x.Name == request.Name);
                if (existsAlreadyCode != null)
                {
                    throw new ApplicationValidationException("This code already exists in our system.");
                }

                department.Name = request.Name;
            }

            await _departmentRepository.Update(department);
            if (await _unitOfWork.Commit())
            {
                return department;
            }
            throw new ApplicationValidationException("Something went wrong. Please try again later.");
        }

        public async Task<Department> Delete(string code)
        {
            var department = await _departmentRepository.FirstOrDefaultAsync(x => x.Code == code);
            if (department == null)
            {
                throw new ApplicationValidationException("The department was not found.");
            }
            await _departmentRepository.Delete(department);
            if (await _unitOfWork.Commit())
            {
                return department;
            }
            throw new ApplicationValidationException("Something went wrong. Please try again later.");
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