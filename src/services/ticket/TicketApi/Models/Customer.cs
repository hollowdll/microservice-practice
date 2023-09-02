namespace TicketApi.Models;

public class Customer
{
    public uint Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
}