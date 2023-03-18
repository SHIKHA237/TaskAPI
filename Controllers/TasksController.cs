using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Http;
using System.Linq;
using TaskAPI.Models;
using TaskAPI.Services;
using TaskAPI.Task.Contracts;
using TaskAPI.Task.Contracts.Queries;

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

                _taskService.CreateTask(task);

                List<String> AssigneesName = request.AssigneesName;
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

                return Ok(response);

            }
            catch(Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
          

        }

        [HttpGet]
        public IActionResult GetTask([FromQuery] GetAllPostQuery query)
        {
            var taskinformation = _taskService.GetTask(query);
            return Ok(taskinformation);
        }

        [HttpGet("count")]
        public IActionResult GetTeamsDetails()
        {
            var teamdetails = _taskService.GetTeamsDetails();
            return Ok(teamdetails);
        }
    }
}
