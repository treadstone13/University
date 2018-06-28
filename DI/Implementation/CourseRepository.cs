using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using University.DI.Interfaces;
using University.Models;

namespace University.DI.Implementation
{
    public class CourseRepository : ICourseRepository
    {
        private UniversityContext ctx;
        public CourseRepository(UniversityContext universityContext)
        {
            this.ctx = universityContext;
        }
        public void AddCourse(Course course)
        {
            ctx.Courses.Add(course);
            ctx.SaveChanges();
            
        }

        public void DeleteCourse(int id)
        {
            var course = ctx.Courses.FirstOrDefault(b => b.CourseID == id);
            if (course != null)
            {
                ctx.Courses.Remove(course);
                ctx.SaveChanges();
            }
          
        }

        public Course GetCourse(Course course)
        {
            var c = ctx.Courses.FirstOrDefault(b => b.CourseID == course.CourseID);
            return c;
           
        }

        public IEnumerable<Course> GetCourses()
        {
            var course = ctx.Courses.ToList();
            return course;
           
        }

        public void UpdateCourse(Course course)
        {           
            var c = ctx.Courses.Find(course.CourseID);
            if (c != null)
            {
                c.CourseID = course.CourseID;
                c.Title = course.Title;
                c.Credits = course.Credits;                
                ctx.SaveChanges();
            }
          
        }
    }
}
