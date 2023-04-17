using TaskAPI.Models;
using TaskAPI.Task.Contracts.Queries;

namespace TaskAPI.Repository
{
    public interface ITaskRepository
    {
        List<DBResponseRow> GetDBResponseList(GetAllPostQuery queryparams);
        AssigneeDetails GetAssigneeCountResponse(string? assigneeName);
        List<TeamDetails> TeamCountResponse();
        void SaveCreateTask(Tasks task);
        bool GetTaskData(Tasks task );
    }
}
