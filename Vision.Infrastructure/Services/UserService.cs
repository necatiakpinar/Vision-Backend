using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using Vision.Application.Services;
using Vision.Domain.Identity;
using Vision.Infrastructure.DBContexts;

namespace Vision.Infrastructure.Services;

public class UserService : IUserService
{
    private readonly VisionDbContext _dbContext;
    public UserService(VisionDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public IEnumerable<User> GetAll()
    {
        return _dbContext.Users.OrderByDescending(c => c.Id).Take(20).AsNoTracking().AsEnumerable();
    }
    public User? GetById(string id)
    {
        return _dbContext.Users.FirstOrDefault(c => c.Id == id);
    }
    public async Task Add(User newEntity)
    {
        await _dbContext.Users.AddAsync(newEntity);

        _dbContext.ChangeTracker.DetectChanges();
        Console.WriteLine(_dbContext.ChangeTracker.DebugView.LongView);
        await _dbContext.SaveChangesAsync();
    }
    public async Task Update(User updatedEntity)
    {
        var entity = _dbContext.Users.FirstOrDefault(c => c.Id == updatedEntity.Id);

        if (entity != null)
        {
            entity = updatedEntity;

            _dbContext.Users.Update(entity);
            _dbContext.ChangeTracker.DetectChanges();
            Console.WriteLine(_dbContext.ChangeTracker.DebugView.LongView);

            await _dbContext.SaveChangesAsync();
        }
        else
        {
            throw new Exception("Entity not found");
        }
    }
    public async Task Delete(User entityToDelete)
    {
        var entity = _dbContext.Users.FirstOrDefault(c => c.Id == entityToDelete.Id);

        if (entity != null)
        {
            _dbContext.Users.Remove(entity);
            _dbContext.ChangeTracker.DetectChanges();
            Console.WriteLine(_dbContext.ChangeTracker.DebugView.LongView);
            await _dbContext.SaveChangesAsync();
        }
        else
        {
            throw new Exception("Entity not found");
        }
    }
}