using System.ComponentModel.DataAnnotations;

namespace TaskAPI.Task.Contracts.DataAnnotations
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter ,AllowMultiple = false)]
    public class FutureAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            return value is DateTime dateTime && dateTime < DateTime.UtcNow;
        }
    }
}
