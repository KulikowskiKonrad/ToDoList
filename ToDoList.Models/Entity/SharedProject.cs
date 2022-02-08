using System;
using ToDoList.Models.Interfaces;

namespace ToDoList.Models.Entity
{
    public class SharedProject : IEntity
    {
        public Guid Id { get; set; }
        public Guid ProjectId { get; set; }
        public Guid UserId { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? ModificationDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}