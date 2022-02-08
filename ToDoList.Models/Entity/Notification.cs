using System;
using ToDoList.Models.Interfaces;

namespace ToDoList.Models.Entity
{
    public class Notification : IEntity
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Message { get; set; }
        public NotificationType Type { get; set; }
        public DateTime? SendDate { get; set; }
        public DateTime? ReadDate { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? ModificationDate { get; set; }
        public bool IsDeleted { get; set; }
    }

    public enum NotificationType
    {
    }
}