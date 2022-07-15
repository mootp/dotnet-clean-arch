namespace Domain.Interfaces.Common;

public interface IUnitOfWork : IAsyncDisposable
{
    int SaveChanges();
    Task<int> SaveChangesAsync();
    void BeginTransaction();
    void Commit();
    Task CommitAsync();
    void Rollback();
    Task RollbackAsync();
    IRepository<TEntity> GetRepository<TEntity>() where TEntity : class;
    //===============
    // IAuthLocalRepository authLocalRepo { get; }
    // IAuthProviderRepository authProviderRepo { get; }
    // IAuthSessionRepository authSessionRepo { get; }
    // IPermissionRepository permissionRepo { get; }
    IRoleRepository roleRepo { get; }
    // IRolePermissionRepository rolePermissionRepo { get; }
    // IRoleUserRepository roleUserRepo { get; }
    // IUserRepository userRepo { get; }
    // IUserNotificationRepository userNotificationRepo { get; }
}
