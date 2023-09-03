using Microsoft.AspNetCore.Mvc;
using TicketApi.Dtos;
using TicketApi.Helpers;
using TicketApi.Models;

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
        return Ok($"Hello! Message generated {DateTime.Now}");
    }

    [HttpGet]
    [Route("generatetest")]
    public ActionResult<TicketDto> GenerateTest()
    {
        var ticket = new Ticket("This is a test ticket");
        return Ok(ticket.ToDto());
    }
}
