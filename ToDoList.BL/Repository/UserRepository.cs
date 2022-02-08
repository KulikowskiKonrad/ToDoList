using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ToDoList.BL.RepositoryInterfaces;
using ToDoList.Models;
using ToDoList.Models.Entity;

namespace ToDoList.BL.Repository
{
    public class UserRepository : RepositoryBaseGeneric<User>,
        IUserRepository
    {
        public UserRepository(ToDoListContext dbContext) : base(dbContext)
        {
        }

        public User GetByEmail(string email)
        {
            return DbContext.Users.SingleOrDefault(x => x.Email == email && x.IsDeleted == false);
        }

        public List<User> GetAllActiveUsers()
        {
            //TODO
            // return _dbContext.Users.Where(x => x.)
            return null;
        }

        public async Task<bool> IsEmailExistAsync(string email)
        {
            return await DbContext.Users.Where(x => x.Email == email).AnyAsync();
        }
    }
}