using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using University.DI.Interfaces;
using University.Models;

namespace University.DI.Implementation
{
    public class EnrollmentRepository : IEnrollmentRepository
    {
        private UniversityContext ctx;
        public EnrollmentRepository(UniversityContext universityContext)
        {
            this.ctx = universityContext;
        }
        public void AddEnrollment(Enrollment enrollment)
        {
            ctx.Enrollments.Add(enrollment);
            ctx.SaveChanges();
          
        }

        public void DeleteEnrollment(int id)
        {
            var e = ctx.Enrollments.FirstOrDefault(b => b.EnrollmentID == id);
            if (e != null)
            {
                ctx.Enrollments.Remove(e);
                ctx.SaveChanges();
            }
           
        }

        public Enrollment GetEnrollment(Enrollment enrollment)
        {
            var s = ctx.Enrollments.FirstOrDefault(b => b.EnrollmentID == enrollment.EnrollmentID);
            return s;
         
        }

        public IEnumerable<Enrollment> GetEnrollments()
        {
            var e = ctx.Enrollments.ToList();
            return e;
           
        }

        public void UpdateEnrollment(Enrollment enrollment)
        {
            var s = ctx.Enrollments.Find(enrollment.EnrollmentID);
            if (s != null)
            {
                s.EnrollmentID = enrollment.EnrollmentID;
                s.CourseID = enrollment.CourseID;
                s.StudentID = enrollment.StudentID;
                s.Grade = enrollment.Grade;
                ctx.SaveChanges();
            }
           
        }
    }
}
