using Domain.Entities;
using FluentValidation;

namespace Api.Validators
{
    public class RoverTaskValidator : AbstractValidator<RoverTask>
    {
        public RoverTaskValidator()
        {
            RuleFor(x => x.RoverName).NotEmpty();
            RuleFor(x => x.Latitude).InclusiveBetween(-90, 90);
            RuleFor(x => x.Longitude).InclusiveBetween(-180, 180);
            RuleFor(x => x.StartsAt).NotEmpty();
            RuleFor(x => x.DurationMinutes).GreaterThan(0);
            RuleFor(x => x.TaskType).IsInEnum();
            RuleFor(x => x.Status).IsInEnum();
        }
    }
}