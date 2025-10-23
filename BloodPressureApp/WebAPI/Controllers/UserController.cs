using Microsoft.AspNetCore.Mvc;
using SharedLibrary.BloodPressureDomain.User;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly ILoginUser _loginUser;

    public UserController(ILoginUser loginUser)
    {
        _loginUser = loginUser;
    }

    // TODO: Expose a register endpoint

    /// <summary>
    /// Finds a registered user and logs them in if found
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    /// <remarks>
    /// Sample request:
    /// 
    ///     POST User/Login
    ///     {
    ///         "email": "test@email.com",
    ///         "password": "password123"
    ///     }
    /// 
    /// </remarks>
    /// <response code="200">User successfully logged in</response>
    /// <response code="401">User was not found</response>
    [HttpPost]
    [Route("Login")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> LoginAsync(LoginUser.Request request)
    {
        var user = await _loginUser.Handle(request);
        if (user is null)
        {
            return NotFound();
        }
        return Ok();
    }
}
