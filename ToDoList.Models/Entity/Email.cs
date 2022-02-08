using System;
using ToDoList.Models.Interfaces;

namespace ToDoList.Models.Entity
{
    public class Email : IEntity
    {
        public Guid Id { get; set; }
        public string ReceiveUserEmail { get; set; }
        public string SendUserEmail { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public DateTime? SendDate { get; set; }
        public DateTime? ReceiveDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}