using System.Text.Json;
using ApiGateway.Config;
using ApiGateway.Models;
using Microsoft.Extensions.Options;
using System.Text;
using System.Net;

namespace ApiGateway.Services;

public class TicketHttpService : ITicketService
{
    private readonly ILogger<TicketHttpService> _logger;
    private readonly HttpClient _apiClient;
    private readonly string _ticketUrl;

    public TicketHttpService(
        ILogger<TicketHttpService> logger,
        HttpClient httpClient,
        IOptions<UrlsConfig> urlsConfig)
    {
        _logger = logger;
        _apiClient = httpClient;
        _ticketUrl = urlsConfig.Value.Ticket;
    }

    public async Task<IList<TicketData>> GetAll()
    {
        var uri = $"{_ticketUrl}{UrlsConfig.TicketOperations.GetAllTickets()}";
        _logger.LogDebug("[GetAll] Calling {Uri} to get all tickets", uri);
        var tickets = await _apiClient.GetFromJsonAsync<List<TicketData>>(uri);

        if (tickets == null)
        {
            return new List<TicketData>();
        }

        return tickets;
    }
    
    public async Task<TicketData?> GetById(int id)
    {
        var uri = $"{_ticketUrl}{UrlsConfig.TicketOperations.GetTicketById(id)}";
        _logger.LogDebug("[GetById] Calling {Uri} to get ticket with id {Id}", uri, id);
        var response = await _apiClient.GetAsync(uri);

        if (response.StatusCode == HttpStatusCode.NotFound)
        {
            return null;
        }

        var ticket = await response.Content.ReadFromJsonAsync<TicketData>();
        if (ticket == null)
        {
            return null;
        }

        return ticket;
    }

    public async Task<TicketData> Create(CreateTicketRequest createTicketRequest)
    {
        var uri = $"{_ticketUrl}{UrlsConfig.TicketOperations.CreateTicket()}";
        _logger.LogDebug("[Create] Calling {Uri} to create a ticket", uri);

        var content = new StringContent(
            JsonSerializer.Serialize(createTicketRequest),
            Encoding.UTF8,
            "application/json");
        var response = await _apiClient.PostAsync(uri, content);

        response.EnsureSuccessStatusCode();

        var ticket = await response.Content.ReadFromJsonAsync<TicketData>();
        if (ticket == null)
        {
            throw new JsonException("JSON deserialization results null");
        }

        return ticket;
    }

    public async Task<IList<TicketData>> GetAllByCustomerId(int customerId)
    {
        var uri = $"{_ticketUrl}{UrlsConfig.TicketOperations.GetCustomerTickets(customerId)}";
        _logger.LogDebug("[GetAllByCustomerId] Calling {Uri} to get all tickets with customer id {Id}", uri, customerId);
        var tickets = await _apiClient.GetFromJsonAsync<List<TicketData>>(uri);

        if (tickets == null)
        {
            return new List<TicketData>();
        }

        return tickets;
    }
}