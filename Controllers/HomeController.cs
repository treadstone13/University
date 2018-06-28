using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using University.Models;
using University.DI.Interfaces;
using X.PagedList;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;

namespace University.Controllers
{
    public class HomeController : Controller
    {
        private IStudentRepository _StudentRepository;
        private ICourseRepository _CourseRepository;
        private IEnrollmentRepository _EnrollmentRepository;
        private IRequestRepository _RequestRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public HomeController(IStudentRepository studentRepository, ICourseRepository courseRepository, IEnrollmentRepository enrollmentRepository, IRequestRepository requestRepository
            ,IHttpContextAccessor httpContextAccessor)
        {
            _StudentRepository = studentRepository;
            _CourseRepository = courseRepository;
            _EnrollmentRepository = enrollmentRepository;
            _RequestRepository = requestRepository;
            _httpContextAccessor = httpContextAccessor;

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sortOrder"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public IActionResult Index(string sortOrder, int? page)
        {
            ViewData["CurrentSort"] = sortOrder;            
            ViewData["IDSortParm"] = sortOrder == "id_desc" ? "" : "id_desc";

            var ListAll = _EnrollmentRepository.GetEnrollments();


            ViewBag.CourseList = _CourseRepository.GetCourses().Select(c => new SelectListItem { Text = c.CourseID.ToString(), Value = c.CourseID.ToString()});
         
            
            ViewBag.StudentList = _StudentRepository.GetStudents().Select(v => new SelectListItem { Text = v.LastName + v.FirstMidName, Value = v.ID.ToString() });
            switch (sortOrder)
            {                
                case "id_desc":
                    ListAll = ListAll.OrderByDescending(s => s.EnrollmentID);
                    break;
                default:
                    ListAll = ListAll.OrderBy(s => s.EnrollmentID);
                    break;
            }

            var pageNumber = page ?? 1;
            var onePageOfEnrollment = ListAll.ToPagedList(pageNumber, 5);
            ViewBag.onePageOfEnrollment = onePageOfEnrollment;
            return View();            
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sortOrder"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public IActionResult Course(string sortOrder, int? page)
        {
            ViewData["CurrentSort"] = sortOrder;            
            ViewData["IDSortParm"] = sortOrder == "id_desc" ? "" : "id_desc";

            var ListAll = _CourseRepository.GetCourses();

            switch (sortOrder)
            {                
                case "id_desc":
                    ListAll = ListAll.OrderByDescending(s => s.CourseID);
                    break;
                default:
                    ListAll = ListAll.OrderBy(s => s.CourseID);
                    break;
            }

            var pageNumber = page ?? 1;
            var onePageOfCourse = ListAll.ToPagedList(pageNumber, 5);
            ViewBag.onePageOfCourse = onePageOfCourse;
            return View();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sortOrder"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public IActionResult Student(string sortOrder, int? page)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "name_asc";
            ViewData["IDSortParm"] = sortOrder == "id_desc" ? "" : "id_desc";
            
            var ListAll = _StudentRepository.GetStudents();

            switch (sortOrder)
            {
                case "name_desc":
                    ListAll = ListAll.OrderByDescending(s => s.FirstMidName);
                    break;
                case "name_asc":
                    ListAll = ListAll.OrderBy(s => s.FirstMidName);
                    break;
                case "id_desc":
                    ListAll = ListAll.OrderByDescending(s => s.ID);
                    break;
                default:
                    ListAll = ListAll.OrderBy(s => s.ID);
                    break;
            }

            var pageNumber = page ?? 1;
            var onePageOfStudent = ListAll.ToPagedList(pageNumber, 5);
            ViewBag.onePageOfStudent = onePageOfStudent;
            return View();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult AddStudent(Student student)
        {            
            _StudentRepository.AddStudent(student);
            return RedirectToAction("Student", "Home");
        }

        public IActionResult DeleteStudent(int id)
        {                 
            _StudentRepository.DeleteStudent(id);
            return RedirectToAction("Student", "Home");
        }

        public IActionResult UpdateStudent(Student student)
        {
            _StudentRepository.UpdateStudent(student);
            return RedirectToAction("Student", "Home");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult DeleteCourse(int id)
        {
            _CourseRepository.DeleteCourse(id);
            return RedirectToAction("Course", "Home");
        }

        public IActionResult UpdateCourse(Course course)
        {
            _CourseRepository.UpdateCourse(course);
            return RedirectToAction("Course", "Home");
        }

        public IActionResult AddCourse(Course course)
        {
            _CourseRepository.AddCourse(course);
            return RedirectToAction("Course", "Home");
        }
        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult DeleteEnrollment(int id)
        {
            _EnrollmentRepository.DeleteEnrollment(id);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult UpdateEnrollment(Enrollment enrollment)
        {
            _EnrollmentRepository.UpdateEnrollment(enrollment);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult AddEnrollment(Enrollment enrollment)
        {
            _EnrollmentRepository.AddEnrollment(enrollment);
            return RedirectToAction("Index", "Home");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IActionResult MiddlewareTest(int id)
        {
            ViewBag.Request = _RequestRepository.GetRequests();
            

            return View();
        }




        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
