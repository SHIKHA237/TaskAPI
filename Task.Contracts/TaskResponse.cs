namespace TaskAPI.Task.Contracts
{
    public record TaskResponse(
         int TaskId,
         string Title,
         string Description,
         string Team,
         List<string> AssigneesName,
         DateTime CreatedDate,
         DateTime DueDate,
       //  List<string> Image,
         string Status);           
}
