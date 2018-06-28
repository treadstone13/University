using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;


namespace University.Models
{
    public class UniversityContext : DbContext
    {
        public UniversityContext(DbContextOptions<UniversityContext> options) : base(options)
        {
        }
        public DbSet<Student> Students { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Request> Requests { get; set; }       

        public DbSet<ApplicationRole> ApplicationRoles { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        public DbSet<ApplicationRoleViewModels> ApplicationRoleViewModels { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //var connection = @"Server=EGOISTSYSTEM;Database=University;Trusted_Connection=True;ConnectRetryCount=0";
            //optionsBuilder.UseSqlServer(connection);

        }
    }
}
