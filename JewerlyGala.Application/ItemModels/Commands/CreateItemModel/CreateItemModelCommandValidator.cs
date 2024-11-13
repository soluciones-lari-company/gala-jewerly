using FluentValidation;

namespace JewerlyGala.Application.ItemModels.Commands.CreateItemModel
{
    public class CreateItemModelCommandValidator : AbstractValidator<CreateItemModelCommand>
    {
        public CreateItemModelCommandValidator()
        {
            RuleFor(v => v.Name)
                .MaximumLength(50)
                .NotEmpty();
        }
    }
}
