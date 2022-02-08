using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ToDoList.Models.Entity;

namespace ToDoList.Models
{
    public class ToDoListContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public ToDoListContext(DbContextOptions options, IConfiguration configuration) : base(options)
        {
            Configuration = configuration;
        }

        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<ToDoTask> ToDoTasks { get; set; }
        public DbSet<SharedProject> SharedProjects { get; set; }
        public DbSet<Email> Emails { get; set; }
        public DbSet<FileInfo> FileInfos { get; set; }
    }
}