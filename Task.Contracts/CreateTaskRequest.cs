using TaskAPI.Models;
using TaskAPI.Task.Contracts.DataAnnotations;


namespace TaskAPI.Task.Contracts
{
     public record CreateTaskRequest(
         int TaskId,
         string Title,
         string Description,
         string Team,
         List<string> AssigneesName,
         [Future] DateTime DueDate);
      //   List<string> Image);           
}
