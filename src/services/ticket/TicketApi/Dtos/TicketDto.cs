namespace TicketApi.Dtos;

public class TicketDto
{
    public int Id { get; set; }
    public string Code { get; set; } = null!;
    public string Message { get; set; } = null!;
    public int CustomerId { get; set; }
    public DateTime CreatedAt { get; set; }
}