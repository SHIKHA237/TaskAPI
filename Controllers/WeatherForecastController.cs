using Microsoft.AspNetCore.Mvc;
using TaskAPI.Models;

namespace TaskAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        //task demo controller
        //private static List<Tasks> Tasks = new List<Tasks>();


        //[HttpPost]
        //public IActionResult Post(CreateTaskRequest task)
        //{
        //    Tasks newtask = new Tasks();
        //    newtask.TaskID = task.TaskId;
        //    newtask.Title = task.Title;
        //    newtask.Team = task.Team;
        //    newtask.Description = task.Description;
        //    //  newtask.AssigneesName = task.AssigneesName;
        //    newtask.DueDate = task.DueDate;
        //    //  newtask.Image = task.Image;
        //    newtask.Status = "Active";
        //    newtask.CreatedDate = DateTime.UtcNow;
        //    int query = Tasks.Where(x => x.TaskID == newtask.TaskID).Select(x => x.TaskID).FirstOrDefault();
        //    //if (query == Guid.Empty)
        //    Console.WriteLine(query);
        //    if (query == 0)
        //    {
        //        Tasks.Add(newtask);
        //    }
        //    else { return StatusCode(409); }
        //    return Ok(newtask);
        //}

        //public class SomeObject
        //{
        //    public string S { get; set; } = "";
        //    public int I { get; set; } = 0;
        //    public DateTime? Date { get; set; } = null;
        //}

        //[HttpGet]
        //public List<Tasks> Get([FromQuery] SomeObject SomeObject)
        //{
        //    //return Tasks.Where(x => x.Team == team).ToList();
        //    return Tasks.ToList();
        //}
        //[HttpGet("count")]
        //public ActionResult GetteamsDetails()
        //{
        //    var query = Tasks.GroupBy(x => x.Team).
        //              Select(x => new { Team = x.Key, TasksCount = x.Count() }).ToList();
        //    return Ok(query);
        //}

    }
}