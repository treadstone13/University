using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using University.DI.Interfaces;
using University.Models;

namespace University.DI.Implementation
{
    public class StudentRepository : IStudentRepository
    {
        private UniversityContext ctx;
        public StudentRepository(UniversityContext universityContext)
        {
            this.ctx = universityContext;
        }
        public void AddStudent(Student student)
        {
            ctx.Students.Add(student);
            ctx.SaveChanges();
            
        }

        public void DeleteStudent(int id)
        {
            var student = ctx.Students.FirstOrDefault(b => b.ID == id);
            if (student != null)
            {
                ctx.Students.Remove(student);
                ctx.SaveChanges();
            }
            
        }

        public Student GetStudent(Student student)
        {
            var s = ctx.Students.FirstOrDefault(b => b.ID == student.ID);
            return s;
          
        }

        public IEnumerable<Student> GetStudents()
        {
            var students = ctx.Students.ToList();
            return students;
         
        }

        public void UpdateStudent(Student student)
        {          
            var s = ctx.Students.Find(student.ID);
            if (s != null)
            {
                s.ID = student.ID;
                s.LastName = student.LastName;
                s.FirstMidName = student.FirstMidName;
                s.EnrollmentDate = student.EnrollmentDate;
                ctx.SaveChanges();
            }
           
        }
    }
}
