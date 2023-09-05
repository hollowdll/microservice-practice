namespace CustomerApi.Data;

public static class SeedData
{
    public static void InitializeDatabase(CustomerDbContext customerContext)
    {
        customerContext.Database.EnsureCreated();
    }
}