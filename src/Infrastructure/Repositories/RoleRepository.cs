using Domain.Entities.Core;
using Domain.Interfaces;
using Infrastructure.Common;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class RoleRepository : Repository<Role>, IRoleRepository
{
    public RoleRepository(AppDbContext context) : base(context)
    { }

    public IQueryable<Role> QueryInclude()
    {
        return _dbSet
            .Include(x => x.Users)
                .ThenInclude(x => x.User)
            .Include(x => x.Permissions)
                .ThenInclude(x => x.Permission);
    }
}