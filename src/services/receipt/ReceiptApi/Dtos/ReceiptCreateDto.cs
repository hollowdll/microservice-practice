namespace ReceiptApi.Dtos;

public class ReceiptCreateDto
{
    public int CustomerId { get; set; }
    public int TicketId { get; set; }
    public int CustomerTicketCount { get; set; }
}