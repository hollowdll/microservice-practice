namespace ReceiptApi.Data;

public static class SeedData
{
    public static void InitializeDatabase(ReceiptDbContext receiptContext)
    {
        receiptContext.Database.EnsureCreated();
    }
}