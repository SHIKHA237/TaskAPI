using TaskAPI.Models;

namespace TaskAPI.Task.Contracts
{
     public record CreateTaskRequest(
         int TaskId,
         string Title,
         string Description,
         string Team,
         List<string> AssigneesName,
         DateTime DueDate,
         List<string> Image);           
}
