using Domain.Entities;
using Domain.Entities.Core;
using Domain.Interfaces.Common;

namespace Domain.Interfaces;
public interface IRoleRepository : IRepository<Role>
{
    IQueryable<Role> QueryInclude();
}
