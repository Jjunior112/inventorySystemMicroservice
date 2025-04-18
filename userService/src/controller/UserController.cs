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

    public async Task<IActionResult> GetAll() => Ok(await _userService.GetUsers());

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserById(Guid id)
    {
        var user = await _userService.GetUserById(id);

        return user == null ? NotFound() : Ok(user);
    }

    [HttpPost("Signup")]

    public async Task<IActionResult> AddUser(RegisterRequest request)
    {
        var user = new User(request.firstName, request.lastName, request.userEmail, request.password, request.bornDate);

        await _userService.RegisterUser(user);

        var userdto = new UserDto(user.UserId, user.UserName);

        return CreatedAtAction(nameof(GetUserById), new { id = user.UserId }, userdto);

    }

    [HttpPost("Signin")]

    public async Task<IActionResult> Login(LoginRequest request, IConfiguration config)
    {

        var token = await _userService.LoginUser(request, config);

        if (token == null) return Forbid("Usuário ou senha inválidos!");

        return Ok(token);

    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        return await _userService.DeleteUser(id) ? NoContent() : NotFound();
    }

}