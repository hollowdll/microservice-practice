using Grpc.Core;
using TicketApi.Data;
using Google.Protobuf.WellKnownTypes;
using Microsoft.EntityFrameworkCore;

namespace GrpcTicket;

public class TicketService : Ticket.TicketBase
{
    private readonly ILogger<TicketService> _logger;
    private readonly TicketDbContext _ticketDbContext;
    
    public TicketService(ILogger<TicketService> logger, TicketDbContext ticketDbContext)
    {
        _logger = logger;
        _ticketDbContext = ticketDbContext;
    }

    public override async Task<TicketListResponse> GetAllTickets(TicketGetRequest request, ServerCallContext context)
    {
        LogGrpcCall(context);

        var response = new TicketListResponse();
        
        var tickets = await _ticketDbContext.Tickets
            .Select(i => MapToTicketResponse(i))
            .ToListAsync();
        tickets.ForEach(i => response.Tickets.Add(i));
        
        context.Status = new Status(StatusCode.OK, "Get all tickets");

        return response;
    }

    public override async Task<TicketResponse> GetTicketById(TicketGetByIdRequest request, ServerCallContext context)
    {
        LogGrpcCall(context);

        var ticket = await _ticketDbContext.Tickets.FindAsync(request.Id);
        if (ticket == null)
        {
            context.Status = new Status(StatusCode.NotFound, $"Ticket with id {request.Id} not found");
        }
        else
        {
            context.Status = new Status(StatusCode.OK, $"Ticket with id {request.Id} found");

            return MapToTicketResponse(ticket);
        }

        return new TicketResponse();
    }

    public override async Task<TicketResponse> CreateTicket(TicketCreateRequest request, ServerCallContext context)
    {
        LogGrpcCall(context);

        var ticket = new TicketApi.Models.Ticket(request.CustomerId);

        _ticketDbContext.Tickets.Add(ticket);
        await _ticketDbContext.SaveChangesAsync();

        context.Status = new Status(StatusCode.OK, "Ticket added");

        return MapToTicketResponse(ticket);
    }

    public override async Task<TicketListResponse> GetCustomerTickets(CustomerTicketsGetRequest request, ServerCallContext context)
    {
        LogGrpcCall(context);

        var response = new TicketListResponse();

        var tickets = await _ticketDbContext.Tickets
            .Where(i => i.CustomerId == request.CustomerId)
            .Select(i => MapToTicketResponse(i))
            .ToListAsync();
        tickets.ForEach(i => response.Tickets.Add(i));
        
        context.Status = new Status(StatusCode.OK, "Get customer tickets");

        return response;
    }

    private static TicketResponse MapToTicketResponse(TicketApi.Models.Ticket ticket)
    {
        return new TicketResponse
        {
            Id = ticket.Id,
            Code = ticket.Code,
            Message = ticket.Message,
            CustomerId = ticket.CustomerId,
            CreatedAt = Timestamp.FromDateTime(DateTime.SpecifyKind(ticket.CreatedAt, DateTimeKind.Utc)),
        };
    }

    private void LogGrpcCall(ServerCallContext context)
    {
        _logger.LogInformation("[TicketService] gRPC call method {Method}", context.Method);
    }
}