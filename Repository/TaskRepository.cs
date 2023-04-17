using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using TaskAPI.Data;
using TaskAPI.Models;
using TaskAPI.Task.Contracts.Queries;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace TaskAPI.Repository
{
    public class TaskRepository :BaseRepository, ITaskRepository
    {
        public TaskRepository(TaskDbContext dbContext, IConfiguration configuration) : base(dbContext, configuration)
        {
        }

        public List<DBResponseRow> GetDBResponseList(GetAllPostQuery queryparams)
        {
            string basequery = GetTaskQuery(queryparams);
            DataTable table = GetDataTable(queryparams, basequery);
            List<DBResponseRow> dbresponseRowList = GetDBResponseRowList(table);
            return dbresponseRowList;
        }   

        private List<DBResponseRow> GetDBResponseRowList(DataTable table)
        {
            List<DBResponseRow> dbresponseRowList = new List<DBResponseRow>();

            foreach (DataRow reader in table.Rows)
            {
                DBResponseRow taskdetail = new DBResponseRow();
                taskdetail.TaskId = int.Parse(reader["TaskId"].ToString()!);
                taskdetail.Title = reader["Title"].ToString();
                taskdetail.Description = reader["Description"].ToString();
                taskdetail.Team = reader["Team"].ToString();
                taskdetail.Name = Convert.ToString(reader["Name"]);
                taskdetail.CreatedDate = Convert.ToDateTime(reader["CreatedDate"]);
                taskdetail.DueDate = Convert.ToDateTime(reader["DueDate"]);
                taskdetail.Status = reader["Status"].ToString();
                dbresponseRowList.Add(taskdetail);
            }
            return dbresponseRowList;
        }

        private DataTable GetDataTable(GetAllPostQuery queryparams, string basequery)
        {
            DataTable table = new DataTable();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
               
                using (SqlCommand cmd = new SqlCommand(basequery, con))
                {
                    using (var da = new SqlDataAdapter(cmd))
                    {
                        if (!string.IsNullOrEmpty(queryparams.assigneeName))
                        {
                            cmd.Parameters.AddWithValue("@assignee", queryparams.assigneeName);
                        }
                        if (!string.IsNullOrEmpty(queryparams.team))
                        {
                            cmd.Parameters.AddWithValue("@team", queryparams.team);
                        }
                        da.Fill(table);
                    }
                }
                con.Close();
            }
            return table;
        }
        private string GetTaskQuery(GetAllPostQuery query) 
        {
            string basequery = "SELECT [t].[TaskId], [t].[Title], [t].[Description], [t].[Team],[a].[Name], [t].[CreatedDate], [t].[DueDate], [t].[Status] FROM [Tasks] AS [t] LEFT JOIN [dbo].[Assignees] AS [a] ON [t].[TaskId] = [a].[TaskId]";
            string whereclause = string.Empty;
            if (query.assigneeName != null)
            {
                whereclause = "WHERE [a].[Name] = @assignee";
               // sqlParameterCollection.AddWithValue("@assignee", string.IsNullOrEmpty(query.assigneeName) ? DBNull.Value : query.assigneeName);
            }
            if (query.team != null)
            {
                if (string.IsNullOrEmpty(whereclause))
                {
                    whereclause = "WHERE [t].[team] = @team";
                }
                else
                {
                    whereclause = whereclause + " AND [t].[team] = @team ";
                }
                //sqlParameterCollection.AddWithValue("@team", string.IsNullOrEmpty(query.team) ? DBNull.Value : query.team);
            }
            basequery = basequery + whereclause;
            return basequery;
        }

        public AssigneeDetails GetAssigneeCountResponse(string? assigneeName)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("Select count(TaskId) from Assignees WHERE Name=@assignee", con))
                {
                    cmd.Parameters.AddWithValue("@assignee", assigneeName);
                    int result = (int)cmd.ExecuteScalar();
                    con.Close();
                    return new AssigneeDetails { assigneeCount = result };
                }
            }
        }

        public List<TeamDetails> TeamCountResponse()
        {
            List<TeamDetails> teamcount = _dbContext.Tasks.GroupBy(x => x.Team).
                     Select(x => new TeamDetails { Team = x.Key, Count = x.Count() }).ToList();
            return teamcount;
        }

        public void SaveCreateTask(Tasks task)
        {
            _dbContext.Tasks.Add(task);
            _dbContext.SaveChanges();
        }

        public bool GetTaskData(Tasks task)
        {
           // var taskid = _dbContext.Tasks.Where(x => x.TaskId == taskId).Select(x => x.TaskId).FirstOrDefault();
            var taskdata = _dbContext.Tasks.Contains(task);
            return taskdata;
        }
            
    }
}
