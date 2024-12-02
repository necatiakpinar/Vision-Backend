using Microsoft.EntityFrameworkCore;
using Vision.Domain.Common;

namespace Vision.Application.Repositories;

public interface IRepository<T> where T : class, IEntity
{
    DbSet<T> Table { get; }
}