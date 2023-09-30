namespace ApiGateway.Models;

public class CreateReceiptRequest
{
    public int CustomerId { get; set; }
    public int TicketId { get; set; }
    public int CustomerTicketCount { get; set; }
}