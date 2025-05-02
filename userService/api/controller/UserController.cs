using Microsoft.AspNetCore.Mvc;


[ApiController]
[Route("api/users")]
public class UserController : ControllerBase
{
    private readonly UserService _userService;

    public UserController(UserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllUsers() => Ok(await _userService.GetAllUsersAsync());

    [HttpGet("{id}")]

    public async Task<IActionResult> GetUserById(Guid id)
    {
        var user = await _userService.GetUserByIdAsync(id);

        if (user == null) return NotFound();

        return Ok(user);
    }

    [HttpPost]

    public async Task<IActionResult> CreateUser(AddUserRequest request)
    {
        var user = new User(request.firstName, request.lastName, request.userEmail, request.userPassword);

        var confirmUser = await _userService.CreateUser(user);

        if (confirmUser == null) return BadRequest();

        return CreatedAtAction(nameof(GetUserById), new { id = confirmUser.UserId }, confirmUser);

    }
    [HttpDelete]
    public async Task<IActionResult> DeleteUser(Guid id)
    {
        var user = await _userService.DeleteUser(id);

        if (!user) return NotFound("User not found!");

        return NoContent();

    }
}