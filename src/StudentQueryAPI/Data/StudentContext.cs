using Microsoft.EntityFrameworkCore;

namespace StudentQueryAPI.Data
{
    public class StudentContext : DbContext
    {
        public StudentContext(DbContextOptions<StudentContext> options) : base(options) { }

        #region DbSet
        public DbSet<Student> Students { get; set; }
        #endregion
    }
}
