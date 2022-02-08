using System;
using System.ComponentModel.DataAnnotations;

namespace ToDoList.Models.Interfaces
{
    public interface IEntity
    {
        [Required]
        public Guid Id { get; set; }
    }
}