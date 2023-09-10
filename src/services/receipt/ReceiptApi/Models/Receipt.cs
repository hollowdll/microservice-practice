namespace ReceiptApi.Models;

public class Receipt
{
    public int Id { get; set; }
    public string Message { get; set; } = null!;
    public int CustomerId { get; set; }
    public long TicketId { get; set; }
    public int CustomerTicketCount { get; set; }
    public DateTime CreatedAt { get; set; }

    public Receipt() {}

    public Receipt(Customer customer, Ticket ticket, int customerTicketCount)
    {
        Message = $@"Customer {customer.FirstName} {customer.LastName}
        generated a ticket with code {ticket.Code} at {ticket.CreatedAt}.
        They have generated {customerTicketCount} tickets in total.";
        CustomerId = customer.Id;
        TicketId = ticket.Id;
        CustomerTicketCount = customerTicketCount;
        CreatedAt = DateTime.Now;
    }
}