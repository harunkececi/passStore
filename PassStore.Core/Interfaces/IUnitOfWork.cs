using PassStore.Core.Entities;
using PassStore.Core.Interfaces;

namespace PassStore.Core.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IRepository<Password> PasswordRepository { get; }
    IRepository<User> UserRepository { get; }
    Task<int> SaveChangesAsync();
    Task BeginTransactionAsync();
    Task CommitTransactionAsync();
    Task RollbackTransactionAsync();
}
