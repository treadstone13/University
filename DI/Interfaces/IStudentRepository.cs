using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using University.Models;

namespace University.DI.Interfaces
{
    public interface IStudentRepository
    {
        IEnumerable<Student> GetStudents();
        Student GetStudent(Student student);
        void AddStudent(Student student);
        void UpdateStudent(Student student);
        void DeleteStudent(int id);
    }
}
