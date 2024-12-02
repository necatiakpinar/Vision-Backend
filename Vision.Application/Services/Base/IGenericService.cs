using MongoDB.Bson;

namespace Vision.Application.Services.Base
{
    public interface IGenericService<T> where T : class
    {
        IEnumerable<T> GetAll();
        T? GetById(string id);
        Task Add(T newEntity);
        Task Update(T updatedEntity);
        Task Delete(T entityToDelete);
    }
}