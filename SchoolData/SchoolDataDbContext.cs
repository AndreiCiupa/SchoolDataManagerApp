using Microsoft.EntityFrameworkCore;
using SchoolData.Models;

namespace SchoolData
{
    public class SchoolDataDbContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Mark> Marks { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Teacher> Teachers { get; set; }

        public SchoolDataDbContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\00.1_FastrackIT\SchoolDataManagerApp\SchoolData\SchoolDataDb.mdf;Integrated Security=True");
        }
    }
}
