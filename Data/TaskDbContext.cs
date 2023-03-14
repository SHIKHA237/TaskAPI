using Microsoft.EntityFrameworkCore;
using TaskAPI.Models;

namespace TaskAPI.Data
{
    public class TaskDbContext : DbContext
    {
        public TaskDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Tasks> Tasks { get; set; }
        public DbSet<Assignee> Assignees { get; set; }
        public DbSet<Team> Teams { get; set; }

    }
}
