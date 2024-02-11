
namespace GreenFlux_SmartCharging.Domain.Common.Exceptions;
public class DomainValidationException : Exception
{
    public DomainValidationException()
    {
    }

    public DomainValidationException(string message) : base(message)
    {
    }
}

