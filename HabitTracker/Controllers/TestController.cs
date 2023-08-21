using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HabitTracker.Controllers;

[Authorize(AuthenticationSchemes = "Bearer")]
[ApiController]
[Route("api/protected")]
public class ProtectedController : ControllerBase
{
    [HttpGet]
    public IActionResult GetProtectedResource()
    {
        return Ok("This is a protected resource");
    }
}