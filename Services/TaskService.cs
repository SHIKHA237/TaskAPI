using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using TaskAPI.Data;
using TaskAPI.Models;
using TaskAPI.Task.Contracts;

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
        public List<TaskInformation> GetTask(string assignee = null, string team = null)
        {
            if (assignee == null || team == null)
            {
                var tasklist = (from t in _dbContext.Tasks
                                join a in _dbContext.Assignees on t.TaskId equals a.TaskId
                                select new TaskInformation()
                                {
                                    TaskId = t.TaskId,
                                    Title = t.Title,
                                    Description = t.Description,
                                    Team = t.Team,
                                    AssigneeName = a.Name,
                                    CreatedDate = t.CreatedDate,
                                    DueDate = t.DueDate,
                                    Status = t.Status
                                }).ToList();
                return tasklist;
            }
            else
            {
                var tasklist = (from t in _dbContext.Tasks
                                join a in _dbContext.Assignees on t.TaskId equals a.TaskId
                                where a.Name == assignee || t.Team == team
                                select new TaskInformation()
                                {
                                    TaskId = t.TaskId,
                                    Title = t.Title,
                                    Description = t.Description,
                                    Team = t.Team,
                                    AssigneeName = a.Name,
                                    CreatedDate = t.CreatedDate,
                                    DueDate = t.DueDate,
                                    Status = t.Status
                                }).ToList();
                return tasklist;
            }            
        }

        public List<Object> GetTeamsDetails()
        {

            var query = _dbContext.Tasks.GroupBy(x => x.Team).
                      Select(x => new { Team = x.Key, TasksCount = x.Count() }).ToList();
            return query.ToList<Object>();
        }

    }
}
