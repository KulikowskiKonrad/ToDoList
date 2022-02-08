using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoList.Models.Entity;
using ToDoList.Models.Model.User;

namespace ToDoList.BL.LogicInterfaces
{
    public interface IUserLogic
    {
        User Authenticate(string email, string password);
        void Delete(Guid id);
        void Update(Guid id, UpdateModel model);
        Task<UserModel> GetByIdAsync(Guid userId);
        Task<User> Create(RegisterUserModel model);
        List<User> GetAll();
        Guid GetCurrentUserId();
    }
}