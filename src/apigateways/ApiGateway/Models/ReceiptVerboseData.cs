namespace ApiGateway.Models;

public class ReceiptVerboseData
{
    public ReceiptData Receipt { get; set; } = null!;
    public TicketData Ticket { get; set; } = null!;
    public CustomerData Customer { get; set; } = null!;
}