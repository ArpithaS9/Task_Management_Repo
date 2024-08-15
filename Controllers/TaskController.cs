using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Task_Mangement.Repository;
using Task_Mangement.Models;

namespace Task_Mangement.Controllers
{ 
    // Controllers/TasksController.cs

    [ApiController]

    [Route("api/[controller]")]

    public class UserController : ControllerBase

    {

        private readonly ITask _taskRepository;

        public UserController(ITask taskRepository)

        {

            _taskRepository = taskRepository;

        }

        [HttpGet("GetTasks")]
        public  IActionResult GetTasks()
        {
            return Ok( _taskRepository.GetAllTasks());
        }


        [HttpGet("{id}")]

        public  IActionResult GetTask(int id)
        {
            if (id == 0)
            {
                return BadRequest("Invalid Id");
            }
            var task =  _taskRepository.GetTaskById(id);
            if (task == null) return NotFound();
            return Ok(task);
        }

        [HttpPost]
        public  IActionResult CreateTask([FromBody] Tasks task)
        {
            if (task == null)
            {
                return BadRequest("Invalid Data");
            }
            var result =_taskRepository.AddTask(task);
            if (result == null)
            {
                return StatusCode(500, "Failed to create User");
            }

            return Ok(result);
           // return CreatedAtAction(nameof(GetTask), new { id = task.Id }, task);
        }

        [HttpPut("{id}")]

        public  IActionResult UpdateTask(int id, Tasks task)
        {
            if (task == null)
            {
                return BadRequest("Provide Task details to be updated");
            }
            if (id != task.Id || id == 0)
            {
                return BadRequest("Invalid ID");
            }
            
           var result =_taskRepository.UpdateTask(task);
            if (result == null) { return NotFound(); }
            return Ok(result);
        }

        [HttpDelete("{id}")]

        public IActionResult DeleteTask(int id)
        {
            if (id == 0)
            {
                return  BadRequest("Invalid ID");
            }
             _taskRepository.DeleteTask(id);
            return NoContent();
        }

    }

}
