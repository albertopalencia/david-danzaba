using RealState.Application.Ports;
using RealState.Infrastructure.DataSource;
using Microsoft.EntityFrameworkCore;

namespace RealState.Infrastructure.Adapters;

public class UnitOfWork(DataContext context) : IUnitOfWork
{
    public async Task SaveAsync(CancellationToken? cancellationToken = null)
    {
        var token = cancellationToken ?? new CancellationTokenSource().Token;

        await context.SaveChangesAsync(token);
    }
}
