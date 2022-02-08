using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoList.Models.Entity;

namespace ToDoList.BL.RepositoryInterfaces
{
    public interface IUserRepository : IRepository<User>
    {
        List<User> GetAllActiveUsers();
        User GetByEmail(string email);
        Task<bool> IsEmailExistAsync(string email);
    }
}