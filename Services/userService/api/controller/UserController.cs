using Microsoft.AspNetCore.Mvc;


[ApiVersion("1.0")]
[ApiController]
[Route("api/v{version:apiVersion}/users")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
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

        return Ok(new UserDto(user.UserName,user.UserEmail));
    }

    [HttpPost]

    public async Task<IActionResult> CreateUser(AddUserRequest request)
    {



        var user = new User(request.firstName, request.lastName, request.userEmail, request.userPassword);

        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(user.UserPassword);

        user.UserPassword = hashedPassword;

        var confirmUser = await _userService.CreateUser(user);

        if (confirmUser == null) return BadRequest();

        return CreatedAtAction(nameof(GetUserById), new { id = confirmUser.UserId }, new UserDto(confirmUser.UserName,confirmUser.UserEmail));

    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(Guid id)
    {
        var user = await _userService.DeleteUser(id);

        if (!user) return NotFound("User not found!");

        return NoContent();

    }
}