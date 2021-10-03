using System;
using System.Threading;
using System.Threading.Tasks;
using BLL.Services;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace BLL.Request
{
    public class DepartmentInsertRequestModel
    {
        public string Code { get; set; }
        public string Name { get; set; }
    }

    public class DepartmentInsertRequestModelValidator : AbstractValidator<DepartmentInsertRequestModel>
    {
        private readonly IServiceProvider _serviceProvider;

        public DepartmentInsertRequestModelValidator(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            RuleFor(x => x.Code).NotEmpty().NotNull().MinimumLength(2).MaximumLength(10).MustAsync(CodeExists)
                .WithMessage("This code already exists in our system.");
            RuleFor(x => x.Name).NotEmpty().NotNull().MinimumLength(2).MaximumLength(255).MustAsync(NameExists)
                .WithMessage("This name already exists in our system.");
        }
        
        private async Task<bool> CodeExists(string code, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(code))
            {
                return true;
            }

            var requiredService = _serviceProvider.GetRequiredService<IDepartmentService>();

            return await requiredService.IsCodeExists(code);
        }
        
        private async Task<bool> NameExists(string name, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(name))
            {
                return true;
            }

            var requiredService = _serviceProvider.GetRequiredService<IDepartmentService>();

            return await requiredService.IsNameExists(name);
        }
    }
}