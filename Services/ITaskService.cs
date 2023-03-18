using TaskAPI.Models;
using TaskAPI.Task.Contracts.Queries;

namespace TaskAPI.Services
{
    public interface ITaskService
    {
        void CreateTask(Tasks task);
        List<TaskInformation> GetTask(GetAllPostQuery query);
        List<Object> GetTeamsDetails();
    }
}
