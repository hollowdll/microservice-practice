namespace ReceiptApi.Dtos;

public class ReceiptDto
{
    public int Id { get; set; }
    public string Message { get; set; } = null!;
    public int CustomerId { get; set; }
    public long TicketId { get; set; }
    public DateTime CreatedAt { get; set; }
}