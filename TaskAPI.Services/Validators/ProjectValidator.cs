using FluentValidation;
using TaskAPI.DataAccess.Entity;

namespace TaskAPI.Services.Validators
{
    public class ProjectValidator : AbstractValidator<ProjectEntity>
    {
        public ProjectValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Nome deve ser preenchido");
        }
    }
}
