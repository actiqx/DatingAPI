using System.Threading.Tasks;
using System.Collections.Generic;
using Datingapp.API.Models;

namespace Datingapp.API.Data
{
    public interface IDatingRepository
    {
        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;

        Task<bool> SaveAll();
        Task<IEnumerable<User>> GetUsers();

        Task<User> getUser(int id);
    }
}