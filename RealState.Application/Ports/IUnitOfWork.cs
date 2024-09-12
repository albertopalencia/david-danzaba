namespace RealState.Application.Ports
{
    public interface IUnitOfWork
    {
        Task SaveAsync(CancellationToken? cancellationToken = null);
    }
}
