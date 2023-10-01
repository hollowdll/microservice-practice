using Microsoft.AspNetCore.Mvc;
using ApiGateway.Models;
using ApiGateway.Services;
using Microsoft.AspNetCore.Localization;

namespace ApiGateway.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class TicketController : ControllerBase
{
    private readonly ITicketService _ticketService;
    private readonly IReceiptService _receiptService;
    private readonly ICustomerService _customerService;
    
    public TicketController(
        ITicketService ticketService,
        IReceiptService receiptService,
        ICustomerService customerService)
    {
        _ticketService = ticketService;
        _receiptService = receiptService;
        _customerService = customerService;
    }

    // Gets all tickets if no query parameters.
    // If customer id is given in query param, returns tickets for that customer.
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
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ReceiptVerboseData>> CreateTicketAndReceipt(CreateTicketRequest createTicketRequest)
    {
        var customer = await _customerService.GetById(createTicketRequest.CustomerId);
        if (customer == null)
        {
            return BadRequest("No customer found with the given customer id");
        }

        var ticket = await _ticketService.Create(createTicketRequest);
        var customerTickets = await _ticketService.GetAllByCustomerId(createTicketRequest.CustomerId);

        var receiptRequest = new CreateReceiptRequest {
            CustomerId = createTicketRequest.CustomerId,
            TicketId = ticket.Id,
            CustomerTicketCount = customerTickets.Count
        };
        var receipt = await _receiptService.Create(receiptRequest);

        return Ok(new ReceiptVerboseData { Receipt = receipt, Ticket = ticket, Customer = customer });
    }
}