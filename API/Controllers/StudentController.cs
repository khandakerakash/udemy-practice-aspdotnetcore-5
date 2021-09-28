using System.Collections.Generic;
using System.Linq;
using DLL.Model;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class StudentController : RootController
    {
        [HttpGet]
        public IActionResult GetAllStudent()
        {
            return Ok(StudentStatic.GetAllStudent());
        }

        [HttpGet("{rollNo}")]
        public IActionResult GetAStudent(string rollNo)
        {
            return Ok(StudentStatic.GetAStudent(rollNo));
        }

        [HttpPost]
        public IActionResult InsertStudent(Student student)
        {
            return Ok(StudentStatic.InsertStudent(student));
        }

        [HttpPut("{rollNo}")]
        public IActionResult UpdateStudent(Student student, string rollNo)
        {
            return Ok(StudentStatic.UpdateStudent(student, rollNo));
        }
        
        [HttpDelete("{rollNo}")]
        public IActionResult DeleteStudent(string rollNo)
        {
            return Ok(StudentStatic.DeleteStudent(rollNo));
        }
    }

    public static class StudentStatic
    {
        private static List<Student> AllStudent { get; set; } = new List<Student>();

        public static Student InsertStudent(Student student)
        {
           AllStudent.Add(student);
           return student;
        }

        public static List<Student> GetAllStudent()
        {
            return AllStudent;
        }

        public static Student GetAStudent(string rollNo)
        {
            return AllStudent.FirstOrDefault(x => x.RollNo == rollNo);
        }

        public static Student UpdateStudent(Student student, string rollNo)
        {
            var result = new Student();

            foreach (var aStudent in AllStudent)
            {
                if (rollNo == student.RollNo)
                {
                    aStudent.Name = student.Name;
                    result = aStudent;
                }
            }
            return result;
        }

        public static Student DeleteStudent(string rollNo)
        {
            var student = AllStudent.FirstOrDefault(x => x.RollNo == rollNo);
            AllStudent.Remove(student);
            return student;
        }

    }
}