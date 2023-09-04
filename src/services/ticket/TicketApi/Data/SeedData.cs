namespace TicketApi.Data;

public static class SeedData
{
    public static void InitializeDatabase(TicketDbContext ticketContext)
    {
        ticketContext.Database.EnsureCreated();
    }
}