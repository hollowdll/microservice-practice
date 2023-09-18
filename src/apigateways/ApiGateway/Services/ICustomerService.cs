using ApiGateway.Models;

namespace ApiGateway.Services;

public interface ICustomerService
{
    Task<IList<CustomerData>> GetAll();
    Task<CustomerData?> GetById(int id);
    Task Create(CreateCustomerRequest createCustomerRequest);
}