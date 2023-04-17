using System;
using TaskAPI.Data;

namespace TaskAPI.Repository
{
    public abstract class BaseRepository
    {
        protected readonly TaskDbContext _dbContext;
        protected readonly string? connectionString;

        public BaseRepository(TaskDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            connectionString = configuration.GetConnectionString("TaskConnectionString");
        }
    }
}
