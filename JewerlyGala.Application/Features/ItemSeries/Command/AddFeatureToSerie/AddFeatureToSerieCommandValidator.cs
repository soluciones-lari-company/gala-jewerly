using FluentValidation;

namespace JewerlyGala.Application.Features.ItemSeries.Command.AddFeatureToSerie
{
    public class AddFeatureToSerieCommandValidator : AbstractValidator<AddFeatureToSerieCommand>
    {
        public AddFeatureToSerieCommandValidator()
        {
            RuleFor(c => c.SerieId)
                .NotEmpty();

            RuleFor(c => c.FeatureName)
                .NotEmpty();

            RuleFor(c => c.Value)
                .NotEmpty();
        }
    }
}
