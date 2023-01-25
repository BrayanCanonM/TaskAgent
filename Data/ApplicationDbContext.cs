using Microsoft.EntityFrameworkCore;
using TaskAgent.Models;

namespace TaskAgent.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> option) : base(option)
        { 
        }

        public DbSet<TimeTracker> timeTrackers { get; set; }
    }
}
