using FluentValidation;

namespace JewerlyGala.Application.Features.ItemSeries.Command.RemoveFeatureToSerie
{
    public class RemoveFeatureToSerieCommandValidator : AbstractValidator<RemoveFeatureToSerieCommand>
    {
        public RemoveFeatureToSerieCommandValidator()
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
