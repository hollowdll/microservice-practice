using ApiGateway.Config;
using ApiGateway.Services;
using GrpcCustomer;
using Microsoft.Extensions.Options;

internal static class Extensions
{
    // Setup HTTP clients
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // These are for sending HTTP requests to different services.
        services.AddHttpClient<ICustomerService, CustomerHttpService>();
        services.AddHttpClient<ITicketService, TicketHttpService>();
        services.AddHttpClient<IReceiptService, ReceiptHttpService>();

        return services;
    }

    // Setup gRPC services and clients
    public static IServiceCollection AddGrpcServices(this IServiceCollection services)
    {
        services.AddScoped<ICustomerService, CustomerGrpcService>();

        services.AddGrpcClient<Customer.CustomerClient>((services, options) =>
        {
            var customerApi = services.GetRequiredService<IOptions<UrlsConfig>>().Value.GrpcCustomer;
            options.Address = new Uri(customerApi);
        });

        return services;
    }
}