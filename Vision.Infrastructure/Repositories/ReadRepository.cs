using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using Vision.Application.Repositories;
using Vision.Domain.Common;
using Vision.Infrastructure.DBContexts;

namespace Vision.Infrastructure.Repositories;

public class ReadRepository<T> : IReadRepository<T> where T : class, IEntity
{
    protected readonly VisionDbContext context;
    public DbSet<T> Table => context.Set<T>();

    public ReadRepository(VisionDbContext context)
    {
        this.context = context;
    }


    public IQueryable<T> GetAll(bool tracking = true)
    {
        var query = Table.AsQueryable();
        if (!tracking)
            query = query.AsNoTracking();

        return query;
    }

    public IQueryable<T> GetWhere(Expression<Func<T, bool>> method, bool tracking = true)
    {
        var query = Table.Where(method);
        if (!tracking)
            query = query.AsNoTracking();

        return query;
    }

    public async Task<T> GetSingleAsync(Expression<Func<T, bool>> method, bool tracking = true, CancellationToken? cancellationToken = default)
    {
        var query = Table.AsQueryable();
        if (!tracking)
            query = Table.AsNoTracking();

        return await query.FirstOrDefaultAsync(method, cancellationToken ?? CancellationToken.None);
    }

    public async Task<T> GetByIdAsync(string id, bool tracking = true, CancellationToken? cancellationToken = default)
    {
        var query = Table.AsQueryable();
        if (!tracking)
            query = Table.AsNoTracking();

        return await query.FirstOrDefaultAsync(data => data.Id == id, cancellationToken ?? CancellationToken.None);
    }
}