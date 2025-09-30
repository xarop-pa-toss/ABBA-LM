using LMWebAPI.Models;
using LMWebAPI.Services.Players;
using Microsoft.AspNetCore.Mvc;
namespace LMWebAPI.Controllers;

// [Authorize]
[ApiController]
[Route("api/[controller]")]
public class TestController() : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<string>> Test()
    {
        return Ok("Test completed successfuly!!");
    }
}