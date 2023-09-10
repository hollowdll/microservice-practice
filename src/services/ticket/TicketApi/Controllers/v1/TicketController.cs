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

    [HttpGet("id/{id}")]
    public async Task<ActionResult<TicketDto>> GetTicketById(int id)
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
    public async Task<ActionResult<IList<TicketDto>>> GetAllTickets()
    {
        var tickets = await _ticketContext.Tickets
            .Select(i => i.ToDto())
            .ToListAsync();

        return Ok(tickets);
    }

    [HttpPost]
    [Route("generate")]
    public async Task<ActionResult> CreateTicket(TicketCreateDto ticketCreateDto)
    {
        var ticket = new Ticket(ticketCreateDto.CustomerId);

        _ticketContext.Tickets.Add(ticket);
        await _ticketContext.SaveChangesAsync();

        return CreatedAtAction(nameof(GetTicketById), new { id = ticket.Id }, ticket);
    }
}
