using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;
using System.Globalization;
using System.Threading.Tasks;
using TaskAPI.Data;
using TaskAPI.Models;
using TaskAPI.Task.Contracts;
using TaskAPI.Task.Contracts.Queries;
using static Azure.Core.HttpHeader;

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
        public List<TaskInformation> GetTask(GetAllPostQuery query)
        {
           
            if (!string.IsNullOrEmpty(query.AssigneeName) && query.Team=="")
            {
                var assignees = (from a in _dbContext.Assignees
                               where a.Name == query.AssigneeName
                                 select new
                               {
                                   a.Name,
                                   a.TaskId
                               }).ToList();
                var tasks = _dbContext.Tasks.ToList();

                var tasklist = (from t in tasks
                               join a in assignees on t.TaskId equals a.TaskId
                                select new TaskInformation()
                                {
                                    TaskId = t.TaskId,
                                    Title = t.Title,
                                    Description = t.Description,
                                    Team = t.Team,
                                    AssigneeName = a.Name.Split().ToList(),
                                    CreatedDate = t.CreatedDate,
                                    DueDate = t.DueDate,
                                    Status = t.Status
                                }).ToList();
               
                return tasklist;
            }
            else if (!string.IsNullOrEmpty(query.Team) && query.AssigneeName =="")
            {
                var tasklist = (from t in _dbContext.Tasks
                                let st = (from a in _dbContext.Assignees
                                          where a.TaskId == t.TaskId
                                          select a.Name).ToList()
                                where t.Team == query.Team
                                select new TaskInformation()
                                {
                                    TaskId = t.TaskId,
                                    Title = t.Title,
                                    Description = t.Description,
                                    Team = t.Team,
                                    AssigneeName = st,//string.Join(",", st),
                                    CreatedDate = t.CreatedDate,
                                    DueDate = t.DueDate,
                                    Status = t.Status
                                }).ToList();

                return tasklist;
            }
            else if (!string.IsNullOrEmpty(query.AssigneeName) && !string.IsNullOrEmpty(query.Team))
            {
                var tasklist = (from t in _dbContext.Tasks
                                join a in _dbContext.Assignees on t.TaskId equals a.TaskId
                                into eGroup
                                from e in eGroup.DefaultIfEmpty()
                                where t.Team == query.Team || e.Name==query.AssigneeName
                                select new TaskInformation()
                                {
                                    TaskId = t.TaskId,
                                    Title = t.Title,
                                    Description = t.Description,
                                    Team = t.Team,
                                    AssigneeName = e.Name != null ? e.Name.Split().ToList() : null,//string.Join(",", st),
                                    CreatedDate = t.CreatedDate,
                                    DueDate = t.DueDate,
                                    Status = t.Status
                                }).ToList();
                return tasklist;
            }
            else
            {
                var tasklist = (from t in _dbContext.Tasks
                                let st = (from a in _dbContext.Assignees where a.TaskId == t.TaskId select a.Name).ToList()
                                select new TaskInformation()
                                {
                                    TaskId = t.TaskId,
                                    Title = t.Title,
                                    Description = t.Description,
                                    Team = t.Team,
                                    AssigneeName = st,//string.Join(",", st),
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
