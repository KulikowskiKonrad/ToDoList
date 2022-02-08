using System.ComponentModel.DataAnnotations;

namespace ToDoList.Models.Model.User
{
    public class AuthenticateModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
