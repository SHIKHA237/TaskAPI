using System;
using TaskAPI.Data;

namespace TaskAPI.Repository
{
    public abstract class BaseRepository
    {
        protected readonly TaskDbContext _context;

        public BaseRepository(TaskDbContext context)
        {
            _context = context;
        }
    }
}
