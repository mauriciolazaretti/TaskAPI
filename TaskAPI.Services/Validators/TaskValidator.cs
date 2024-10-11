using FluentValidation;
using TaskAPI.DataAccess.Entity;

namespace TaskAPI.Services.Validators
{
    public class TaskValidator : AbstractValidator<TaskEntity>
    {
        public TaskValidator()
        {
            RuleFor(x => x.Priority).NotEmpty().WithMessage("Prioridade deve ser preenchido");
            RuleFor(x => x.Title).NotEmpty().WithMessage("Titulo deve ser preenchido");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Descrição deve ser preenchido");
            RuleFor(x => x.Status).NotEmpty().WithMessage("Status deve ser preenchido");
            RuleFor(x => x.ExpireDate).NotEmpty().WithMessage("Data Expiração deve ser preenchido");
            RuleFor(x => x.ProjectId).NotEmpty().WithMessage("Id Project deve ser preenchido");
            RuleFor(x => x).Must(x =>
            {
                return (x.Id > 0 && x.User != string.Empty ) || x.Id == 0;
            }).WithName("User").WithMessage("User deve ser preenchido");
        }
    }
}
