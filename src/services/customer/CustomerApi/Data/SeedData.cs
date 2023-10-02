using CustomerApi.Models;

namespace CustomerApi.Data;

public static class SeedData
{
    public static void InitializeDatabase(CustomerDbContext customerDbContext)
    {
        customerDbContext.Database.EnsureCreated();

        if (!customerDbContext.Customers.Any())
        {
            customerDbContext.Customers.Add(new Customer("John", "Smith", "john.smith@example.com"));
            customerDbContext.Customers.Add(new Customer("Noah", "Anderson", "noah.anderson@example.com"));
            customerDbContext.Customers.Add(new Customer("Mary", "Johnson", "mary.johnson@example.com"));
            customerDbContext.Customers.Add(new Customer("Dan", "Smith", "dan.smith@example.com"));

            customerDbContext.SaveChanges();
        }
    }
}