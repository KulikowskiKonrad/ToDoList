using ToDoList.BL.LogicInterfaces;
using ToDoList.BL.RepositoryInterfaces;
using ToDoList.Models.Entity;

namespace ToDoList.BL.Logic
{
    public class NotificationLogic : INotificationLogic
    {
        private readonly IRepository<Notification> _notificationRepository;

        public NotificationLogic(IRepository<Notification> notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }
    }
}