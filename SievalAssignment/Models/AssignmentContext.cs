using Microsoft.EntityFrameworkCore;

namespace SievalAssignment.Models
{
    public class AssignmentContext : DbContext // TODO unit tests for this class
    {
        public DbSet<Article> Articles { get; set; }

        public string DbPath { get; }

        public AssignmentContext()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = Path.Join(path, "assignment.db");
        }

        // The following configures EF to create a Sqlite database file in the
        // special "local" folder for your platform.
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");
    }
}