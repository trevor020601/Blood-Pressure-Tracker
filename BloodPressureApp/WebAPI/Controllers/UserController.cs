using Microsoft.AspNetCore.Mvc;
using SharedLibrary.BloodPressureDomain.User;
using SharedLibrary.BloodPressureDomain.ValueObjects;
using SharedLibrary.Messaging;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController(ILoginUser loginUser) : ControllerBase
{
    // Test this out and revise...
    [HttpPost]
    [Route("Register")]
    public async Task<IActionResult> RegisterAsync(string email, 
                                                   string password, 
                                                   ICommandHandler<UserRegisterCommand, UserId> handler, 
                                                   CancellationToken cancellationToken)
    {
        var command = new UserRegisterCommand(Email.Create(email), password);
        var result = await handler.Handle(command, cancellationToken);
        return Ok(result);
    }

    // TODO: Refactor below now that there is a concrete CQRS implementation

    /// <summary>
    /// Finds a registered user and logs them in if found
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
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
    public async Task<IActionResult> LoginAsync(LoginUser.Request request, CancellationToken cancellationToken)
    {
        var user = await loginUser.Handle(request, cancellationToken);
        if (user is null)
        {
            return NotFound();
        }
        return Ok();
    }
}
