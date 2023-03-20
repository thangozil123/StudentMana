using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentManage.Data;
using StudentManage.Models.DTO;
using StudentManage.Models.Entities;

namespace StudentManage.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : Controller
    {
        public readonly StudentDbContext dbContext;
        public StudentsController(StudentDbContext dbContext) 
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllStudent()
        {
            var students = await dbContext.Students.ToListAsync();
            return Ok(students);
        }

        [HttpGet]
        [Route("{id:int}")]

        public async Task<IActionResult> GetStudentById(int id)
        {
            var student = await dbContext.Students.FirstOrDefaultAsync(x => x.Id == id);
            if(student!=null)
            {
                return Ok(student);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> AddStudent(AddStudentRequest addStudentRequest)
        {
            var student = new Student()
            {
                Name= addStudentRequest.Name,
                Gender=addStudentRequest.Gender,
                Age=addStudentRequest.Age,
                Address=addStudentRequest.Address,
                Email=addStudentRequest.Email,
            };
            await dbContext.Students.AddAsync(student);
            await dbContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetStudentById), new {id=student.Id}, student);
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateStudent([FromRoute] int id, UpdateStudentRequest updateStudentRequest)
        {
            var student = new Student()
            {
                Name= updateStudentRequest.Name,
                Gender=updateStudentRequest.Gender,
                Age=updateStudentRequest.Age,
                Address=updateStudentRequest.Address,
                Email=updateStudentRequest.Email,
            };
            //check if exist
            var exist = await dbContext.Students.FindAsync(id);           
            if(exist != null)
            {
                exist.Name = updateStudentRequest.Name;
                exist.Gender = updateStudentRequest.Gender;
                exist.Age = updateStudentRequest.Age;
                exist.Address = updateStudentRequest.Address;
                exist.Email = updateStudentRequest.Email;
                await dbContext.SaveChangesAsync();
                return Ok(exist);
            }
            return NotFound();
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var exist = await dbContext.Students.FindAsync(id);
            if (exist != null)
            {
                dbContext.Remove(exist);
                await dbContext.SaveChangesAsync();
                return Ok(exist);
            }
            return NotFound();
        }
    }
}
