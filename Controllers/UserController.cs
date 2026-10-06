using Microsoft.AspNetCore.Mvc;
[ApiController]
[Route("api/[controller]")]
public class UserController:ControllerBase
{
    private static readonly Dictionary<int, User> Users = new()
    {
        [1] = new User
        {
            Id = 1,
            FirstName = "John",
            LastName = "Smith",
            Email = "john.smith@techhive.com",
            Department = "IT"
        },
        [2] = new User
        {
            Id = 2,
            FirstName = "Jane",
            LastName = "Doe",
            Email = "jane.doe@techhive.com",
            Department = "HR"
        }
    };    

    [HttpGet]
    public ActionResult<IEnumerable<User>> GetUsers()
    {
        return Ok(Users.Values);
    }

    [HttpGet("{id}")]
    public ActionResult<User> GetUserById(int id)
    {
        try
        {
            if (!Users.TryGetValue(id, out var user))
            {
                return NotFound($"User with ID {id} not found.");
            }
             return Ok(user);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
       
       
    }

    [HttpPost]
    public ActionResult<User> CreateUser(User user)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
        user.Id = Users.Count > 0 ? Users.Keys.Max() + 1 : 1;
        Users.Add(user.Id, user);
        return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);
    
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }  

    [HttpPut("{id}")]
    public ActionResult<User> UpdateUser(int id, User updatedUser)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
        if (!Users.TryGetValue(id, out var user))
        {
            return NotFound();
        }
        user.FirstName = updatedUser.FirstName;
        user.LastName = updatedUser.LastName;
        user.Email = updatedUser.Email;
        user.Department = updatedUser.Department;
        return Ok(user);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public ActionResult DeleteUser(int id)
    {
        try
        {
            if (!Users.Remove(id))
            {
                return NotFound();
            }
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
       
    }
}