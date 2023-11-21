namespace Accouting.Data.Interfaces
{
    public interface  IUnitOfWork<T> where T : class
    {
        IGRepository<T> Entity { get; }
        Task SaveAsync();
    }
}
