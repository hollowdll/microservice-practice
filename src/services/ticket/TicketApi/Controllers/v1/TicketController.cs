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

    // Gets a ticket by id.
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

    // Gets all tickets.
    [HttpGet]
    [Route("all")]
    public async Task<ActionResult<IList<TicketDto>>> GetAllTickets()
    {
        var tickets = await _ticketContext.Tickets
            .Select(i => i.ToDto())
            .ToListAsync();

        return Ok(tickets);
    }

    // Creates a new ticket.
    [HttpPost]
    public async Task<ActionResult<TicketDto>> CreateTicket(TicketCreateDto ticketCreateDto)
    {
        var ticket = new Ticket(ticketCreateDto.CustomerId);

        _ticketContext.Tickets.Add(ticket);
        await _ticketContext.SaveChangesAsync();

        _logger.LogInformation("Created a new ticket with ID '{TicketId}' via HTTP API", ticket.Id);

        return CreatedAtAction(nameof(GetTicketById), new { id = ticket.Id }, ticket.ToDto());
    }

    // Gets all customer's tickets.
    [HttpGet]
    [Route("all/customer/{customerId}")]
    public async Task<ActionResult<IList<TicketDto>>> GetCustomerTickets(int customerId)
    {
        var tickets = await _ticketContext.Tickets
            .Where(i => i.CustomerId == customerId)
            .Select(i => i.ToDto())
            .ToListAsync();

        return Ok(tickets);
    }
}
