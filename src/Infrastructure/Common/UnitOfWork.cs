using Domain.Interfaces;
using Domain.Interfaces.Common;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore.Storage;

namespace Infrastructure.Common;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    private IDbContextTransaction _objTran;
    private Dictionary<Type, object> repositories;
    //===============
    // public IAuthLocalRepository authLocalRepo { get; }
    // public IAuthProviderRepository authProviderRepo { get; }
    // public IAuthSessionRepository authSessionRepo { get; }
    // public IPermissionRepository permissionRepo { get; }
    public IRoleRepository roleRepo { get; }
    // public IRolePermissionRepository rolePermissionRepo { get; }
    // public IRoleUserRepository roleUserRepo { get; }
    // public IUserRepository userRepo { get; }
    // public IUserNotificationRepository userNotificationRepo { get; }
    //===============

    public UnitOfWork(AppDbContext context)
    {
        _context = context;

        roleRepo = new RoleRepository(_context);
    }

    public async ValueTask DisposeAsync()
    {
        await _context.DisposeAsync();
    }

    public int SaveChanges()
    {
        return _context.SaveChanges();
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void BeginTransaction()
    {
        _objTran = _context.Database.BeginTransaction();
    }

    public void Commit()
    {
        _objTran.Commit();
    }

    public async Task CommitAsync()
    {
        await _objTran.CommitAsync();
    }

    public void Rollback()
    {
        _objTran.Rollback();
        _objTran.Dispose();
    }

    public async Task RollbackAsync()
    {
        await _objTran.RollbackAsync();
        await _objTran.DisposeAsync();
    }

    public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class
    {
        if (repositories == null)
        {
            repositories = new Dictionary<Type, object>();
        }

        var type = typeof(TEntity);
        if (!repositories.ContainsKey(type))
        {
            repositories[type] = new Repository<TEntity>(_context);
        }

        return (IRepository<TEntity>)repositories[type];
    }

}
