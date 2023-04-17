using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Http;
using System.Data;
using System.Linq;
using TaskAPI.Models;
using TaskAPI.Services;
using TaskAPI.Task.Contracts;
using TaskAPI.Task.Contracts.Queries;
using Newtonsoft.Json;
using System.Net;
using Swashbuckle.AspNetCore.Annotations;

namespace TaskAPI.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class TasksController : Controller
    {
        private readonly ITaskService _taskService;
        private readonly IAssigneeService _assigneeService;
        public TasksController(ITaskService taskService, IAssigneeService assigneeService)
        {
            _taskService = taskService;
            _assigneeService = assigneeService;
        }

        [HttpPost]
        [SwaggerResponse(201, Type = typeof(List<TaskResponse>))]
        public IActionResult CreateTask(CreateTaskRequest request)
        {
            try
            {
                var task = new Tasks(
               request.TaskId,
               request.Title,
               request.Description,
               request.Team,
               DateTime.UtcNow,
               request.DueDate,
               //  request.Image,
               "Active");
                var taskdata = _taskService.CreateTask(task);
               
                if(taskdata) 
                { 
                    return Conflict(new { message = $"An existing record with the id '{task.TaskId}' was already found." });
                }
                 List<string> AssigneesName = request.AssigneesName;
                foreach (var assigneeName in AssigneesName)
                {
                    var assignee = new Assignee(
                        assigneeName,
                        request.TaskId
                        );
                    _assigneeService.CreateTaskAssignee(assignee);
                }
                var response = new TaskResponse(
                task.TaskId,
                task.Title,
                task.Description,
                task.Team,
                AssigneesName,
                task.CreatedDate,
                task.DueDate,
                // task.Image,
                task.Status
                );

                // return Ok(response);
                return CreatedAtAction(
                    nameof(GetTask),
                    new {query = ""},
                    response);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet]
        [SwaggerResponse(200, Type = typeof(List<TaskInformation>))]
        public IActionResult GetTask([FromQuery] GetAllPostQuery query)
        {
            var TaskInformation   = _taskService.GetTask(query);
            return Ok(TaskInformation);
        }

        [HttpGet("team_count")]
        [SwaggerResponse(200, Type = typeof(List<TeamDetails>))]
        public IActionResult GetTeamsDetails()
        {
            var teamdetails = _taskService.GetTeamsDetails();
            return Ok(teamdetails);
        }

        [HttpGet("assignee_count")]
        [SwaggerResponse(200, Type = typeof(AssigneeDetails))]
        public IActionResult GetAssigneeCount([FromHeader] string assigneeName)
        {
            var AssigneeDetails = _taskService.GetAssigneeCount(assigneeName);
            return Ok(AssigneeDetails);
        }
    }
}
