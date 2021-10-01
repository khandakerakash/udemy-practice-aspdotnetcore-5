using FluentValidation;

namespace BLL.Request
{
    public class InsertDepartmentRequestModel
    {
        public string Code { get; set; }
        public string Name { get; set; }
    }

    public class InsertDepartmentRequestModelValidator : AbstractValidator<InsertDepartmentRequestModel>
    {
        public InsertDepartmentRequestModelValidator()
        {
            RuleFor(x => x.Code).NotEmpty().NotNull();
            RuleFor(x => x.Name).NotEmpty().NotNull();
        }
    }
}