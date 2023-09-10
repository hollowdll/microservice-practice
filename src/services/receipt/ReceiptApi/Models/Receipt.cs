namespace ReceiptApi.Models;

public class Receipt
{
    public int Id { get; set; }
    public string Message { get; set; } = null!;
    public int CustomerId { get; set; }
    public int TicketId { get; set; }
    public int CustomerTicketCount { get; set; }
    public DateTime CreatedAt { get; set; }

    public Receipt() {}

    public Receipt(int customerId, int ticketId, int customerTicketCount)
    {
        Message = "Receipt for a generated ticket";
        CustomerId = customerId;
        TicketId = ticketId;
        CustomerTicketCount = customerTicketCount;
        CreatedAt = DateTime.Now;
    }
}