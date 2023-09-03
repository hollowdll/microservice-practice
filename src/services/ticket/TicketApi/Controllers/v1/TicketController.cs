using Microsoft.AspNetCore.Mvc;

namespace TicketApi.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class TicketController : ControllerBase
{
    private readonly ILogger<TicketController> _logger;

    public TicketController(ILogger<TicketController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    [Route("message")]
    public ActionResult Message()
    {
        var date = DateTime.Now;
        return Ok($"Hello! Message generated {date}");
    }
}
