using TaskAPI.Models;

namespace TaskAPI.Services
{
    public interface IAssigneeService
    {
        void CreateAssignee(Assignee assignee);
    }
}
