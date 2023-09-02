namespace TicketApi.Models;

public class Ticket
{
    public ulong Id { get; set; }
    public string Code { get; set; } = null!;
    public string Message { get; set; } = null!;
    public DateTime CreatedAt { get; set; }

    public uint CustomerId { get; set; }
    public Customer Customer { get; set; } = null!;
}
