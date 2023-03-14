using Microsoft.EntityFrameworkCore;
using TaskAPI.Data;
using TaskAPI.Models;

namespace TaskAPI.Services
{
    public class TaskService : ITaskService
    {
        private readonly TaskDbContext _dbContext;

        public TaskService(TaskDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void CreateTask(Tasks task)
        {
            _dbContext.Tasks.Add(task);
            _dbContext.SaveChanges();
        }
        public List<Tasks> GetTask()
        {
            return _dbContext.Tasks.ToList();
        }
    }
}
