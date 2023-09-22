using ApiGateway.Services;

internal static class Extensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddHttpClient<ICustomerService, CustomerHttpService>();

        return services;
    }
}