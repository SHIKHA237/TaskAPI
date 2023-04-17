using Azure;
using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.Net;
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
        private readonly ITaskRepository _taskRepository;
        
        public TaskService(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }
        public bool CreateTask(Tasks task)
        {
           
            var taskdata = _taskRepository.GetTaskData(task);
            if(taskdata)
            {
                return true;
            }
            else
            {
                _taskRepository.SaveCreateTask(task);
                return false;
            }
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
            return _taskRepository.TeamCountResponse();
        }

        public AssigneeDetails GetAssigneeCount(string? assigneeName)
        {
            return _taskRepository.GetAssigneeCountResponse(assigneeName);
        }

       
    }
}
