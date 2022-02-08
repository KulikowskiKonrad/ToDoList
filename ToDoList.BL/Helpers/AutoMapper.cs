using AutoMapper;
using ToDoList.Models.Entity;
using ToDoList.Models.Model.User;

namespace ToDoList.BL.Helpers
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {
            CreateMap<User, UserModel>();
            CreateMap<RegisterUserModel, User>();
            CreateMap<UpdateModel, User>();
        }
    }
}
