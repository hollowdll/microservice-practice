namespace TicketApi.Models;

public class Ticket
{
    public Ticket() {}

    public Ticket(string message)
    {
        Code = "ABCD1234";
        Message = message;
        CreatedAt = DateTime.Now;
    }

    public long Id { get; set; }
    public string Code { get; set; } = null!;
    public string Message { get; set; } = null!;
    public DateTime CreatedAt { get; set; }

    // public int CustomerId { get; set; }
    // public Customer Customer { get; set; } = null!;
}
