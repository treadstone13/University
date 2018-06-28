using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using University.Models;
namespace University.DI.Interfaces
{
    public interface IEnrollmentRepository
    {
        IEnumerable<Enrollment> GetEnrollments();
        Enrollment GetEnrollment(Enrollment enrollment);
        void AddEnrollment(Enrollment enrollment);
        void UpdateEnrollment(Enrollment enrollment);
        void DeleteEnrollment(int id);
    }
}
