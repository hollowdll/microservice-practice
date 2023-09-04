using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TicketApi.Data;
using TicketApi.Dtos;
using TicketApi.Helpers;
using TicketApi.Models;

namespace TicketApi.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class TicketController : ControllerBase
{
    private readonly ILogger<TicketController> _logger;
    private readonly TicketDbContext _ticketContext;

    public TicketController(ILogger<TicketController> logger, TicketDbContext ticketContext)
    {
        _logger = logger;
        _ticketContext = ticketContext;
    }

    [HttpGet]
    [Route("message")]
    public ActionResult Message()
    {
        return Ok($"Hello! Message generated {DateTime.Now}");
    }

    [HttpGet]
    [Route("generate")]
    public async Task<ActionResult<TicketDto>> Generate()
    {
        var ticket = new Ticket();

        _ticketContext.Tickets.Add(ticket);
        await _ticketContext.SaveChangesAsync();

        return Ok(ticket.ToDto());
    }

    [HttpGet("id/{id}")]
    public async Task<ActionResult<TicketDto>> GetTicketById(long id)
    {
        var ticket = await _ticketContext.Tickets.FindAsync(id);
        if (ticket == null)
        {
            return NotFound();
        }

        return Ok(ticket.ToDto());
    }

    [HttpGet]
    [Route("all")]
    public async Task<ActionResult<TicketDto>> GetAllTickets()
    {
        return Ok(await _ticketContext.Tickets.ToListAsync());
    }
}
