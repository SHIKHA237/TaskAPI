using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.Threading.Tasks;
using TaskAPI.Data;
using TaskAPI.Models;
using TaskAPI.Repository;
using TaskAPI.Task.Contracts;
using TaskAPI.Task.Contracts.Queries;
using static Azure.Core.HttpHeader;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace TaskAPI.Services
{
    public class TaskService : ITaskService
    {
        private readonly TaskDbContext _dbContext;
        private readonly TaskRepository _taskRepository;
        private readonly string? connectionString;

        public TaskService(TaskDbContext dbContext,IConfiguration configuration, TaskRepository taskRepository)
        {
            _dbContext = dbContext;
             connectionString = configuration.GetConnectionString("TaskConnectionString");
            _taskRepository = taskRepository;
        }
        public void CreateTask(Tasks task)
        {
            _dbContext.Tasks.Add(task);
            _dbContext.SaveChanges();
        }
        public List<TaskInformation> GetTask(GetAllPostQuery query)
        {
            List<DBResponseRow> rowlist = _taskRepository.GetDBResponseList(query);
            Dictionary<int, TaskInformation> result = new Dictionary<int, TaskInformation>();
            foreach (DBResponseRow row in rowlist)
            {
                if(result.ContainsKey(row.TaskId) && !string.IsNullOrEmpty(row.Name))
                {
                    result[row.TaskId].AssigneeName.Add(row.Name);
                }
                else
                {
                    TaskInformation taskInformation = new TaskInformation();
                    taskInformation.TaskId = row.TaskId;
                    taskInformation.Status = row.Status;
                    taskInformation.DueDate = row.DueDate;
                    taskInformation.CreatedDate = row.CreatedDate;
                    taskInformation.AssigneeName = new HashSet<string>();
                    if(!string.IsNullOrEmpty(row.Name))
                    {
                        taskInformation.AssigneeName.Add(row.Name);
                    }
                    taskInformation.Description = row.Description;
                    taskInformation.Title = row.Title;
                    taskInformation.Team = row.Team;
                    result[row.TaskId] = taskInformation;
                }
            }
            return result.Values.ToList();
             
        }


        public List<TeamDetails> GetTeamsDetails()
        {

            var query = _dbContext.Tasks.GroupBy(x => x.Team).
                      Select(x => new TeamDetails{ Team = x.Key, Count = x.Count() }).ToList();
            return query;
        }

        public AssigneeDetails GetAssigneeCount(string? assigneeName)
        {

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("DECLARE @Assignees table (TaskId int,Name varchar(250));INSERT INTO @Assignees(TaskId,Name) SELECT DISTINCT a1.TaskId,STUFF((SELECT ', ' + a2.name FROM [Assignees] a2 where a1.TaskId = a2.TaskId FOR XML PATH ('')) , 1, 1, '')  AS Name from [Assignees] a1; SELECT COUNT([t].[TaskId]) FROM [Tasks] AS [t] LEFT JOIN  @Assignees AS [a] ON [t].[TaskId] = [a].[TaskId] WHERE  (@assignee IS NULL OR [a].[Name] like  '%'+@assignee+'%' );", con))
                {
                    cmd.Parameters.AddWithValue("@assignee", string.IsNullOrEmpty(assigneeName) ? DBNull.Value : assigneeName);
                    int result = (int)cmd.ExecuteScalar();
                    con.Close();
                    return new AssigneeDetails { assigneeCount = result };
                }
            }
        }
    }
}
