using FluentValidation;
using FluentValidation.Results;
using GreenFlux_SmartCharging.Application.Dto;

namespace GreenFlux_SmartCharging.api.DtoValidators;
public class GroupDtoValidator : AbstractValidator<GroupDto>
{
    public GroupDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotNull()
            .NotEmpty()
            .NotEqual(" ")
            .WithMessage("Name for group can't be Empty or whitespace");

        RuleFor(x => x.Capacity)
            .GreaterThan(0)
            .WithMessage("Capacity for group must be more than 0");

        RuleFor(x => x.Capacity)
            .GreaterThanOrEqualTo(
                g => g.ChargeStations
                    .SelectMany(x => x.Connectors)
                    .Sum(x => x.MaxCurrent))
            .WithMessage("Capacity must be more than or equal to sum of All max current for connectors");

    }
    public override ValidationResult Validate(ValidationContext<GroupDto> group)
    {
        var groupValidationResult = base.Validate(group);
        var errors = new List<ValidationFailure>();
        errors.AddRange(groupValidationResult.Errors);
        if (group.InstanceToValidate.ChargeStations.Count > 0)
        {
            var chargeStationValidationResult = new List<ValidationResult>();
            foreach (var chargeStation in group.InstanceToValidate.ChargeStations)
            {
                chargeStationValidationResult.Add(new ChargeStationDtoValidators().Validate(chargeStation));
            }
            errors.AddRange(chargeStationValidationResult.SelectMany(x => x.Errors).ToList());

        }
        return new ValidationResult(errors);
    }
}
