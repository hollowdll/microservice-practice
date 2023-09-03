using TicketApi.Models;
using TicketApi.Dtos;

namespace TicketApi.Helpers;

public static class DtoExtensions
{
    /// <summary>
    /// Creates a new <see cref="TicketDto" /> from <see cref="Ticket" />.
    /// </summary>
    public static TicketDto ToDto(this Ticket ticket)
    {
        return new TicketDto
        {
            Id = ticket.Id,
            Code = ticket.Code,
            Message = ticket.Message,
            CreatedAt = ticket.CreatedAt
        };
    }
}
