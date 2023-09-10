namespace ReceiptApi.Models;

public class Ticket
{
    public int Id { get; set; }
    public string Code { get; set; } = null!;
    public string Message { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
}