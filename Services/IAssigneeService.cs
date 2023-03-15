using TaskAPI.Models;

namespace TaskAPI.Services
{
    public interface IAssigneeService
    {
        void CreateTaskAssignee(Assignee assignee);
    }
}
