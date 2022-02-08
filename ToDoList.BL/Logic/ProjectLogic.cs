using ToDoList.BL.LogicInterfaces;
using ToDoList.BL.RepositoryInterfaces;
using ToDoList.Models.Entity;

namespace ToDoList.BL.Logic
{
    public class ProjectLogic : IProjectLogic
    {
        private readonly IRepository<Project> _projectRepository;

        public ProjectLogic(IRepository<Project> projectRepository)
        {
            _projectRepository = projectRepository;
        }
    }
}