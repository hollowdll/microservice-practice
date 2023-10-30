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

    public override async Task<CustomerResponse> GetCustomerById(CustomerGetByIdRequest request, ServerCallContext context)
    {
        _logger.LogInformation("[CustomerService] gRPC call method {Method}", context.Method);

        var customer = await _customerDbContext.Customers.FindAsync(request.Id);
        if (customer == null)
        {
            context.Status = new Status(StatusCode.NotFound, $"Customer with id {request.Id} not found");
        }
        else
        {
            context.Status = new Status(StatusCode.OK, $"Customer with id {request.Id} found");

            return MapToCustomerResponse(customer);
        }

        return new CustomerResponse();
    }

    public override async Task<CustomerResponse> CreateCustomer(CustomerCreateRequest request, ServerCallContext context)
    {
        _logger.LogInformation("[CustomerService] gRPC call method {Method}", context.Method);

        var customer = new CustomerApi.Models.Customer(
            request.FirstName,
            request.LastName,
            request.Email);

        _customerDbContext.Customers.Add(customer);
        await _customerDbContext.SaveChangesAsync();

        context.Status = new Status(StatusCode.OK, "Customer added");

        return MapToCustomerResponse(customer);
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