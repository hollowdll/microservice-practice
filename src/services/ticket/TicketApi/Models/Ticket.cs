namespace TicketApi.Models;

public class Ticket
{
    public Ticket()
    {   
        Code = Guid.NewGuid().ToString().ToUpper();
        Message = "Ticket with a randomly generated code.";
        CreatedAt = DateTime.Now;
    }

    public long Id { get; set; }
    public string Code { get; set; } = null!;
    public string Message { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
}
