using Microsoft.AspNetCore.Mvc;
using System.Data;
using TaskAPI.Models;
using TaskAPI.Task.Contracts;
using TaskAPI.Task.Contracts.Queries;

namespace TaskAPI.Services
{
    public interface ITaskService
    {
        bool CreateTask(Tasks task);
        List<TaskInformation> GetTask(GetAllPostQuery query);
        List<TeamDetails> GetTeamsDetails();
        AssigneeDetails GetAssigneeCount(string? assigneeName = null);
    }
}
