using System;

namespace ToDoList.Models.Model.User
{
    public class UpdateUserModel
    {
        public Guid Id { get; set; }
        public string PasswordSalt { get; set; }
        public string Login { get; set; }
        public string PasswordEncrypted { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? ModificationDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
