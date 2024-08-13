using Microsoft.AspNetCore.Mvc;
using Task_Mangement.Models;
using Task_Mangement.Repository;

namespace Task_Mangement.Controllers
{


  [ApiController]

  [Route("api/[controller]")]

public class UsersController : ControllerBase

    {

        private readonly IUser _userRepository;

        public UsersController(IUser userRepository)

        {

            _userRepository = userRepository;

        }

        [HttpGet]

        public IActionResult GetUsers()
        {
            return Ok(_userRepository.GetAllUsers());
        }

        [HttpGet("{id}")]

        public  IActionResult GetUser(int id)
        {
            if(id == 0)
            {
                return BadRequest("Invalid Id");
            }
            var user =  _userRepository.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost]

        public IActionResult CreateUser( [FromBody] User user)
        {
            if(user == null)
            {
                return BadRequest("Invalid Data");
            }

            _userRepository.AddUser(user);
            var createdUser =   _userRepository.GetLatestUser();
            return Ok(createdUser);
           // return CreatedAtAction(nameof(GetUser), new { id = user.Id }, createdUser);
        }

        [HttpPut("{id}")]

        public  IActionResult UpdateUser(int id, [FromBody] User user)
        {
            if (id != user.Id || id == 0 )
            {
                return BadRequest("Invalid ID");
            }
            if(user == null)
            {
                return BadRequest("Provide User details to be updated");

            }
            _userRepository.UpdateUser(user);
              return NoContent();
        }


        [HttpDelete("{id}")]

        public IActionResult DeleteUser(int id)
        {
            if(id == 0)
            {
                return BadRequest("Invalid Id");
            }
            _userRepository.DeleteUser(id);
            return NoContent();
        }
        
    } 

}