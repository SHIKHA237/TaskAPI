using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Http;
using System.Linq;
using TaskAPI.Models;
using TaskAPI.Services;
using TaskAPI.Task.Contracts;

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
            var task = new Tasks(
               request.TaskId,
               request.Title,
               request.Description,
               request.Team,               
               DateTime.UtcNow,
               request.DueDate,
             //  request.Image,
               "Active");

            List<String> AssigneesName = request.AssigneesName;
            foreach (var assigneeName in AssigneesName)
            {
                var assignee = new Assignee(
                    assigneeName,
                    request.TaskId
                    );
                _assigneeService.CreateAssignee(assignee);
            }
            _taskService.CreateTask(task);

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

        [HttpGet]
        public IActionResult GetTask()
        {
            return Ok();
        }

        [HttpGet("count")]
        public IActionResult GetTeamsDetails()
        {
            return Ok();
        }
    }
}
