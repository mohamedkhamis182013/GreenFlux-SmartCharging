using GreenFlux_SmartCharging.Domain.Common;
using GreenFlux_SmartCharging.Domain.Common.Exceptions;
using GreenFlux_SmartCharging.Infrastructure.Persistence;
using Microsoft.Extensions.Logging;

namespace GreenFlux_SmartCharging.Infrastructure;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _appDbContext;
    private readonly ILogger<UnitOfWork> _logger;
    public UnitOfWork(AppDbContext databaseContext, ILogger<UnitOfWork> logger)
    {
        _appDbContext = databaseContext;
        _logger = logger;
    }
    public async Task CommitAsync()
    {
        try
        {
            await _appDbContext.SaveChangesAsync();
        }
        catch (Exception e)
        {
            _logger.LogError($"Error while commit changes: {e.InnerException}");
            throw new DomainValidationException($"Error while commit changes: {e.InnerException}");
        }

    }
}

