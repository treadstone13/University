using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using University.Models;
namespace University.DI.Interfaces
{
    public interface ICourseRepository
    {
        IEnumerable<Course> GetCourses();
        Course GetCourse(Course course);
        void AddCourse(Course course);
        void UpdateCourse(Course course);
        void DeleteCourse(int id);
    }

}
