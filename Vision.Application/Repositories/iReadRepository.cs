using System.Linq.Expressions;
using Vision.Domain.Common;

namespace Vision.Application.Repositories;

public interface IReadRepository<T> : IRepository<T> where T : class, IEntity
{
    IQueryable<T> GetAll(bool tracking = true);
    IQueryable<T> GetWhere(Expression<Func<T, bool>> method, bool tracking = true);
    Task<T> GetSingleAsync(Expression<Func<T, bool>> method, bool tracking = true, CancellationToken? cancellationToken = default);
    Task<T> GetByIdAsync(string id, bool tracking = true, CancellationToken? cancellationToken = default);
}