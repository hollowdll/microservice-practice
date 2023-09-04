using System.Data.SqlTypes;

namespace TicketApi.Models;

public class Ticket
{
    public Ticket()
    {
        var now = DateTime.Now;
        
        Code = Guid.NewGuid();
        Message = $"Ticket generated {now}";
        CreatedAt = now;
    }

    public long Id { get; set; }
    public Guid Code { get; set; }
    public string Message { get; set; } = null!;
    public DateTime CreatedAt { get; set; }

    // public int CustomerId { get; set; }
    // public Customer Customer { get; set; } = null!;
}
