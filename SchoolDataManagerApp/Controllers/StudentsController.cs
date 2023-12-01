﻿using Microsoft.AspNetCore.Mvc;
using SchoolData;
using SchoolDataManagerApp.Dtos;
using SchoolManagerApp.Extensions;

namespace SchoolDataManagerApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        // Get All Students
        [HttpGet("get-all-students")]
        public List<StudentToGet> GetAllStudents()
        {
            using var ctx = new SchoolDataDbContext();

            return ctx.Students.
                Select(s => s.ToDto()).
                ToList();
        }

        // Get Student
        [HttpGet("{studentId}/get-student")]
        public StudentToGet GetStudent(int studentId) 
        {
            using var ctx = new SchoolDataDbContext();

            return ctx.Students.
                FirstOrDefault(s => s.Id == studentId).
                ToDto();
        }

        // Create Student
        // !!!!!!
        // Add subject
        // !!!!!!
        [HttpPost("add-student")]
        public void AddStudent([FromBody] StudentToCreate student)
        {
            using var ctx = new SchoolDataDbContext();

            ctx.Students.Add(student.ToEntity());
            ctx.SaveChanges();
        }

        // Change Student Data
        [HttpPost("{studentId}/change-student-data")]
        public void ChangeStudentData(int studentId, [FromBody] StudentToCreate newStudent)
        {
            using var ctx = new SchoolDataDbContext();

            var student = ctx.Students.FirstOrDefault(s => s.Id == studentId);

            student = newStudent.ToEntity();
            ctx.SaveChanges();
        }
    }
}
