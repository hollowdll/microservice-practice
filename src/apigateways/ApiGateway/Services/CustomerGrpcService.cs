using ApiGateway.Models;
using GrpcCustomer;
using Grpc.Core;

namespace ApiGateway.Services;

public class CustomerGrpcService : ICustomerService
{
    private readonly Customer.CustomerClient _customerClient;
    private readonly ILogger<CustomerGrpcService> _logger;

    public CustomerGrpcService(Customer.CustomerClient customerClient, ILogger<CustomerGrpcService> logger)
    {
        _customerClient = customerClient;
        _logger = logger;
    }

    public async Task<IList<CustomerData>> GetAll()
    {
        try
        {
            var response = await _customerClient.GetAllCustomersAsync(new CustomerGetRequest());
            if (response == null)
            {
                return new List<CustomerData>();
            }

            var customers = new List<CustomerData>();

            response.Customers.ToList().ForEach(i => customers.Add(MapToCustomerData(i)));

            return customers;
        }
        catch(RpcException e)
        {
            LogGrpcError(e);

            throw e;
        }
    }

    public async Task<CustomerData?> GetById(int id)
    {
        try
        {
            var response = await _customerClient.GetCustomerByIdAsync(new CustomerGetByIdRequest { Id = id });
            if (response == null)
            {
                return null;
            }

            return MapToCustomerData(response);
        }
        catch (RpcException e)
        {
            LogGrpcError(e);

            if (e.StatusCode == StatusCode.NotFound)
            {
                return null;
            }

            throw e;
        }
    }

    public async Task<CustomerData> Create(CreateCustomerRequest createCustomerRequest)
    {
        var request = new CustomerCreateRequest
        {
            FirstName = createCustomerRequest.FirstName,
            LastName = createCustomerRequest.LastName,
            Email = createCustomerRequest.Email,
        };

        try
        {
            var response = await _customerClient.CreateCustomerAsync(request);
            
            return MapToCustomerData(response);
        }
        catch (RpcException e)
        {
            LogGrpcError(e);

            throw e;
        }
    }

    private static CustomerData MapToCustomerData(CustomerResponse customerResponse)
    {
        return new CustomerData
        {
            Id = customerResponse.Id,
            FirstName = customerResponse.FirstName,
            LastName = customerResponse.LastName,
            Email = customerResponse.Email,
            CreatedAt = customerResponse.CreatedAt.ToDateTime(),
        };
    }

    private void LogGrpcError(RpcException e)
    {
        _logger.LogError(e, "[CustomerGrpcService] Error calling via gRPC: {Status}", e.Status);
    }
}