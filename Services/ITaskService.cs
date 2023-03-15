using TaskAPI.Models;

namespace TaskAPI.Services
{
    public interface ITaskService
    {
        void CreateTask(Tasks task);
        List<TaskInformation> GetTask(string assignee, string team);
        List<Object> GetTeamsDetails();
    }
}
