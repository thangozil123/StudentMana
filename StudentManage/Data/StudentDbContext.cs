using Microsoft.EntityFrameworkCore;
using StudentManage.Models.Entities;

namespace StudentManage.Data
{
    public class StudentDbContext: DbContext
    {
        public StudentDbContext(DbContextOptions<StudentDbContext> options) : base(options) { }

        public DbSet<Student> Students { get; set; }
    }
}
