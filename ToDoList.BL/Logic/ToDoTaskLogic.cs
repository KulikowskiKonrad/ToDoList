using ToDoList.BL.LogicInterfaces;
using ToDoList.BL.RepositoryInterfaces;
using ToDoList.Models.Entity;

namespace ToDoList.BL.Logic
{
    public class ToDoTaskLogic : IToDoTaskLogic
    {
        private readonly IRepository<ToDoTask> _toDoTaskRepository;

        public ToDoTaskLogic(IRepository<ToDoTask> toDoTaskRepository)
        {
            _toDoTaskRepository = toDoTaskRepository;
        }
    }
}