using FluentValidation;
using GreenFlux_SmartCharging.Application.Dto;

namespace GreenFlux_SmartCharging.api.DtoValidators;
public class ConnectorDtoValidator : AbstractValidator<ConnectorDto>
{
    public ConnectorDtoValidator()
    {
        RuleFor(a => a.MaxCurrent)
            .GreaterThan(0)
            .WithMessage("maxCurrent for connector must be more than 0");
    }
}

