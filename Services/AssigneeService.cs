using TaskAPI.Data;
using TaskAPI.Models;

namespace TaskAPI.Services
{
    public class AssigneeService : IAssigneeService
    {
        private readonly TaskDbContext _dbContext;
        public AssigneeService(TaskDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void CreateAssignee(Assignee assignee)
        {
            _dbContext.Assignees.Add(assignee);
            _dbContext.SaveChanges();
        }
    }
}
