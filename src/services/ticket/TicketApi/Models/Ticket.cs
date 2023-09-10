namespace TicketApi.Models;

public class Ticket
{
    public Ticket() {}

    public Ticket(int customerId)
    {   
        Code = Guid.NewGuid().ToString().ToUpper();
        Message = "Ticket with a randomly generated code.";
        CustomerId = customerId;
        CreatedAt = DateTime.Now;
    }

    public int Id { get; set; }
    public string Code { get; set; } = null!;
    public string Message { get; set; } = null!;
    public int CustomerId { get; set; }
    public DateTime CreatedAt { get; set; }
}
