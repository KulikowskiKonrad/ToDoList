using ToDoList.BL.LogicInterfaces;
using ToDoList.BL.RepositoryInterfaces;
using ToDoList.Models.Entity;

namespace ToDoList.BL.Logic
{
    public class SharedProjectLogic : ISharedProjectLogic
    {
        private readonly IRepository<SharedProject> _sharedProjectRepository;

        public SharedProjectLogic(IRepository<SharedProject> sharedProjectRepository)
        {
            _sharedProjectRepository = sharedProjectRepository;
        }
    }
}