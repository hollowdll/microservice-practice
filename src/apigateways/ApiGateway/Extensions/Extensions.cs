using ApiGateway.Services;

internal static class Extensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // These are for sending HTTP requests to different services.
        services.AddHttpClient<ICustomerService, CustomerHttpService>();
        services.AddHttpClient<ITicketService, TicketHttpService>();
        services.AddHttpClient<IReceiptService, ReceiptHttpService>();

        return services;
    }
}