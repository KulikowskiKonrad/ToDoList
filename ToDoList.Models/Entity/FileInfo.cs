using System;
using ToDoList.Models.Interfaces;

namespace ToDoList.Models.Entity
{
    public class FileInfo : IEntity
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string RelativePath { get; set; }
        public DateTime AddDate { get; set; }
        public string OriginalName { get; set; }
        public double Size { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? ModificationDate { get; set; }
    }
}