using FluentValidation;
using FluentValidation.Results;
using GreenFlux_SmartCharging.Application.Dto;

namespace GreenFlux_SmartCharging.api.DtoValidators
{
    public class ChargeStationDtoValidators : AbstractValidator<ChargeStationDto>
    {
        public ChargeStationDtoValidators()
        {
            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty()
                .NotEqual(" ")
                .WithMessage("Name for chargeStation can't be Empty or whitespace");

            RuleFor(x => x.Connectors.Count)
                .GreaterThan(0)
                .LessThan(6)
                .WithMessage("connectors for chargeStation must be between 1 and 5");
        }
        public override ValidationResult Validate(ValidationContext<ChargeStationDto> chargeStation)
        {
            var chargeStationValidationResult = base.Validate(chargeStation);
            var connectorsValidationResult = new List<ValidationResult>();
            foreach (var connectorDto in chargeStation.InstanceToValidate.Connectors)
            {
                connectorsValidationResult.Add(new ConnectorDtoValidator().Validate(connectorDto));
            }
            var errors = new List<ValidationFailure>();
            errors.AddRange(chargeStationValidationResult.Errors);
            errors.AddRange(connectorsValidationResult.SelectMany(x => x.Errors).ToList());
            return new ValidationResult(errors);
        }
    }
}
