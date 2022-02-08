using System;
using ToDoList.Models.Interfaces;

namespace ToDoList.Models.Entity
{
    public class Project : IEntity
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public ProjectType Type { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? ModificationDate { get; set; }
        public bool IsDeleted { get; set; }
    }

    public enum ProjectType
    {
    }
}