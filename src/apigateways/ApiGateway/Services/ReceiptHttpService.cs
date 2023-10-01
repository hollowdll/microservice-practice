using System.Text.Json;
using ApiGateway.Config;
using ApiGateway.Models;
using Microsoft.Extensions.Options;
using System.Text;
using System.Net;

namespace ApiGateway.Services;

public class ReceiptHttpService : IReceiptService
{
    private readonly ILogger<ReceiptHttpService> _logger;
    private readonly HttpClient _apiClient;
    private readonly string _receiptUrl;

    public ReceiptHttpService(
        ILogger<ReceiptHttpService> logger,
        HttpClient httpClient,
        IOptions<UrlsConfig> urlsConfig)
    {
        _logger = logger;
        _apiClient = httpClient;
        _receiptUrl = urlsConfig.Value.Receipt;
    }

    public async Task<IList<ReceiptData>> GetAll()
    {
        var uri = $"{_receiptUrl}{UrlsConfig.ReceiptOperations.GetAllReceipts()}";
        _logger.LogDebug("[GetAll] Calling {Uri} to get all receipts", uri);
        var receipts = await _apiClient.GetFromJsonAsync<List<ReceiptData>>(uri);

        if (receipts == null)
        {
            return new List<ReceiptData>();
        }

        return receipts;
    }
    
    public async Task<ReceiptData?> GetById(int id)
    {
        var uri = $"{_receiptUrl}{UrlsConfig.ReceiptOperations.GetReceiptById(id)}";
        _logger.LogDebug("[GetById] Calling {Uri} to get receipt with id {Id}", uri, id);
        var response = await _apiClient.GetAsync(uri);

        if (response.StatusCode == HttpStatusCode.NotFound)
        {
            return null;
        }

        var receipt = await response.Content.ReadFromJsonAsync<ReceiptData>();
        if (receipt == null)
        {
            return null;
        }

        return receipt;
    }

    public async Task<ReceiptData> Create(CreateReceiptRequest createReceiptRequest)
    {
        var uri = $"{_receiptUrl}{UrlsConfig.ReceiptOperations.CreateReceipt()}";
        _logger.LogDebug("[Create] Calling {Uri} to create a receipt", uri);

        var content = new StringContent(
            JsonSerializer.Serialize(createReceiptRequest),
            Encoding.UTF8,
            "application/json");
        var response = await _apiClient.PostAsync(uri, content);

        response.EnsureSuccessStatusCode();

        var receipt = await response.Content.ReadFromJsonAsync<ReceiptData>();
        if (receipt == null)
        {
            throw new JsonException("JSON deserialization results null");
        }

        return receipt;
    }

    public async Task<IList<ReceiptData>> GetAllByCustomerId(int customerId)
    {
        var uri = $"{_receiptUrl}{UrlsConfig.ReceiptOperations.GetCustomerReceipts(customerId)}";
        _logger.LogDebug("[GetAllByCustomerId] Calling {Uri} to get all receipts with customer id {Id}", uri, customerId);
        var receipts = await _apiClient.GetFromJsonAsync<List<ReceiptData>>(uri);

        if (receipts == null)
        {
            return new List<ReceiptData>();
        }

        return receipts;
    }
}