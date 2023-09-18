using System.Text.Json;
using ApiGateway.Config;
using ApiGateway.Models;
using Microsoft.Extensions.Options;
using System.Text;

namespace ApiGateway.Services;

public class CustomerHttpService : ICustomerService
{
    private readonly ILogger<CustomerHttpService> _logger;
    private readonly HttpClient _apiClient;
    private readonly string _customerUrl;

    public CustomerHttpService(
        ILogger<CustomerHttpService> logger,
        HttpClient httpClient,
        IOptions<UrlsConfig> urlsConfig)
    {
        _logger = logger;
        _apiClient = httpClient;
        _customerUrl = urlsConfig.Value.Customer;
    }

    public async Task<IList<CustomerData>> GetAll()
    {
        var uri = $"{_customerUrl}{UrlsConfig.CustomerOperations.GetAllCustomers()}";
        _logger.LogDebug("[GetAll] Calling {Uri} to get all customers", uri);
        var customers = await _apiClient.GetFromJsonAsync<List<CustomerData>>(uri);

        if (customers == null)
        {
            return new List<CustomerData>();
        }

        return customers;
    }

    public async Task<CustomerData?> GetById(int id)
    {
        var uri = $"{_customerUrl}{UrlsConfig.CustomerOperations.GetCustomerById(id)}";
        _logger.LogDebug("[GetById] Calling {Uri} to get customer with id {Id}", uri, id);
        var customer = await _apiClient.GetFromJsonAsync<CustomerData>(uri);

        return customer;
    }

    public async Task Create(CreateCustomerRequest createCustomerRequest)
    {
        var uri = $"{_customerUrl}{UrlsConfig.CustomerOperations.CreateCustomer()}";
        _logger.LogDebug("[Create] Calling {Uri} to create a customer", uri);

        var content = new StringContent(
            JsonSerializer.Serialize(createCustomerRequest),
            Encoding.UTF8,
            "application/json");
        var response = await _apiClient.PostAsync(uri, content);

        response.EnsureSuccessStatusCode();
    }
}