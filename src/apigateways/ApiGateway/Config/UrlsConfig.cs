namespace ApiGateway.Config;

public class UrlsConfig
{
    public class CustomerOperations
    {
        public static string GetAllCustomers() => "/api/v1/customer/all";
        public static string GetCustomerById(int id) => $"/api/v1/customer/id/{id}";
        public static string CreateCustomer() => "/api/v1/customer";
    }

    public class TicketOperations
    {
        public static string GetAllTickets() => "/api/v1/ticket/all";
        public static string GetTicketById(int id) => $"/api/v1/ticket/id/{id}";
        public static string CreateTicket() => "/api/v1/ticket";
        public static string GetCustomerTickets(int customerId) => $"/api/v1/ticket/all/customer/{customerId}";
    }

    public class ReceiptOperations
    {
        public static string GetAllReceipts() => "/api/v1/receipt/all";
        public static string GetReceiptById(int id) => $"/api/v1/receipt/id/{id}";
        public static string CreateReceipt() => "/api/v1/receipt";
    }

    // Microservice URLs
    public string Customer { get; set; } = null!;
    public string Ticket { get; set; } = null!;
    public string Receipt { get; set; } = null!;
}