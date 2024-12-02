using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using MongoDB.Bson;
using Vision.Application.Repositories;
using Vision.Domain.Common;
using Vision.Infrastructure.DBContexts;

namespace Vision.Infrastructure.Repositories;


public class WriteRepository<T> : IWriteRepository<T> where T : class, IEntity
{
    protected readonly VisionDbContext context;

    public WriteRepository(VisionDbContext context)
    {
        this.context = context;
    }

    public DbSet<T> Table => context.Set<T>();

    public async Task<bool> AddAsync(T model, CancellationToken? cancellationToken = default)
    {
        EntityEntry<T> entityEntry = await Table.AddAsync(model, cancellationToken ?? CancellationToken.None);
        return entityEntry.State == EntityState.Added;
    }

    public async Task<bool> AddRangeAsync(List<T> datas , CancellationToken? cancellationToken = default)
    {
        await Table.AddRangeAsync(datas, cancellationToken ?? CancellationToken.None);
        return true;
    }

    public bool Remove(T model)
    {
        EntityEntry<T> entityEntry = Table.Remove(model);
        return entityEntry.State == EntityState.Deleted;
    }

    public bool RemoveRange(List<T> datas)
    {
        Table.RemoveRange(datas);
        return true;
    }

    public async Task<bool> RemoveAsync(string id, CancellationToken? cancellationToken = default)
    {
        T model = await Table.FirstOrDefaultAsync(data => data.Id == id, cancellationToken ?? CancellationToken.None);
        return Remove(model);
    }

    public bool Update(T model)
    {
        EntityEntry entitiyEntry = Table.Update(model);
        return entitiyEntry.State == EntityState.Modified;
    }

    public async Task<int> SaveChangesAsync()
        => await context.SaveChangesAsync();
}