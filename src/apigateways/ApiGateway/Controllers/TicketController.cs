using Microsoft.AspNetCore.Mvc;
using ApiGateway.Models;
using ApiGateway.Services;

namespace ApiGateway.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class TicketController : ControllerBase
{
    private readonly ITicketService _ticketService;
    
    public TicketController(ITicketService ticketService)
    {
        _ticketService = ticketService;
    }

    // Gets all tickets if no query parameters.
    // If customer id is given in query param, this returns tickets for that customer.
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IList<TicketData>>> GetAllTickets([FromQuery(Name = "customer")] int? customerId)
    {
        if (customerId.HasValue)
        {
            var customerTickets = await _ticketService.GetAllByCustomerId((int) customerId);

            return Ok(customerTickets);
        }

        var tickets = await _ticketService.GetAll();

        return Ok(tickets);
    }

    [HttpGet]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IList<TicketData>>> GetTicketById(int id)
    {
        var ticket = await _ticketService.GetById(id);
        if (ticket == null)
        {
            return NotFound();
        }

        return Ok(ticket);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult> CreateTicket(CreateTicketRequest createTicketRequest)
    {
        var ticket = await _ticketService.Create(createTicketRequest);

        return CreatedAtAction(nameof(GetTicketById), new { id = ticket.Id }, null);
    }

    /*
    [HttpGet]
    [Route("{customerId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IList<TicketData>>> GetCustomerTickets(int customerId)
    {

    }
    */
}