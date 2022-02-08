using System;
using ToDoList.Models.Interfaces;

namespace ToDoList.Models.Entity
{
    public class ToDoTask : IEntity
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid ProjectId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ToDoTaskType Type { get; set; }
        public ToDoTaskStatus Status { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? ModificationDate { get; set; }
        public bool IsDeleted { get; set; }
    }

    public enum ToDoTaskType
    {
    }

    public enum ToDoTaskStatus
    {
    }
}