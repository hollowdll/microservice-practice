namespace TicketApi.Dtos;

public class TicketDto
{
    public long Id { get; set; }
    public Guid Code { get; set; }
    public string Message { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    // public int CustomerId { get; set; }
}