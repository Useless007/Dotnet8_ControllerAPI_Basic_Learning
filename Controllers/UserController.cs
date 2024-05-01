using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")] // api/User  
// api/User มาจากชื่อ class controllerbase ที่ตั้งไว้ ในที่นี้คือ User | Controller
public class UserController : ControllerBase
{

    //Mock data for users
    private static readonly List<User> _users = new List<User>
    {
        new User{
            Id= 1,
            Username= "user1",
            Email= "hee@gmail.com",
            FullName= "User One"
        },
        new User{
            Id= 2,
            Username= "user2",
            Email= "hee2@gmail.com",
            FullName= "User Two"
        },
    };

    //Get all users
    //GET: api/User
    [HttpGet]
    public ActionResult<IEnumerable<User>> GetUsers()
    //ActionResult คือ คลาสที่ใช้สำหรับ return ค่ากลับไปให้ client
    // IEnumerable เป็น interface ใน .NET Framework ที่ใช้แทน Collection ของ object ที่สามารถ iterate ได้
    {
        return Ok(_users);
    }

    //Get user by id
    //GET: api/User/{id}
    [HttpGet("{id}")]
    public ActionResult<User> GetUser(int id)
    {
        //LINQ คืออะไร
        //Language Integrated Query หรือ LINQ คือ ภาษา query
        //ที่ใช้ในการ query ข้อมูลจาก datasource ใดๆ ใน .NET Framework
        var user = _users.Find(u => u.Id == id);
        if (user == null)
        {
            return NotFound();
        }
        return Ok(user);
    }

    //Create new user
    //POST: api/User
    [HttpPost]
    public ActionResult<User> CreateUser([FromBody] User user)
    {
        _users.Add(user);
        return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
    }

    //Update user by id
    //PUT: api/User/{id}
    [HttpPut("{id}")]
    public IActionResult UpdateUser(int id, [FromBody] User user)
    {
        //validate user id
        if (id != user.Id)
        {
            return BadRequest();
        }

        //Find existing user
        var existingUser = _users.Find(u => u.Id == id);
        if (existingUser == null)
        {
            return NotFound();
        }

        //Update user
        existingUser.Username = user.Username;
        existingUser.Email = user.Email;
        existingUser.FullName = user.FullName;

        //Return updated user
        return Ok(existingUser);
    }

    //Delete user by id
    //DELETE: api/User/{id}
    [HttpDelete("{id}")]
    public ActionResult DeleteUser(int id)
    {
        //Find existing user
        var existingUser = _users.Find(u => u.Id == id);
        if (existingUser == null)
        {
            return NotFound();
        }

        //Delete user
        _users.Remove(existingUser);

        //Return deleted user
        return NoContent();
    }
}