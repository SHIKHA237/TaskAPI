using TaskAPI.Models;

namespace TaskAPI.Services
{
    public interface ITaskService
    {
        void CreateTask(Tasks task);
        List<Tasks> GetTask();
    }
}
