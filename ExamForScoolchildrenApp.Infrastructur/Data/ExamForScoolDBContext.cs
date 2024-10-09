using ExamForScoolchildrenApp.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Xml;

namespace ExamForScoolchildrenApp.Infrastructur.Data
{

    public class ExamForScoolDBContext : IdentityDbContext<AppUser>
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public ExamForScoolDBContext(DbContextOptions<ExamForScoolDBContext> options) : base(options) { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Exam> Exams { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Lesson>()
            .HasKey(e => e.Id);

            modelBuilder.Entity<Student>()
            .HasKey(e => e.Id);

            modelBuilder.Entity<Exam>()
            .HasKey(e => e.Id);
          
          
        }
    }



}
