namespace ApiGateway.Models;

public class ReceiptData
{
    public int Id { get; set; }
    public string Message { get; set; } = null!;
    public int CustomerId { get; set; }
    public int TicketId { get; set; }
    public int CustomerTicketCount { get; set; }
    public DateTime CreatedAt { get; set; }
}