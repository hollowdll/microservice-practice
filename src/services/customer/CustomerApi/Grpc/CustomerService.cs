using Grpc.Core;
using CustomerApi.Data;
using CustomerApi.Models;
using Google.Protobuf.WellKnownTypes;
using Microsoft.EntityFrameworkCore;

namespace GrpcCustomer;

public class CustomerService : Customer.CustomerBase
{
    private readonly ILogger<CustomerService> _logger;
    private readonly CustomerDbContext _customerDbContext;
    
    public CustomerService(ILogger<CustomerService> logger, CustomerDbContext customerDbContext)
    {
        _logger = logger;
        _customerDbContext = customerDbContext;
    }

    public override async Task<CustomerListResponse> GetAllCustomers(CustomerGetRequest request, ServerCallContext context)
    {
        _logger.LogInformation("[CustomerService] gRPC call method {Method}", context.Method);

        var response = new CustomerListResponse();
        
        var customers = await _customerDbContext.Customers
            .Select(i => MapToCustomerResponse(i))
            .ToListAsync();
        customers.ForEach(i => response.Customers.Add(i));
        
        context.Status = new Status(StatusCode.OK, "Get all customers");

        return response;
    }

    private static CustomerResponse MapToCustomerResponse(CustomerApi.Models.Customer customer)
    {
        return new CustomerResponse
        {
            Id = customer.Id,
            FirstName = customer.FirstName,
            LastName = customer.LastName,
            Email = customer.LastName,
            CreatedAt = Timestamp.FromDateTime(customer.CreatedAt),
        };
    }
}