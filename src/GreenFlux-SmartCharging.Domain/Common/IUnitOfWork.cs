namespace GreenFlux_SmartCharging.Domain.Common;
public interface IUnitOfWork
{
    Task CommitAsync();
}

